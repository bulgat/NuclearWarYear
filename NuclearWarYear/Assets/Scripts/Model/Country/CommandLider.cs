using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandLider 
{

	//public Dictionary<string, bool> VisibleEventList;

	private List<IWeapon> _MissleList;

	private CityModel _TargetCity;

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
		//VisibleEventList[Key] = Value;
		this.VisibleList.Add(Key);
        //return this.NameCommand == SwitchActionHelper.ActionCommand.Defence.ToString();
    }
	public int GetSizeIdMissle()
    {
		return this.MissleId;

	}

	public void SetVisibleMissle(bool visibleMissle, int MissleId)
	{
        this.VisibleList.Add("Missle");
        //VisibleEventList[DictionaryEssence.TypeEvent.Missle.ToString()] = visibleMissle;
		this.MissleId = MissleId;

	}
	public bool GetVisibleMissle()
    {
        return this.IncidentCommand?.Name == SwitchActionHelper.ActionCommand.Missle.ToString();
        //return VisibleEventList[DictionaryEssence.TypeEvent.Missle.ToString()];
    }
	public void SetVisibleBomber(bool visibleBomber, int MissleId)
    {
        this.VisibleList.Add("Bomber");
        this.VisibleList.Add("Airport");
        //VisibleEventList[DictionaryEssence.TypeEvent.Bomber.ToString()]= visibleBomber;
        //VisibleEventList[DictionaryEssence.TypeEvent.Airport.ToString()]= visibleBomber;
		this.MissleId = MissleId;
	}
	

	public bool GetVisibleBomber() {
        
        return this.IncidentCommand?.Name == SwitchActionHelper.ActionCommand.Bomber.ToString();
        //return VisibleEventList[DictionaryEssence.TypeEvent.Bomber.ToString()];

    }
	public void SetVisibleAttackBomber(bool visibleAttackBomber) {
        this.VisibleList.Add("AttackBomber");
        this.VisibleList.Add("AttackAirport");
        //VisibleEventList[DictionaryEssence.TypeEvent.AttackBomber.ToString()] = visibleAttackBomber;
		//VisibleEventList[DictionaryEssence.TypeEvent.AttackAirport.ToString()] = visibleAttackBomber;

    }
	public bool GetVisibleAttackBomber()
	{
        return this.IncidentCommand?.Name == SwitchActionHelper.ActionCommand.AttackBomber.ToString();
        //return VisibleEventList[DictionaryEssence.TypeEvent.AttackBomber.ToString()];
    }

	public string GetNameCommandFirst()
    {
		return IncidentCommand?.Name;
    }

	public bool GetDefence() {

        return this.IncidentCommand.Name == SwitchActionHelper.ActionCommand.Defence.ToString();
        //return VisibleEventList[DictionaryEssence.TypeEvent.Defence.ToString()];
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
	
	public IWeapon GetAttackMissle(){
		return this._AttackMissle;
	}
	public void SetAttackMissle(IWeapon AttackMissle){
		this._AttackMissle = AttackMissle;
	}
}
