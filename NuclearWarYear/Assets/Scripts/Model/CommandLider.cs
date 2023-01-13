using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLider 
{

	public Dictionary<string, bool> VisibleEventList;

	private List<Weapon> _MissleList;
	//private List<Bomber> _BomberList;
	//private List<Warhead> _WarheadList;
	//private List<Defence> _DefenceList;
	private CityModel _TargetCity;
	
	private Weapon _AttackMissle;
	private Weapon _AttackBomber;
	private Warhead _AttackWarhead;
	private string NameCommand;
	private string NameLiderFiend;
	List<string> _reportProducedWeaponList;

	public CommandLider() {
		_MissleList = new List<Weapon>();
		//_BomberList = new List<Bomber>();
		//_WarheadList = new List<Warhead>();
		//_DefenceList = new List<Defence>();

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
    public bool SetVisibleMissle(bool visibleMissle)
	{

		return VisibleEventList["Missle"];

    }
	public bool GetVisibleMissle()
    {

		return VisibleEventList["Missle"];
    }
	public void SetVisibleBomber(bool visibleBomber)
    {
        VisibleEventList["Bomber"]= visibleBomber;
        VisibleEventList["Airport"]= visibleBomber;
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

	}
	public bool GetDefence() {

		//return VisibleDefence;
        return VisibleEventList["Defence"];
    }
	//AddWarhead
	//public List<Warhead> GetWarhead(){
		//return _WarheadList;
	//}

	//public void AddWarhead(List<Warhead> WarheadList){
		//_WarheadList = WarheadList;
	//}
	public Warhead GetAttackWarhead(){
		return _AttackWarhead;
	}
	public void SetAttackWarhead(Warhead AttackWarhead){
		_AttackWarhead = AttackWarhead;
	}

	//public List<Defence> GetDefenceWeapon()
	//{
		//return this._DefenceList;
	//}
	//public void AddDefenceWeapon(List<Defence> DefenceWeapon)
	//{
		//this._DefenceList = DefenceWeapon;
	//}
	//public List<Bomber> GetBomber(){
		//return _BomberList;
	//}
	//public void AddBomber(List<Bomber> BomberList){
	//	_BomberList = BomberList;
	//}
	public Weapon GetAttackBomber(){
		return _AttackBomber;
	}
	public void SetAttackBomber(Weapon AttackBomber){
		_AttackBomber = AttackBomber;
	}
	
	
	public List<Weapon> GetMissle(){
		return _MissleList;
	}
	public void AddMissle(List<Weapon> MissleList){
		_MissleList = MissleList;
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
	public void SetTargetNameLider(string nameLiderFiend)
	{
		this.NameLiderFiend = nameLiderFiend;
	}
	public string GetTargetNameLider(){
		
		return this.NameLiderFiend;
		
	}
	public Weapon GetAttackMissle(){
		return _AttackMissle;
	}
	public void SetAttackMissle(Weapon AttackMissle){
		_AttackMissle = AttackMissle;
	}
}
