using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLider 
{

	public Dictionary<string, bool> VisibleEventList;

	private List<IWeapon> _MissleList;

	private CityModel _TargetCity;

	private int MissleId;

	private IWeapon _AttackMissle;
	private IWeapon _AttackBomber;
	private List<string> NameCommand;
	public CountryLider LiderFiend { get; private set; }
	List<string> _reportProducedWeaponList;

	public CommandLider(string nameCommand) {
		this._MissleList = new List<IWeapon>();
        this.NameCommand = new List<string> { nameCommand };
       // this.NameCommand = nameCommand;

        this.VisibleEventList = new Dictionary<string, bool>();
		VisibleEventList.Add(DictionaryEssence.TypeEvent.Propaganda.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Building.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Defence.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Missle.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Airport.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Bomber.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.AttackBomber.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.AttackAirport.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.AttackMissle.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Defectors.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Ufo.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.Baby.ToString(), false);
        VisibleEventList.Add(DictionaryEssence.TypeEvent.RocketRich.ToString(), false);
		VisibleEventList.Add(DictionaryEssence.TypeEvent.CrazyCow.ToString(), false);
	}
	public void SetVisibleEventList(string Key,bool Value)
	{
		VisibleEventList[Key] = Value;

    }
	public int GetSizeIdMissle()
    {
		return this.MissleId;

	}

	public void SetVisibleMissle(bool visibleMissle, int MissleId)
	{
		VisibleEventList[DictionaryEssence.TypeEvent.Missle.ToString()] = visibleMissle;
		this.MissleId = MissleId;

	}
	public bool GetVisibleMissle()
    {

		return VisibleEventList[DictionaryEssence.TypeEvent.Missle.ToString()];
    }
	public void SetVisibleBomber(bool visibleBomber, int MissleId)
    {
        VisibleEventList[DictionaryEssence.TypeEvent.Bomber.ToString()]= visibleBomber;
        VisibleEventList[DictionaryEssence.TypeEvent.Airport.ToString()]= visibleBomber;
		this.MissleId = MissleId;
	}
	

	public bool GetVisibleBomber() {
        return VisibleEventList[DictionaryEssence.TypeEvent.Bomber.ToString()];

    }
	public void SetVisibleAttackBomber(bool visibleAttackBomber) {
		VisibleEventList[DictionaryEssence.TypeEvent.AttackBomber.ToString()] = visibleAttackBomber;
		VisibleEventList[DictionaryEssence.TypeEvent.AttackAirport.ToString()] = visibleAttackBomber;

    }
	public bool GetVisibleAttackBomber()
	{
		return VisibleEventList[DictionaryEssence.TypeEvent.AttackBomber.ToString()];
    }

	public List<string> GetNameCommandList()
    {
		return this.NameCommand;
    }



	public void ResetCommand(){
		foreach(var item in VisibleEventList)
		{
			VisibleEventList[item.Key] = false;

        }
		this.MissleId = 0;
	}
	public bool GetDefence() {

        return VisibleEventList[DictionaryEssence.TypeEvent.Defence.ToString()];
    }

	public IWeapon GetAttackBomber(){
		return _AttackBomber;
	}
	public void SetAttackBomber(IWeapon AttackBomber){
		_AttackBomber = AttackBomber;
	}
	
	
	public List<IWeapon> GetMissle(){
		return this._MissleList;
	}
	public void AddMissle(List<IWeapon> MissleList){
		this._MissleList = MissleList;
	}
	public void AddReportProducedWeaponList(List<string> ReportProducedWeaponList)
    {
		_reportProducedWeaponList = ReportProducedWeaponList;

	}
	public List<string> GetReportProducedWeaponList()
    {
		return _reportProducedWeaponList;

	}
	public void SetTargetCity(CityModel TargetCity)
	{
		this._TargetCity = TargetCity;
	}
	public CityModel GetTargetCity(){

		return this._TargetCity;
	
	}
	public void SetTargetLider(CountryLider nameLiderFiend)
	{

		this.LiderFiend = nameLiderFiend;
	}
	/*
	public CountryLider GetTargetLider(){
        Debug.Log("0  ReconTotal   LiderFiend  = " + this.LiderFiend);
        return this.LiderFiend;
		
	}
	*/
	public IWeapon GetAttackMissle(){
		return this._AttackMissle;
	}
	public void SetAttackMissle(IWeapon AttackMissle){
		this._AttackMissle = AttackMissle;
	}
}
