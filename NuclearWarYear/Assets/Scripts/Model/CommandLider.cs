using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLider 
{
	public bool VisibleProp;
	public bool  VisibleBuild;
	public bool  VisibleDefence;
	private bool VisibleMissle;
	public bool VisibleAirport;
	private bool VisibleBomber;
	private bool VisibleAttackBomber;
	public bool VisibleAttackAirport;
	public bool VisibleAttackMissle;
	private List<Missle> _MissleList;
	private List<Bomber> _BomberList;
	private List<Warhead> _WarheadList;
	private List<Defence> _DefenceList;
	private CityModel _TargetCity;
	
	private Missle _AttackMissle;
	private Bomber _AttackBomber;
	private Warhead _AttackWarhead;
	private string NameCommand;
	private string NameLiderFiend;

	public CommandLider() {
		_MissleList = new List<Missle>();
		_BomberList = new List<Bomber>();
		_WarheadList = new List<Warhead>();
		_DefenceList = new List<Defence>();
	}
	public bool SetVisibleMissle(bool visibleMissle)
	{
		return this.VisibleMissle= visibleMissle;
	}
	public bool GetVisibleMissle()
    {
		return this.VisibleMissle;
    }
	public void SetVisibleBomber(bool visibleBomber)
    {
		this.VisibleBomber = visibleBomber;
		this.VisibleAirport = visibleBomber;

	}
	

	public bool GetVisibleBomber() {
		return this.VisibleBomber;
	}
	public void SetVisibleAttackBomber(bool visibleAttackBomber) {
		this.VisibleAttackBomber = visibleAttackBomber;
		this.VisibleAttackAirport = visibleAttackBomber;
	}
	public bool GetVisibleAttackBomber()
	{
		return this.VisibleAttackBomber;
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
		this.VisibleProp=false;
		this.VisibleBuild =false;
		this.VisibleDefence =false;
		this.VisibleMissle =false;
		this.VisibleAirport = false;
		this.VisibleBomber =false;
		this.VisibleAttackBomber =false;
		this.VisibleAttackAirport = false;
		this.VisibleAttackMissle =false;
	}
	public bool GetDefence() {

		return VisibleDefence;
	}
	//AddWarhead
	public List<Warhead> GetWarhead(){
		return _WarheadList;
	}

	public void AddWarhead(List<Warhead> WarheadList){
		_WarheadList = WarheadList;
	}
	public Warhead GetAttackWarhead(){
		return _AttackWarhead;
	}
	public void SetAttackWarhead(Warhead AttackWarhead){
		_AttackWarhead = AttackWarhead;
	}

	public List<Defence> GetDefenceWeapon()
	{
		return this._DefenceList;
	}
	public void AddDefenceWeapon(List<Defence> DefenceWeapon)
	{
		this._DefenceList = DefenceWeapon;
	}
	public List<Bomber> GetBomber(){
		return _BomberList;
	}
	public void AddBomber(List<Bomber> BomberList){
		_BomberList = BomberList;
	}
	public Bomber GetAttackBomber(){
		return _AttackBomber;
	}
	public void SetAttackBomber(Bomber AttackBomber){
		_AttackBomber = AttackBomber;
	}
	
	
	public List<Missle> GetMissle(){
		return _MissleList;
	}
	public void AddMissle(List<Missle> MissleList){
		_MissleList = MissleList;
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
	public Missle GetAttackMissle(){
		return _AttackMissle;
	}
	public void SetAttackMissle(Missle AttackMissle){
		_AttackMissle = AttackMissle;
	}
}
