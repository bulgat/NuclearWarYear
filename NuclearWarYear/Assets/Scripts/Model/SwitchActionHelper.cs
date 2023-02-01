using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SwitchActionHelper
{

    public CommandLider SwitchAction(Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, string actionCommand, int FlagId,int MissleId)
    {

        ResetAction();
        CommandLider commandLider = new CommandLider();
        CountryLider countryLider = new LiderHelperOne().GetLiderOne(CountryLiderList, FlagId);
        bool AIfiend = FlagId != _flagIdPlayer;

        

        //CityModel targetCity = null;

        commandLider.SetNameCommand(actionCommand);
        
        if (countryLider.GetCommandLider()?.GetVisibleBomber() == true) { 
            Debug.Log ("0011  Command  FlagId =" + FlagId);
        }
        //Change Ai Command
        if (AIfiend)
        {
            //int countWeapon;
            if (actionCommand== "Missle")
            {
                MissleId = countryLider.GetRandomMissleSizeId(DictionaryMissle.TypeWeapon.Missle);
                if (MissleId== 0)
                {
                    actionCommand = "Propaganda";
                }
             
            }
            if(actionCommand == "Bomber")
            {
                MissleId = countryLider.GetRandomMissleSizeId(DictionaryMissle.TypeWeapon.Bomber);
                //bomber
                if (MissleId == 0)
                {
                    actionCommand = "Propaganda";
                }
            }
            if(actionCommand == ActionCommand.Command.Defence.ToString())
            {
                if (countryLider.GetDefenceWeapon().Count() <= 0)
                {
                    actionCommand = "Propaganda";
                }
            }

         }
        CityModel targetCity = new TargetHelper().GetTargetRandom(CountryLiderList, _flagIdPlayer, AIfiend, true, TownList, _flagIdPlayer);

        commandLider.SetTargetLider( CountryLiderList.Where(a => a.FlagId == targetCity.FlagId).FirstOrDefault());


        if (AIfiend) {
            commandLider.SetTargetCity(targetCity);
            
        }

        // Счастливая карта!
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            
            
            actionCommand = "Defectors";
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {

            
            
            actionCommand = "Ufo";
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            actionCommand = "Baby";
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            actionCommand = "RocketRich";
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            actionCommand = "CrazyCow";
        }
        switch (actionCommand)
        {
            case "CrazyCow":
                commandLider.SetVisibleEventList("CrazyCow", true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "RocketRich":
                commandLider.SetVisibleEventList("RocketRich", true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "Baby":
                commandLider.SetVisibleEventList("Baby", true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "Ufo":
                commandLider.SetVisibleEventList("Ufo", true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "Defectors":
                commandLider.SetVisibleEventList("Defectors", true);
                commandLider.SetTargetCity(targetCity);
                break;
            case "Propaganda":
                commandLider.SetVisibleEventList("Prop",true);
                targetCity = new ModGameEngine().GetCityRandomFlagId(TownList, CountryLiderList[4], FlagId, AIfiend);
                break;
            case "Building":
                commandLider.SetVisibleEventList("Build", true);
                // Add missle
                BuildWeapon buildWeapon = new BuildWeapon();

                commandLider.AddMissle(buildWeapon.AddLiderBuildWeaponSwithAction());
                commandLider.AddReportProducedWeaponList(buildWeapon.GetReportProducedWeaponList());
           
                break;
            case "Defence":
                commandLider.SetVisibleEventList("Defence", true);
                break;
            case "Missle":
                //MissleId
              

                    commandLider.SetVisibleMissle(true, MissleId);
                break;
            case "Bomber":
                commandLider.SetVisibleBomber(true, MissleId);
                break;
            case "AttackBomber":

                if (targetCity == null)
                {
                    commandLider.SetVisibleEventList("Prop", true);
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
                    commandLider.SetVisibleEventList("Prop", true);
                }
                else
                {
                    commandLider.SetVisibleEventList("AttackMissle", true);
                    commandLider.SetTargetCity(targetCity);
                    commandLider.SetAttackMissle(countryLider.GetMissle());

                }

                break;
            default:
                //print ("Incorrect intelligence level.");
                break;
        }

  

        if (countryLider.GetTargetCitySelectPlayer() != null)
        {
            commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
        }


        return commandLider;
    }
 
}
