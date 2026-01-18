using Assets.Scripts.Model;
using Assets.Scripts.Model.AiTurn;
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

    private IWeapon _AttackMissle;
    private IWeapon _AttackBomber;
    public Incident IncidentCommand { private set; get; }
    public CountryLider LiderFiend { get; private set; }
    public List<string> _reportProducedWeaponList { get; private set; }
    List<GlobalParam.TypeEvent> VisibleList;
    public int LiderId;
    public CommandLider(
        GlobalParam.TypeEvent nameCommand,
        CountryLider liderFiend,
        int Year,
        TargetCityModel TargetCity,
        int LiderId,
        Incident secondIncident = null
        )
    {
        this._MissleList = new List<IWeapon>();
        this.IncidentCommand = new DictionaryEssence().BuildIncident(nameCommand, Year);
        this.IncidentCommand.SetSecondIncident(secondIncident);

        this.VisibleList = new List<GlobalParam.TypeEvent>();
        this.LiderFiend = liderFiend;
        _TargetCity = TargetCity;
        this.LiderId = LiderId;
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


    public void SetVisibleEventList(GlobalParam.TypeEvent Key, bool Value)
    {
        this.VisibleList.Add(Key);
    }

    public void SetVisibleMissle(bool visibleMissle)
    {
        this.VisibleList.Add(GlobalParam.TypeEvent.Missle);
    }
    public bool GetVisibleMissle()
    {
        return new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupMissleList ,IncidentCommand);
        //return this.IncidentCommand.Name == GlobalParam.TypeEvent.Missle;
    }
    public void SetVisibleBomber(bool visibleBomber)
    {
        this.VisibleList.Add(GlobalParam.TypeEvent.Bomber);
        this.VisibleList.Add(GlobalParam.TypeEvent.Airport);
    }

    public bool GetVisibleBomber()
    {
        return new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupBomberList, IncidentCommand);
        //return this.IncidentCommand?.Name == GlobalParam.TypeEvent.Bomber;
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
    public bool GetVisibleAttackRocket()
    {
        return this.IncidentCommand?.Name == GlobalParam.TypeEvent.AttackMissle;
    }

    public GlobalParam.TypeEvent GetNameCommand()
    {
        return IncidentCommand.Name;
    }

    public bool GetDefence()
    {
        return new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupDefenceList, this.IncidentCommand);
        //return this.IncidentCommand.Name == GlobalParam.TypeEvent.Defence;
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

    public void SetTargetLider(CountryLider nameLiderFiend)
    {

        this.LiderFiend = nameLiderFiend;
    }

    public void SetAttackMissle(IWeapon AttackMissle)
    {
        this._AttackMissle = AttackMissle;
    }
}
