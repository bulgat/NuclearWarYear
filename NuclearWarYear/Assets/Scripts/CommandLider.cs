using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLider 
{
	public bool VisibleProp;
	public bool  VisibleBuild;
	public bool  VisibleDefence;
	public bool VisibleMissle;
	public bool VisibleBomber;
	public bool VisibleAttackBomber;
	public bool VisibleAttackMissle;
	private List<Missle> _MissleList;
	private List<Bomber> _BomberList;
	private List<Warhead> _WarheadList;
	private CityModel _TargetCity;
	
	private Missle _AttackMissle;
	private Bomber _AttackBomber;
	private Warhead _AttackWarhead;
	private string NameCommand;

	public CommandLider() {
		_MissleList = new List<Missle>();
		_BomberList = new List<Bomber>();
		_WarheadList = new List<Warhead>();
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
		VisibleProp=false;
		VisibleBuild=false;
		VisibleDefence=false;
		VisibleMissle=false;
		VisibleBomber=false;
		VisibleAttackBomber=false;
		VisibleAttackMissle=false;
	}
public bool GetDefence() {
	//return true;
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
	public CityModel GetTargetCity(){

			return _TargetCity;
	
	}

	public void SetTargetCity(CityModel TargetCity){
		
			_TargetCity=TargetCity;
		
	}
	public Missle GetAttackMissle(){
		return _AttackMissle;
	}
	public void SetAttackMissle(Missle AttackMissle){
		_AttackMissle = AttackMissle;
	}
}
