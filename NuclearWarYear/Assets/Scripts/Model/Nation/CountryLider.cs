using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Model.scenario;

 [System.Serializable]
public class CountryLider 
{
    public int FlagId;
	[SerializeField]
	public bool Player;
	
	public GameObject PropagandaBuilding;
	private CommandLider _CommandLider;
	private bool _dead;
	private List<Weapon> _MissleList;

	public int FlagIdAttack=1;
	
	private List<CityModel> _TownListOwn;
	//public int Mood =2;
	private int _maxPopulation;
	private CityModel _targetCitySelectPlayer;

	private string Name;
	private string EventTotalTurn;

	public int MissleId;
	public RelationShip _RelationShip;
	public int GraphicId { get; }
	public bool MoveMade { private set; get; }
	public CountryLider(bool player,Missle missle,Bomber bomber,
		GameObject PropagandaBuild,List<CityModel> TownList, ScenarioLider scenarioLider,int CountryId) {
		this.FlagId = scenarioLider.FlagId;
		this.Player = player;

		_MissleList = new List<Weapon>();
		_MissleList.Add(missle);


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
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber).Count();
	}
	public int GetBomberSpecCount(int Id)
	{
		 
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber && a.Size == Id).Count();
	}


	public void AddDefenceWeapon(List<Weapon> DefenceList)
	{
		_MissleList.AddRange(DefenceList);
	}
	public List<Weapon> GetDefenceWeapon()
	{
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Defence).ToList();
	}

	public Weapon GetBomber() {

		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber).FirstOrDefault();
	}
	public void RemoveBomber() {

		Weapon bomberWeapon = _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber).FirstOrDefault();
		_MissleList.Remove(bomberWeapon);
	}
	public void AddBomber(List<Weapon> bomberList) {
		_MissleList.AddRange(bomberList);
	}

	public void RemoveDefenceWeapon()
	{

		Weapon defenceWeapon = _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Defence).FirstOrDefault();
		_MissleList.Remove(defenceWeapon);
	}


	public int GetMissleCount() {
		return _MissleList.Count;
	}
	public int GetRandomMissleSizeId(DictionaryMissle.TypeWeapon TypeWeaponFly)
    {
		List<Weapon> missleList = _MissleList.Where(a => a.Type == TypeWeaponFly).ToList();
        if (missleList.Count() == 0)
        {
			return 0;
        }
		
		int indexWeapon = UnityEngine.Random.Range(0, missleList.Count);
		return missleList[indexWeapon].Size;
	}
	public int GetMissleSpecCount(int Id)
	{
		
		return _MissleList.Where(a=>a.Type== DictionaryMissle.TypeWeapon.Missle && a.Size == Id).Count();
	}

	public Weapon GetMissle() {
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Missle).FirstOrDefault();
	}
	public void RemoveMissle() {
		if (_MissleList.Count>0){
			_MissleList.RemoveAt(0);
		}
	}
	public void AddMissle(List<Weapon> missleList) {
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
}
