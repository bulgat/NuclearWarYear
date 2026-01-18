using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Model.scenario;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Nation;
using Assets.Scripts.Model.param;
using static Assets.Scripts.Model.param.GlobalParam;

[System.Serializable]
public class CountryLider
{
    public int FlagId;
    public int ViewIdImageFlag;
    [SerializeField]
    public bool Player;
    public GameObject PropagandaBuilding;
    public List<Incident> ReleaseCommandList { private set; get; }
    private bool _dead;
    private List<Incident> _missleList;
    public int FlagIdAttack = 1;
    private List<CityModel> _townListOwn;
    private int _maxPopulation;
    public TargetCityModel TargetCitySelectPlayer { private set; get; }
    public CountryLider FiendLider { private set; get; }

    public string Name { private set; get; }

    IncidentEvent EventTotalTurn;

    public int MissleId;
    public RelationShip _RelationFeind;
    public int GraphicId { get; }
    public bool MoveMade { private set; get; }
    public CountryLider(
        bool player,
        List<Incident> missleList,
        GameObject PropagandaBuild,
        List<CityModel> TownList,
        ScenarioLider scenarioLider,
        int CountryId)
    {
        this.FlagId = scenarioLider.FlagId;
        this.Player = player;
        this._missleList = missleList;
        PropagandaBuilding = PropagandaBuild;
        this.Name = scenarioLider.Name;
        this.GraphicId = scenarioLider.GraphicId;
        this._townListOwn = new List<CityModel>();

        foreach (CityModel TownCity in TownList)
        {

            if (CountryId == TownCity.CountryId)
            {
                TownCity.FlagId = scenarioLider.FlagId;
                _townListOwn.Add(TownCity);

            }
        }
        _maxPopulation = GetAllOwnPopulation();
        _RelationFeind = new RelationShip(FlagId);
        this.EventTotalTurn = new IncidentEvent(GlobalParam.TypeEvent.Propaganda);
    }
    public void DoneMoveMade(bool Value)
    {
        MoveMade = Value;

    }
    public string SetEventTotalMessageTurn(string eventTotalTurn, GlobalParam.TypeEvent eventName)
    {
        this.EventTotalTurn = new IncidentEvent(eventName) { EventMessage = eventTotalTurn };
        return this.EventTotalTurn.EventMessage;

    }
    public IncidentEvent GetEventTotalTurn()
    {
        return this.EventTotalTurn;
    }

    public int GetMood(int FlagId)
    {
        return _RelationFeind.GetMood(FlagId);
    }
    public void SetDead()
    {
        this._dead = true;
    }
    public bool GetDead()
    {
        return this._dead;
    }

    public void SetCommandRealise(Incident commandLider)
    {
        this.ReleaseCommandList = new List<Incident>() { commandLider };

    }

    public int GetAllOwnPopulation()
    {
        int maxPopulation = 0;
        foreach (CityModel TownCity in _townListOwn)
        {
            maxPopulation += TownCity.GetPopulation();
        }
        return maxPopulation;
    }
    public List<CityModel> GetOwnTownListLiderFilterPopulation()
    {
        return _townListOwn.Where(a => a.GetPopulation() > 0).ToList();

    }

    public int GetBomberCount()
    {
        return this._missleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).Count();
    }
    public List<Incident> GetDefenceWeapon()
    {
        return this._missleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Defence).ToList();
    }

    public IWeapon GetBomber()
    {


        return this._missleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).FirstOrDefault();
    }


    public void RemoveDefenceWeapon()
    {

        Incident defenceWeapon = _missleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Defence).FirstOrDefault();
        _missleList.Remove(defenceWeapon);
    }

    
    public int GetMissleCount()
    {
        return _missleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Missle).Count();
    }
    
    public IWeapon GetMissleFirst()
    {
        Debug.Log("00800   AttackBomber id = "+ _missleList.FirstOrDefault()?.Id);
        return _missleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Missle).FirstOrDefault();
    }
    public void RemoveWeapon(GlobalParam.TypeEvent name)
    {
        var deleteMissle = _missleList.FirstOrDefault(a => a.GetName() == name);

        if (deleteMissle != null)
        {
            _missleList.Remove(deleteMissle);
        }

    }
    public void AddMissle(List<Incident> missleList)
    {
        if (missleList != null)
        {
            _missleList.AddRange(missleList);
        }
    }
    public GameObject GetCentralBuildingPropogation()
    {
        return PropagandaBuilding;
    }
    public void ResetTargetCity()
    {
        FiendLider = null;
        TargetCitySelectPlayer = null;
    }
    public void SetTargetCity(TargetCityModel targetCitySelectPlayer)
    {
        FiendLider = targetCitySelectPlayer.EnemyLider;
        TargetCitySelectPlayer = targetCitySelectPlayer;
    }
 
    public CityModel GetFirstCityHelper()
    {
        int index = UnityEngine.Random.Range(0, _townListOwn.Count);
        return _townListOwn[index];
    }

    public List<Incident> GetAllWeapon()
    {
        return _missleList;
    }


}
