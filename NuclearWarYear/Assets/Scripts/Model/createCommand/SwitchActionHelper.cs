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

public class SwitchActionHelper
{
    

    public List<CommandLider> SwitchAction(Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int FlagIdPlayer, string actionCommand, int FlagId,int MissleId)
    {
        List<CommandLider> commandLiderList = new List<CommandLider>();

        ResetAction();
    
        CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        bool AIfiend = FlagId != FlagIdPlayer;

        CommandLider commandLider = new CommandLider(actionCommand);
        CountryLider fiendLider1 = new BuildingCentralHelper().GetFiendLider(CountryLiderList, countryLider.FlagId);
        TargetCityModel targetCityModel 
            = new TargetCityModel(new TargetHelper().GetTargetRandom(CountryLiderList, FlagIdPlayer, AIfiend, TownList, countryLider, fiendLider1), fiendLider1);



        //Change Ai Command
        if (AIfiend)
        {
            if (actionCommand== DictionaryEssence.TypeWeapon.Missle.ToString())
            {
                MissleId = countryLider.GetRandomMissleSizeId(DictionaryEssence.TypeWeapon.Missle);
                if (MissleId== 0)
                {
                    actionCommand = GlobalParam.ActionCommand.Propaganda.ToString();
                }
             
            }
           
            if(actionCommand == DictionaryEssence.TypeWeapon.Bomber.ToString())
            {
                MissleId = countryLider.GetRandomMissleSizeId(DictionaryEssence.TypeWeapon.Bomber);
                //bomber
                if (MissleId == 0)
                {
                    actionCommand = GlobalParam.ActionCommand.Propaganda.ToString();
                }
            }
            
            if(actionCommand == GlobalParam.ActionCommand.Defence.ToString())
            {
                if (countryLider.GetDefenceWeapon().Count() <= 0)
                {
                    actionCommand = GlobalParam.ActionCommand.Propaganda.ToString();
                }
            }

         }
     
        


        AiTargetCity(AIfiend, targetCityModel, commandLider, fiendLider1);

        // Счастливая карта!
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(targetCityModel, MissleId, FlagId, AIfiend, TownList, CountryLiderList, countryLider);
       

        this.TreatmentCommand(actionCommand, commandLider, targetCityModel, MissleId, FlagId, AIfiend, TownList,
        CountryLiderList, countryLider);

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
        return commandLiderList;
    }
    private void AiTargetCity(bool AIfiend, TargetCityModel targetCityModel, CommandLider commandLider,CountryLider enemyLider)
    {
        if (AIfiend)
        {
            commandLider.SetTargetCity(targetCityModel);
            commandLider.SetTargetLider(enemyLider);
        }
    }
 private void TreatmentCommand(string actionCommand, CommandLider commandLider,
     TargetCityModel targetCityModel, int MissleId, int FlagId, bool AIfiend, List<CityModel> TownList,
        List<CountryLider> CountryLiderList, CountryLider countryLider)
    {
        switch (actionCommand)
        {
            case "Propaganda":
                commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Propaganda.ToString(), true);
                targetCityModel.TargetCity  = new ModGameEngine().GetCityRandomFlagId(TownList, CountryLiderList[4], FlagId, AIfiend);
                break;
            case "Building":
                commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Build.ToString(), true);
                BuildWeapon buildWeapon = new BuildWeapon();
                commandLider.AddMissle(buildWeapon.AddLiderBuildWeaponSwithAction());
                commandLider.AddReportProducedWeaponList(buildWeapon.GetReportProducedWeaponList());
                break;
            case "Defence":
                commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Defence.ToString(), true);
                break;
            case "Missle":
                commandLider.SetVisibleMissle(true, MissleId);
                break;
            case "Bomber":
                commandLider.SetVisibleBomber(true, MissleId);
                break;
            case "AttackBomber":
                if (targetCityModel == null)
                {
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Propaganda.ToString(), true);
                }
                else
                {
                    commandLider.SetVisibleAttackBomber(true);
                    commandLider.SetTargetCity(targetCityModel);
                    commandLider.SetTargetLider(targetCityModel.EnemyLider);
                    commandLider.SetAttackBomber(countryLider.GetBomber());
                }
                break;
            case "AttackMissle":
                if (targetCityModel == null)
                {
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Propaganda.ToString(), true);
                }
                else
                {
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.AttackMissle.ToString(), true);
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
