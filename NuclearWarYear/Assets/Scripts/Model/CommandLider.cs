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
	private Warhead _AttackWarhead;
	private string NameCommand;
	private CountryLider LiderFiend;
	List<string> _reportProducedWeaponList;

	public CommandLider() {
		this._MissleList = new List<IWeapon>();
	

		VisibleEventList = new Dictionary<string, bool>();
		VisibleEventList.Add("Prop",false);
        VisibleEventList.Add("Build", false);
        VisibleEventList.Add("Defence", false);
        VisibleEventList.Add("Missle", false);
        VisibleEventList.Add("Airport", false);
        VisibleEventList.Add("Bomber", false);
        VisibleEventList.Add("AttackBomber", false);
        VisibleEventList.Add("AttackAirport", false);
        VisibleEventList.Add("AttackMissle", false);
        VisibleEventList.Add("Defectors", false);
        VisibleEventList.Add("Ufo", false);
        VisibleEventList.Add("Baby", false);
        VisibleEventList.Add("RocketRich", false);
		VisibleEventList.Add("CrazyCow", false);
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
		VisibleEventList["Missle"] = visibleMissle;
		this.MissleId = MissleId;

	}
	public bool GetVisibleMissle()
    {

		return VisibleEventList["Missle"];
    }
	public void SetVisibleBomber(bool visibleBomber, int MissleId)
    {
        VisibleEventList["Bomber"]= visibleBomber;
        VisibleEventList["Airport"]= visibleBomber;
		this.MissleId = MissleId;
	}
	

	public bool GetVisibleBomber() {
        return VisibleEventList["Bomber"];

    }
	public void SetVisibleAttackBomber(bool visibleAttackBomber) {
		VisibleEventList["AttackBomber"] = visibleAttackBomber;
		VisibleEventList["AttackAirport"] = visibleAttackBomber;

    }
	public bool GetVisibleAttackBomber()
	{
		//return this.VisibleAttackBomber;
		return VisibleEventList["AttackBomber"];

    }
	public void SetNameCommand(string nameCommand)
	{
		this.NameCommand = nameCommand;
	}
	public string GetNameCommand()
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

        return VisibleEventList["Defence"];
    }
	//AddWarhead
	
	public Warhead GetAttackWarhead(){
		return _AttackWarhead;
	}
	public void SetAttackWarhead(Warhead AttackWarhead){
		_AttackWarhead = AttackWarhead;
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
	public CountryLider GetTargetLider(){
		
		return this.LiderFiend;
		
	}
	public IWeapon GetAttackMissle(){
		return this._AttackMissle;
	}
	public void SetAttackMissle(IWeapon AttackMissle){
		this._AttackMissle = AttackMissle;
	}
}
