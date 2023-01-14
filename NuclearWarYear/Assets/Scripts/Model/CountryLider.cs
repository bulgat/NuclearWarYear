using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
	
	private List<CityModel> _TownList;
	public int Mood =2;
	private int _maxPopulation;
	private CityModel _targetCitySelectPlayer;

	private string Name;
	private string EventTotalTurn;

	public int MissleId;

	public CountryLider(int flagId,bool player,Missle missle,Bomber bomber,
		GameObject PropagandaBuild,List<CityModel> TownList,string Name) {
		FlagId = flagId;
		Player = player;

		_MissleList = new List<Weapon>();
		_MissleList.Add(missle);


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
	
	//Defence
	
	public int GetBomberCount() {
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber).Count();
	}
	public int GetBomberSpecCount(int Id)
	{
		Debug.Log(Id + "   ----  Count =  " + _MissleList.Count() + "  = " + _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Missle && a.Size == Id).Count());
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber && a.Size == Id).Count();
	}


	public void AddDefenceWeapon(List<Weapon> DefenceList)
	{
		//_DefenceList.AddRange(DefenceList);
		_MissleList.AddRange(DefenceList);
	}
	public List<Weapon> GetDefenceWeapon()
	{
		//return this._DefenceList;
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Defence).ToList();
	}

	public Weapon GetBomber() {
		//if (_BomberList.Count>0){
		//	return _BomberList[0];
		//}
		//return null;
		return _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber).FirstOrDefault();
	}
	public void RemoveBomber() {
		//if(_BomberList.Count>0)
		//{
		//_BomberList.RemoveAt(0);
		//}
		Weapon bomberWeapon = _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Bomber).FirstOrDefault();
		_MissleList.Remove(bomberWeapon);
	}
	public void AddBomber(List<Weapon> bomberList) {
		//_BomberList.AddRange(bomberList);
		_MissleList.AddRange(bomberList);
	}

	public void RemoveDefenceWeapon()
	{
		//if (_DefenceList.Count > 0)
		//{
		//	_DefenceList.RemoveAt(0);
		//}
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
		Debug.Log(Id+ "   ----  Count =  " + _MissleList.Count()+"  = " + _MissleList.Where(a => a.Type == DictionaryMissle.TypeWeapon.Missle && a.Size == Id).Count());
		return _MissleList.Where(a=>a.Type== DictionaryMissle.TypeWeapon.Missle && a.Size == Id).Count();
	}

	public Weapon GetMissle() {
		//if (_MissleList.Count>0){
		//	return _MissleList[0];
		//}
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
