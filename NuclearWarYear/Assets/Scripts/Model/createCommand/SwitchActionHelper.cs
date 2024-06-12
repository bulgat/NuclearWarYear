using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static SwitchActionHelper;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.Progress;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model;
using static Assets.Scripts.Model.param.GlobalParam;

public class SwitchActionHelper
{


    public List<CommandLider> SwitchAction(List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int FlagIdPlayer, CommandLider commandLider, CountryLider countryLider, int Year)
    {


        List<CommandLider> commandLiderList = new List<CommandLider>();

        //ResetAction();

        //CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        bool AIfiend = countryLider.FlagId != FlagIdPlayer;

        //CommandLider commandLider = new CommandLider(actionCommand, Year);
        CountryLider fiendLider1 = new BuildingCentralHelper().GetFiendLider(CountryLiderList, countryLider.FlagId);

        TargetCityModel targetCityModel
            = new TargetCityModel(new TargetHelper().GetTargetRandom(CountryLiderList, FlagIdPlayer, AIfiend, TownList, countryLider, fiendLider1), fiendLider1);

        //Change Ai Command
        if (AIfiend)
        {
            if (commandLider.GetNameCommandFirst() == GlobalParam.TypeEvent.Defence)
            {
                if (countryLider.GetDefenceWeapon().Count() <= 0)
                {
                    commandLider = new CommandLider(GlobalParam.TypeEvent.Propaganda, Year);
                    //commandLider.Se = GlobalParam.TypeEvent.Propaganda;
                }
            }

            AiAddTargetCity(targetCityModel, commandLider, fiendLider1);
        }
        else
        {
            //add auto target city
            if (commandLider._TargetCity?.TargetCity == null)
            {
                AiAddTargetCity(targetCityModel, commandLider, fiendLider1);
            }
        }



        // Счастливая карта!
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(targetCityModel, countryLider.FlagId, AIfiend, TownList, CountryLiderList, countryLider, Year);


        this.TreatmentCommand(commandLider.GetNameCommandFirst(), commandLider, targetCityModel, countryLider.FlagId, AIfiend, TownList,
        CountryLiderList, countryLider, fiendLider1);

        if (countryLider.GetTargetCitySelectPlayer() != null)
        {
            commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
        }


        commandLider.SetTargetLider(CountryLiderList.Where(a => a.FlagId == targetCityModel.TargetCity.FlagId).FirstOrDefault());

        commandLiderList.Add(commandLider);
        if (commandLiderFortune != null)
        {

            commandLiderList.Add(commandLiderFortune);
        }
        Debug.Log("    ---- ---- ------------ -- -------- Name   =" + commandLiderList.First().GetNameCommandFirst());
        return commandLiderList;
    }
    private void AiAddTargetCity(TargetCityModel targetCityModel, CommandLider commandLider, CountryLider enemyLider)
    {
        commandLider.SetTargetCity(targetCityModel);
        commandLider.SetTargetLider(enemyLider);
    }
    private void TreatmentCommand(GlobalParam.TypeEvent actionCommand, CommandLider commandLider,
        TargetCityModel targetCityModel, int FlagId, bool AIfiend, List<CityModel> TownList,
           List<CountryLider> CountryLiderList, CountryLider countryLider, CountryLider countryLiderFiend)
    {
        switch (actionCommand)
        {
            case GlobalParam.TypeEvent.Propaganda:
                commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                targetCityModel.TargetCity = new ModGameEngine().GetCityRandomFlagId(TownList, countryLiderFiend, FlagId, AIfiend);
                break;
            case GlobalParam.TypeEvent.Build:
                commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Build, true);
                BuildWeapon buildWeapon = new BuildWeapon();
                commandLider.AddMissle(buildWeapon.AddLiderBuildWeaponSwithAction());
                commandLider.AddReportProducedWeaponList(buildWeapon.GetReportProducedWeaponList());
                break;
            case GlobalParam.TypeEvent.Defence:
                commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Defence, true);
                break;
            case GlobalParam.TypeEvent.Missle:
                commandLider.SetVisibleMissle(true);
                break;
            case GlobalParam.TypeEvent.Bomber:
                commandLider.SetVisibleBomber(true);
                break;
            case GlobalParam.TypeEvent.AttackBomber:
                if (targetCityModel == null)
                {
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                }
                else
                {
                    commandLider.SetVisibleAttackBomber(true);
                    commandLider.SetTargetCity(targetCityModel);
                    commandLider.SetTargetLider(targetCityModel.EnemyLider);
                    commandLider.SetAttackBomber(countryLider.GetBomber());
                }
                break;
            case GlobalParam.TypeEvent.AttackMissle:
                if (targetCityModel == null)
                {
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                }
                else
                {
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.AttackMissle, true);
                    commandLider.SetTargetCity(targetCityModel);
                    commandLider.SetTargetLider(targetCityModel.EnemyLider);
                    commandLider.SetAttackMissle(countryLider.GetMissle());
                }
                break;
            default:
                //print ("Incorrect intelligence level.");
                break;
        }
    }

}
