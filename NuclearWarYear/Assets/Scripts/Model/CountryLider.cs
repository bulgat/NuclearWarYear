using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [System.Serializable]
public class CountryLider 
{
    public int FlagId;
	[SerializeField]
	public bool Player;
	
	public GameObject PropagandaBuilding;
	private CommandLider _CommandLider;
	private bool _dead;
	private List<Missle> _MissleList;
	private List<Bomber> _BomberList;
	private List<Warhead> _WarheadList;
	private List<Defence> _DefenceList;
	public int FlagIdAttack=1;
	
	private List<CityModel> _TownList;
	public int Mood =2;
	private int _maxPopulation;
	private CityModel _targetCitySelectPlayer;
	//private CityModel TargetCitySelectAi;
	private string Name;
	private string EventTotalTurn;
	
	public CountryLider(int flagId,bool player,Missle missle,Bomber bomber,Warhead warhead,
		GameObject PropagandaBuild,List<CityModel> TownList,string Name) {
		FlagId = flagId;
		Player = player;
		_BomberList = new List<Bomber>();
		_BomberList.Add(bomber);
		_MissleList = new List<Missle>();
		_MissleList.Add(missle);
		_WarheadList = new List<Warhead>();
		_WarheadList.Add(warhead);
		_DefenceList = new List<Defence>();
		_DefenceList.Add(new DictionaryMissle().GetDefenceWeapon());

		PropagandaBuilding = PropagandaBuild;
		this.Name = Name;

		_TownList = new List<CityModel>();
		foreach(CityModel TownCity in TownList){
			
			if(flagId== TownCity.FlagId){
				_TownList.Add(TownCity);
				
			}
		}
		_maxPopulation=GetAllOwnPopulation();
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
	public void ChangeTurn()
	{
		float percent = GetAllOwnPopulation() / _maxPopulation;
		if (percent > 0.9)
		{
			Mood = 2;
		}
		if (percent < 0.5)
		{
			Mood = 5;
		}
		if (percent < 0.2)
		{
			Mood = 7;
		}
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
		foreach(CityModel TownCity in _TownList){
			maxPopulation+=TownCity.GetPopulation();
		}
		return maxPopulation;
	}
	public int GetWarheadCount() {
		return _WarheadList.Count;
	}
	public Warhead GetWarhead() {
		if (_WarheadList.Count>0){
			return _WarheadList[0];
		}
		return null;
	}
	public void RemoveWarhead() {
		_WarheadList.RemoveAt(0);
	}
	public void AddWarhead(List<Warhead> warheadList) {
		_WarheadList.AddRange(warheadList);
	}
	//Defence
	
	public int GetBomberCount() {
		return _BomberList.Count;
	}



	public void AddDefenceWeapon(List<Defence> DefenceList)
	{
		_DefenceList.AddRange(DefenceList);
	}
	public List<Defence> GetDefenceWeapon()
	{
		return this._DefenceList;
	}

	public Bomber GetBomber() {
		if (_BomberList.Count>0){
			return _BomberList[0];
		}
		return null;
	}
	public void RemoveBomber() {
		if(_BomberList.Count>0)
		{
		_BomberList.RemoveAt(0);
		}
	}
	public void AddBomber(List<Bomber> bomberList) {
		_BomberList.AddRange(bomberList);
	}

	public void RemoveDefenceWeapon()
	{
		if (_DefenceList.Count > 0)
		{
			_DefenceList.RemoveAt(0);
		}
	}


	public int GetMissleCount() {
		return _MissleList.Count;
	}
	public Missle GetMissle() {
		if (_MissleList.Count>0){
			return _MissleList[0];
		}
		return null;
	}
	public void RemoveMissle() {
		if (_MissleList.Count>0){
			_MissleList.RemoveAt(0);
		}
	}
	public void AddMissle(List<Missle> missleList) {
		_MissleList.AddRange(missleList);
	}
	public GameObject GetCentralBuildingPropogation() {
		return PropagandaBuilding;
	}
	public void SetTargetCitySelectPlayer(CityModel targetCitySelectPlayer){
		Debug.Log("0005  ["+ targetCitySelectPlayer + "] BomberObje  ");
		_targetCitySelectPlayer =targetCitySelectPlayer;
	}
	public CityModel GetTargetCitySelectPlayer(){
		return _targetCitySelectPlayer;
	}
	
}
