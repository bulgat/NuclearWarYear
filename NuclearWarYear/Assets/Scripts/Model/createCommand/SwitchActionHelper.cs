﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static SwitchActionHelper;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.Progress;

public class SwitchActionHelper
{
    public enum ActionCommand { Propaganda, Defence, Defectors, Ufo, Baby, RocketRich, CrazyCow,Build, AttackMissle, AttackBomber,Bomber,Missle }

    public List<CommandLider> SwitchAction(Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int FlagIdPlayer, string actionCommand, int FlagId,int MissleId)
    {
        List<CommandLider> commandLiderList = new List<CommandLider>();

        ResetAction();
    
        CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        bool AIfiend = FlagId != FlagIdPlayer;

        CommandLider commandLider = new CommandLider(actionCommand);
        
        
        
        /*
        if (countryLider.ReleaseCommandList?.First()?.GetVisibleBomber() == true) {
            actionCommand = ActionCommand.AttackBomber.ToString();
        }
        if (countryLider.ReleaseCommandList?.First()?.GetVisibleMissle() == true)
        {
            actionCommand = ActionCommand.AttackMissle.ToString();
        }
        */
        //Change Ai Command
        if (AIfiend)
        {
            //int countWeapon;
            
            if (actionCommand== DictionaryEssence.TypeWeapon.Missle.ToString())
            {
                MissleId = countryLider.GetRandomMissleSizeId(DictionaryEssence.TypeWeapon.Missle);
                if (MissleId== 0)
                {
                    actionCommand = ActionCommand.Propaganda.ToString();
                }
             
            }
           
            if(actionCommand == DictionaryEssence.TypeWeapon.Bomber.ToString())
            {
                MissleId = countryLider.GetRandomMissleSizeId(DictionaryEssence.TypeWeapon.Bomber);
                //bomber
                if (MissleId == 0)
                {
                    actionCommand = ActionCommand.Propaganda.ToString();
                }
            }
            
            if(actionCommand == ActionCommand.Defence.ToString())
            {
                if (countryLider.GetDefenceWeapon().Count() <= 0)
                {
                    actionCommand = ActionCommand.Propaganda.ToString();
                }
            }

         }
     
CityModel targetCity = new TargetHelper().GetTargetRandom(CountryLiderList, FlagIdPlayer, AIfiend, TownList, countryLider);

        if (AIfiend) {
            commandLider.SetTargetCity(targetCity);
            
        }

        // Счастливая карта!
        CommandLider commandLiderFortune = FortuneEvent(targetCity, MissleId, FlagId, AIfiend, TownList, CountryLiderList, countryLider);
       

        TreatmentCommand(actionCommand, commandLider, targetCity, MissleId, FlagId, AIfiend, TownList,
        CountryLiderList, countryLider);

        if (countryLider.GetTargetCitySelectPlayer() != null)
        {
            commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
        }
        

        commandLider.SetTargetLider(CountryLiderList.Where(a => a.FlagId == targetCity.FlagId).FirstOrDefault());

        commandLiderList.Add(commandLider);
        if (commandLiderFortune != null)
        {
            
            commandLiderList.Add(commandLiderFortune);
        }
        return commandLiderList;
    }
 private void TreatmentCommand(string actionCommand, CommandLider commandLider, CityModel targetCity, int MissleId, int FlagId, bool AIfiend, List<CityModel> TownList,
        List<CountryLider> CountryLiderList, CountryLider countryLider)
    {
        switch (actionCommand)
        {
            case "Propaganda":
                commandLider.SetVisibleEventList(ActionCommand.Propaganda.ToString(), true);
                targetCity = new ModGameEngine().GetCityRandomFlagId(TownList, CountryLiderList[4], FlagId, AIfiend);
                break;
            case "Building":
                commandLider.SetVisibleEventList(ActionCommand.Build.ToString(), true);
                BuildWeapon buildWeapon = new BuildWeapon();
                commandLider.AddMissle(buildWeapon.AddLiderBuildWeaponSwithAction());
                commandLider.AddReportProducedWeaponList(buildWeapon.GetReportProducedWeaponList());
                break;
            case "Defence":
                commandLider.SetVisibleEventList(ActionCommand.Defence.ToString(), true);
                break;
            case "Missle":
                commandLider.SetVisibleMissle(true, MissleId);
                break;
            case "Bomber":
                commandLider.SetVisibleBomber(true, MissleId);
                break;
            case "AttackBomber":
                if (targetCity == null)
                {
                    commandLider.SetVisibleEventList(ActionCommand.Propaganda.ToString(), true);
                }
                else
                {
                    commandLider.SetVisibleAttackBomber(true);
                    commandLider.SetTargetCity(targetCity);
                    commandLider.SetAttackBomber(countryLider.GetBomber());
                }
                break;
            case "AttackMissle":
                if (targetCity == null)
                {
                    commandLider.SetVisibleEventList(ActionCommand.Propaganda.ToString(), true);
                }
                else
                {
                    commandLider.SetVisibleEventList(ActionCommand.AttackMissle.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    commandLider.SetAttackMissle(countryLider.GetMissle());
                }
                break;
            default:
                //print ("Incorrect intelligence level.");
                break;
        }
    }
    private CommandLider FortuneEvent(CityModel targetCity, int MissleId,int FlagId, bool AIfiend, List<CityModel> TownList,
        List<CountryLider> CountryLiderList, CountryLider countryLider)
    {
        CommandLider commandLider = null;
        string actionCommand=null;
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {


            actionCommand = ActionCommand.Defectors.ToString();
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {



            actionCommand = ActionCommand.Ufo.ToString();
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            actionCommand = ActionCommand.Baby.ToString();
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            actionCommand = ActionCommand.RocketRich.ToString();
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            actionCommand = ActionCommand.CrazyCow.ToString();
        }
        if (actionCommand != null)
        {
            commandLider = new CommandLider(actionCommand);
        }

        switch (actionCommand)
        {
            case "CrazyCow":
                commandLider.SetVisibleEventList(ActionCommand.CrazyCow.ToString(), true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "RocketRich":
                commandLider.SetVisibleEventList(ActionCommand.RocketRich.ToString(), true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "Baby":
                commandLider.SetVisibleEventList(ActionCommand.Baby.ToString(), true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "Ufo":
                commandLider.SetVisibleEventList(ActionCommand.Ufo.ToString(), true);
                commandLider.SetTargetCity(targetCity);
                break;



            case "Defectors":
                commandLider.SetVisibleEventList(ActionCommand.Defectors.ToString(), true);
                commandLider.SetTargetCity(targetCity);
                break;

            default:
                //Debug.LogWarning("Incorrect intelligence level. actionCommand =" + actionCommand);
                break;
        }

                return commandLider;
    }
}