using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Model.scenario;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Nation;
using Assets.Scripts.Model.param;

[System.Serializable]
public class CountryLider 
{
    public int FlagId;
	public int ViewIdImageFlag;
	[SerializeField]
	public bool Player;
	
	public GameObject PropagandaBuilding;
	//private List<CommandLider> _CommandLiderList;
    public  List<CommandLider> StackCommandLiderList { private set; get; }

    public List<Incident> ReleaseCommandList { private set; get; }
    private bool _dead;
	private List<IWeapon> _MissleList;

	public int FlagIdAttack=1;
	
	private List<CityModel> _TownListOwn;

	private int _maxPopulation;
	private TargetCityModel _targetCitySelectPlayer;
    public CountryLider FiendLider { private set; get; }

    public string Name { private set; get; }

    IncidentEvent EventTotalTurn;

    public int MissleId;
	public RelationShip _RelationFeind;
	public int GraphicId { get; }
	public bool MoveMade { private set; get; }
	public CountryLider(bool player,List<IWeapon> missleList,
		GameObject PropagandaBuild,List<CityModel> TownList, ScenarioLider scenarioLider,int CountryId) 
	{
		//this._CommandLiderList = new List<CommandLider>();
        this.StackCommandLiderList = new List<CommandLider>();
        this.FlagId = scenarioLider.FlagId;
		this.Player = player;
		this._MissleList=missleList;
		PropagandaBuilding = PropagandaBuild;
		this.Name = scenarioLider.Name;
		this.GraphicId = scenarioLider.GraphicId;
        this._TownListOwn = new List<CityModel>();

		foreach(CityModel TownCity in TownList){
			
			if(CountryId == TownCity.CountryId){
				TownCity.FlagId = scenarioLider.FlagId;
                _TownListOwn.Add(TownCity);
				
			}
		}
		_maxPopulation=GetAllOwnPopulation();
		_RelationFeind = new RelationShip();
		this.EventTotalTurn = new IncidentEvent(GlobalParam.TypeEvent.Propaganda);
	}
	public void DoneMoveMade(bool Value)
    {
		MoveMade = Value;

	}
	public string SetEventTotalMessageTurn(string eventTotalTurn, GlobalParam.TypeEvent eventName)
    {
		this.EventTotalTurn = new IncidentEvent(eventName) { EventMessage = eventTotalTurn};
		return this.EventTotalTurn.EventMessage;

    }
	public IncidentEvent GetEventTotalTurn() {
		return this.EventTotalTurn;
	}
	/*
	public string GetName()
	{
		return this.Name;

	}
	*/
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
	public List<CommandLider> GetStackCommandLider(int Year)
	{
		return this.StackCommandLiderList.Where(a=>a.GetIncident().Year == Year).ToList();
	}
    public CommandLider GetCommandLiderFirst(int CountYear)
    {
		/*
		if (this._CommandLiderList == null)
		{
			return null;
		}*/
		Debug.Log("GetCommandLiderFirst  CountYear = " + CountYear);
        //return this._CommandLiderList.FirstOrDefault();
        return this.StackCommandLiderList.FirstOrDefault(a=>a.GetIncident().Year == CountYear);
    }
	public void SetCommandRealise(Incident commandLider)
	{
		this.ReleaseCommandList = new List<Incident>() { commandLider  };

	}

    public void AddCommandLiderList(List<CommandLider> commandLiderList)
	{
		//this._CommandLiderList = commandLiderList;
        this.StackCommandLiderList.AddRange(commandLiderList);
        //this._CommandLiderList.AddRange(commandLiderList);
    }
	public int GetAllOwnPopulation() {
		int maxPopulation=0;
		foreach(CityModel TownCity in _TownListOwn){
			maxPopulation+=TownCity.GetPopulation();
		}
		return maxPopulation;
	}
	public List<CityModel> GetOwnTownListLiderFilterPopulation()
    {
		return _TownListOwn.Where(a=>a.GetPopulation()>0).ToList();

	}
	//Defence
	
	public int GetBomberCount() {
		return this._MissleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).Count();
	}
	public List<IWeapon> GetDefenceWeapon()
	{
        return this._MissleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Defence).ToList();
	}

	public IWeapon GetBomber() {

		return this._MissleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).FirstOrDefault();
	}
	public void RemoveBomber() {

		IWeapon bomberWeapon = _MissleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).FirstOrDefault();
		_MissleList.Remove(bomberWeapon);
	}


	public void RemoveDefenceWeapon()
	{

		IWeapon defenceWeapon = _MissleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Defence).FirstOrDefault();
		_MissleList.Remove(defenceWeapon);
	}


	public int GetMissleCount() {
		return _MissleList.Count;
	}
	public int GetRandomMissleSizeId(GlobalParam.TypeEvent TypeWeaponFly)
    {
        
        List<IWeapon> missleList = _MissleList.Where(a => a.GetTypeWeapon() == TypeWeaponFly).ToList();
        if (missleList.Count() == 0)
        {
			return 0;
        }
		
		int indexWeapon = UnityEngine.Random.Range(0, missleList.Count);
		return missleList[indexWeapon].GetSize();
	}


	public IWeapon GetMissle() {
		return _MissleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Missle).FirstOrDefault();
	}
	public void RemoveMissle() {
		
		if (this._MissleList.Count>0){
			this._MissleList.RemoveAt(0);
		}
		
	}
	public void AddMissle(List<IWeapon> missleList) {
		if (missleList != null)
		{
			_MissleList.AddRange(missleList);
		}
	}
	public GameObject GetCentralBuildingPropogation() {
		return PropagandaBuilding;
	}
    public void ResetTargetCity()
    {
        FiendLider = null;
        _targetCitySelectPlayer = null;
    }
    public void SetTargetCity(TargetCityModel targetCitySelectPlayer){
		FiendLider = targetCitySelectPlayer.EnemyLider;
        _targetCitySelectPlayer =targetCitySelectPlayer;
	}
	public TargetCityModel GetTargetCitySelectPlayer(){
		return _targetCitySelectPlayer;
	}


	public List<IWeapon> GetMissleList()
	{
		return _MissleList.Where(a => a.GetTypeWeapon() == GlobalParam.TypeEvent.Missle || a.GetTypeWeapon() == GlobalParam.TypeEvent.Bomber).ToList();
	}
}
