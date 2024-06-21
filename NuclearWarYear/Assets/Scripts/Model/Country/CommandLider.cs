using Assets.Scripts.Model;
using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Model.param.GlobalParam;

public class CommandLider
{

    private List<IWeapon> _MissleList;

    public TargetCityModel _TargetCity { private set; get; }

    //private int MissleId;

    private IWeapon _AttackMissle;
    private IWeapon _AttackBomber;
    private Incident IncidentCommand;
    public CountryLider LiderFiend { get; private set; }
    public List<string> _reportProducedWeaponList { get; private set; }
    List<GlobalParam.TypeEvent> VisibleList;
    //public int Year {  get; private set; }
    public CommandLider(GlobalParam.TypeEvent nameCommand,int Year)
    {
        this._MissleList = new List<IWeapon>();
        this.IncidentCommand = new DictionaryEssence().BuildIncident(nameCommand, Year);
        
        this.VisibleList = new List<GlobalParam.TypeEvent>();
    }
    public bool GetNameExecute(GlobalParam.TypeEvent Name)
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

    public void SetVisibleEventList(GlobalParam.TypeEvent Key, bool Value)
    {
        this.VisibleList.Add(Key);
    }
    /*
    public int GetSizeIdMissle()
    {
        return this.MissleId;

    }
    */
    public void SetVisibleMissle(bool visibleMissle)
    {
        this.VisibleList.Add(GlobalParam.TypeEvent.Missle);
        //this.MissleId = MissleId;

    }
    public bool GetVisibleMissle()
    {
        return this.IncidentCommand.Name == GlobalParam.TypeEvent.Missle;
    }
    public void SetVisibleBomber(bool visibleBomber)
    {
        this.VisibleList.Add(GlobalParam.TypeEvent.Bomber);
        this.VisibleList.Add(GlobalParam.TypeEvent.Airport);
        //this.MissleId = MissleId;
    }


    public bool GetVisibleBomber()
    {

        return this.IncidentCommand?.Name == GlobalParam.TypeEvent.Bomber;
    }
    public void SetVisibleAttackBomber(bool visibleAttackBomber)
    {
        this.VisibleList.Add(GlobalParam.TypeEvent.AttackBomber);
        this.VisibleList.Add(GlobalParam.TypeEvent.AttackAirport);
    }
    public bool GetVisibleAttackBomber()
    {
        return this.IncidentCommand?.Name == GlobalParam.TypeEvent.AttackBomber;
    }

    public GlobalParam.TypeEvent GetNameCommandFirst()
    {
        return IncidentCommand.Name;
    }

    public bool GetDefence()
    {

        return this.IncidentCommand.Name == GlobalParam.TypeEvent.Defence;
    }

    public IWeapon GetAttackBomber()
    {
        return _AttackBomber;
    }
    public void SetAttackBomber(IWeapon AttackBomber)
    {
        _AttackBomber = AttackBomber;
    }


    public List<IWeapon> GetMissle()
    {
        return this._MissleList;
    }
    public void AddMissle(List<IWeapon> MissleList)
    {
        this._MissleList = MissleList;
    }
    public void AddReportProducedWeaponList(List<string> ReportProducedWeaponList)
    {
        _reportProducedWeaponList = ReportProducedWeaponList;

    }

    public void SetTargetCity(TargetCityModel TargetCity)
    {
        //if (TargetCity == null)
        //{
        //    throw new System.Exception("TargetCity");
        //}
        this._TargetCity = TargetCity;
    }



    public void SetTargetLider(CountryLider nameLiderFiend)
    {

        this.LiderFiend = nameLiderFiend;
    }

    public IWeapon GetAttackMissle()
    {
        return this._AttackMissle;
    }
    public void SetAttackMissle(IWeapon AttackMissle)
    {
        this._AttackMissle = AttackMissle;
    }
}
