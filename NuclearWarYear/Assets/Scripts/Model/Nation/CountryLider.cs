using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Model.scenario;
using Assets.Scripts.Model;

[System.Serializable]
public class CountryLider 
{
    public int FlagId;
	[SerializeField]
	public bool Player;
	
	public GameObject PropagandaBuilding;
	private CommandLider _CommandLider;
	private bool _dead;
	private List<IWeapon> _MissleList;

	public int FlagIdAttack=1;
	
	private List<CityModel> _TownListOwn;

	private int _maxPopulation;
	private CityModel _targetCitySelectPlayer;

	private string Name;
	private string EventTotalTurn;

	public int MissleId;
	public RelationShip _RelationShip;
	public int GraphicId { get; }
	public bool MoveMade { private set; get; }
	public CountryLider(bool player,List<IWeapon> missleList,
		GameObject PropagandaBuild,List<CityModel> TownList, ScenarioLider scenarioLider,int CountryId) {
		this.FlagId = scenarioLider.FlagId;
		this.Player = player;

		//_MissleList = new List<IWeapon>();
		this._MissleList=missleList;


		PropagandaBuilding = PropagandaBuild;
		this.Name = scenarioLider.Name;
		this.GraphicId = scenarioLider.GraphicId;

        _TownListOwn = new List<CityModel>();
		foreach(CityModel TownCity in TownList){
			
			if(CountryId == TownCity.CountryId){
				TownCity.FlagId = scenarioLider.FlagId;
                _TownListOwn.Add(TownCity);
				
			}
		}
		_maxPopulation=GetAllOwnPopulation();
		_RelationShip = new RelationShip();
	}
	public void DoneMoveMade(bool Value)
    {
		MoveMade = Value;

	}
	public void SetEventTotalTurn(string eventTotalTurn)
    {
		this.EventTotalTurn = eventTotalTurn;

	}
	public string GetEventTotalTurn() {
		return this.EventTotalTurn;
	}
	public string GetName()
	{
		return this.Name;

	}
	
	public int GetMood(int FlagId)
    {
		return _RelationShip.GetMood(FlagId);
    }
	public void SetDead()
	{
		this._dead = true;
	}
	public bool GetDead()
	{
		return this._dead;
	}
	public CommandLider GetCommandLider()
	{
		return this._CommandLider;
	}
	public void SetCommandLider(CommandLider commandLider)
	{
		this._CommandLider = commandLider;
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
		return _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Bomber).Count();
	}
	public int GetBomberSpecCount(int Id)
	{
		 
		return _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Bomber && a.GetSize() == Id).Count();
	}


	public void AddDefenceWeapon(List<IWeapon> DefenceList)
	{
		_MissleList.AddRange(DefenceList);
	}
	public List<IWeapon> GetDefenceWeapon()
	{
		return _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Defence).ToList();
	}

	public IWeapon GetBomber() {

		return _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Bomber).FirstOrDefault();
	}
	public void RemoveBomber() {

		IWeapon bomberWeapon = _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Bomber).FirstOrDefault();
		_MissleList.Remove(bomberWeapon);
	}
	public void AddBomber(List<IWeapon> bomberList) {
		_MissleList.AddRange(bomberList);
	}

	public void RemoveDefenceWeapon()
	{

		IWeapon defenceWeapon = _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Defence).FirstOrDefault();
		_MissleList.Remove(defenceWeapon);
	}


	public int GetMissleCount() {
		return _MissleList.Count;
	}
	public int GetRandomMissleSizeId(DictionaryMissle.TypeWeapon TypeWeaponFly)
    {
		List<IWeapon> missleList = _MissleList.Where(a => a.GetTypeWeapon() == TypeWeaponFly).ToList();
        if (missleList.Count() == 0)
        {
			return 0;
        }
		
		int indexWeapon = UnityEngine.Random.Range(0, missleList.Count);
		return missleList[indexWeapon].GetSize();
	}
	public int GetMissleSpecCount(int Id)
	{
		
		return _MissleList.Where(a=>a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Missle && a.GetSize() == Id).Count();
	}

	public IWeapon GetMissle() {
		return _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Missle).FirstOrDefault();
	}
	public void RemoveMissle() {
		Debug.Log("RemoveMissle = sently =  ="+ this._MissleList.Count);
		if (this._MissleList.Count>0){
			this._MissleList.RemoveAt(0);
		}
		Debug.Log("====FLAG "+this.FlagId+"    ==" + this._MissleList.Count);
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
	public void SetTargetCitySelectPlayer(CityModel targetCitySelectPlayer){
		
		_targetCitySelectPlayer =targetCitySelectPlayer;
	}
	public CityModel GetTargetCitySelectPlayer(){
		return _targetCitySelectPlayer;
	}
	public void SetMissleSideId(int MissleId) {
		this.MissleId = MissleId;
	}
	public int GetMissleSideId()
	{
		return this.MissleId ;
	}
	public List<IWeapon> GetMissleList()
	{
		return _MissleList.Where(a => a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Missle || a.GetTypeWeapon() == DictionaryMissle.TypeWeapon.Bomber).ToList();
	}
}
