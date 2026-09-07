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
    public List<Incident> WeaponList { private set; get; }
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
        this.WeaponList = missleList;
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
        return this.WeaponList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).Count();
    }
    public List<Incident> GetDefenceWeapon()
    {
        return this.WeaponList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Defence).ToList();
    }
    public IWeapon GetDefenceFirst()
    {
        return this.WeaponList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Defence).FirstOrDefault();
    }
    public IWeapon GetBomberFirst()
    {


        return this.WeaponList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).FirstOrDefault();
    }


    public void RemoveDefenceWeapon()
    {

        Incident defenceWeapon = WeaponList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Defence).FirstOrDefault();
        WeaponList.Remove(defenceWeapon);
    }

    
    public int GetMissleCount()
    {
        return WeaponList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Missle).Count();
    }
    
    public IWeapon GetMissleFirst()
    {

        return WeaponList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Missle).FirstOrDefault();
    }
    public void RemoveWeapon(GlobalParam.TypeEvent name)
    {
        var deleteMissle = WeaponList.FirstOrDefault(a => a.GetName() == name);

        if (deleteMissle != null)
        {
            WeaponList.Remove(deleteMissle);
        }

    }
    public void AddMissle(List<Incident> missleList)
    {
        if (missleList != null)
        {
            WeaponList.AddRange(missleList);
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
        return WeaponList;
    }
    public int GetIndexLider()
    {
        return FlagId - 1;
    }

}
