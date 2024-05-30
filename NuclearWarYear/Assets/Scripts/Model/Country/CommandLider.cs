using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandLider 
{

	private List<IWeapon> _MissleList;

	private TargetCityModel _TargetCity;

	private int MissleId;

	private IWeapon _AttackMissle;
	private IWeapon _AttackBomber;
	private Incident IncidentCommand;
	public CountryLider LiderFiend { get; private set; }
	List<string> _reportProducedWeaponList;
    List<string> VisibleList;
	
    public CommandLider(string nameCommand) {
		this._MissleList = new List<IWeapon>();
        this.IncidentCommand = new DictionaryEssence ().BuildIncident( nameCommand);
        
        this.VisibleList = new List<string>();
        // this.NameCommand = nameCommand;
       
    }
	public bool GetNameExecute(string Name)
	{
		if (this.IncidentCommand?.Name == Name)
		{
			return true;
		}
		if (this.VisibleList.Contains(Name))
		{
			return true;
		}

		return false;
	}
    public Incident GetIncident()
	{
		return this.IncidentCommand;

    }

    public void SetVisibleEventList(string Key,bool Value)
	{
		this.VisibleList.Add(Key);
    }
	public int GetSizeIdMissle()
    {
		return this.MissleId;

	}

	public void SetVisibleMissle(bool visibleMissle, int MissleId)
	{
        this.VisibleList.Add("Missle");
		this.MissleId = MissleId;

	}
	public bool GetVisibleMissle()
    {
        return this.IncidentCommand?.Name == GlobalParam.ActionCommand.Missle.ToString();
    }
	public void SetVisibleBomber(bool visibleBomber, int MissleId)
    {
        this.VisibleList.Add("Bomber");
        this.VisibleList.Add("Airport");
		this.MissleId = MissleId;
	}
	

	public bool GetVisibleBomber() {
        
        return this.IncidentCommand?.Name == GlobalParam.ActionCommand.Bomber.ToString();
    }
	public void SetVisibleAttackBomber(bool visibleAttackBomber) {
        this.VisibleList.Add("AttackBomber");
        this.VisibleList.Add("AttackAirport");
    }
	public bool GetVisibleAttackBomber()
	{
        return this.IncidentCommand?.Name == GlobalParam.ActionCommand.AttackBomber.ToString();
    }

	public string GetNameCommandFirst()
    {
		return IncidentCommand?.Name;
    }

	public bool GetDefence() {

        return this.IncidentCommand.Name == GlobalParam.ActionCommand.Defence.ToString();
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
	public void SetTargetCity(TargetCityModel TargetCity)
	{
		if (TargetCity == null)
		{
			throw new System.Exception("TargetCity");
		}
		this._TargetCity = TargetCity;
	}


    public TargetCityModel GetTargetCity(){

		return this._TargetCity;
	
	}
	public void SetTargetLider(CountryLider nameLiderFiend)
	{

		this.LiderFiend = nameLiderFiend;
	}
	
	public IWeapon GetAttackMissle(){
		return this._AttackMissle;
	}
	public void SetAttackMissle(IWeapon AttackMissle){
		this._AttackMissle = AttackMissle;
	}
}
