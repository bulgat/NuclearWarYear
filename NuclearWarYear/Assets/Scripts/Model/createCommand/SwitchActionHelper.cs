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
        List<CityModel> TownList, int FlagIdPlayer, CommandLider commandLider,
        CountryLider countryLider, int Year, CountryLider fiendLider1,
        //TargetCityModel targetCityModel, 
        CommandLider commandLiderFortune)
    {

        TargetCityModel targetCityModel = countryLider.GetTargetCitySelectPlayer();
        List<CommandLider> commandLiderList = new List<CommandLider>();

        if (countryLider.FlagId != FlagIdPlayer)
        {
            if (commandLider.GetNameCommandFirst() == GlobalParam.TypeEvent.Defence)
            {
                if (countryLider.GetDefenceWeapon().Count() <= 0)
                {
                    commandLider = new CommandLider(GlobalParam.TypeEvent.Propaganda,
                        countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                        Year, targetCityModel, countryLider.FlagId);
                }
            }

            AiAddTargetCity(targetCityModel, commandLider, fiendLider1);

        }
        else
        {
            if (commandLider._TargetCity == null)
            {
                
                AiAddTargetCity(targetCityModel, commandLider, fiendLider1);
            }
        }
        this.TreatmentCommand(commandLider.GetNameCommandFirst(), commandLider,  countryLider.FlagId,
            countryLider.FlagId != FlagIdPlayer, TownList,
        CountryLiderList, countryLider);
        /*
        if (countryLider.GetTargetCitySelectPlayer() != null)
        {
            commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
        }
        */

        commandLider.SetTargetLider(CountryLiderList.Where(a => a.FlagId == targetCityModel.TargetCity.FlagId).FirstOrDefault());

        commandLiderList.Add(commandLider);
        if (commandLiderFortune != null)
        {

            commandLiderList.Add(commandLiderFortune);
        }
        return commandLiderList;
    }
    private void AiAddTargetCity(TargetCityModel targetCityModel, CommandLider commandLider, CountryLider enemyLider)
    {
        //commandLider.SetTargetCity(targetCityModel);
        commandLider.SetTargetLider(enemyLider);
    }
    private void TreatmentCommand(GlobalParam.TypeEvent actionCommand, CommandLider commandLider,
        //TargetCityModel targetCityModel, 
        int FlagId, bool AIfiend, List<CityModel> TownList,
           List<CountryLider> CountryLiderList, CountryLider countryLider)
    {
        
        switch (actionCommand)
        {
            case GlobalParam.TypeEvent.Propaganda:
                commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                
                
                var target = countryLider.GetTargetCitySelectPlayer();
                
                
                countryLider.GetTargetCitySelectPlayer().TargetCity= new ModGameEngine().GetCityRandomFlagId(TownList, countryLider.FiendLider, FlagId, AIfiend);
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
                if (countryLider.GetTargetCitySelectPlayer() == null)
                {
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                }
                else
                {
                    commandLider.SetVisibleAttackBomber(true);
                    //commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
                    commandLider.SetTargetLider(countryLider.GetTargetCitySelectPlayer().EnemyLider);
                    commandLider.SetAttackBomber(countryLider.GetBomber());
                }
                break;
            case GlobalParam.TypeEvent.AttackMissle:
                if (countryLider.GetTargetCitySelectPlayer() == null)
                {
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.Propaganda, true);
                }
                else
                {
                    commandLider.SetVisibleEventList(GlobalParam.TypeEvent.AttackMissle, true);
                    //commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
                    commandLider.SetTargetLider(countryLider.GetTargetCitySelectPlayer().EnemyLider);
                    commandLider.SetAttackMissle(countryLider.GetMissle());
                }
                break;
            default:
                //print ("Incorrect intelligence level.");
                break;
        }
    }

}
