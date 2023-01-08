using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SwitchActionHelper
{

    public CommandLider SwitchAction(Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, string actionCommand, int FlagId)
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
                if (countryLider.GetMissleCount() <= 0)
                {
                    actionCommand = "Propaganda";
                }
            }
            if(actionCommand == "Bomber")
            {
                //bomber
                if (countryLider.GetBomberCount()<=0)
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
        CityModel targetCity = new TargetHelper().GetTarget(CountryLiderList, _flagIdPlayer, AIfiend, true, TownList, _flagIdPlayer);

        commandLider.SetTargetNameLider( CountryLiderList.Where(a => a.FlagId == targetCity.FlagId).FirstOrDefault().GetName());


        if (AIfiend) {
            commandLider.SetTargetCity(targetCity);
        }

        // Счастливая карта!
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {
            Debug.Log("   ----    [  RICH!  ] = ");
            
            actionCommand = "Defectors";
        }
        if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
        {

            Debug.Log("   ---- UFO   [  RICH!  ] = ");
            Debug.Log("  ex ==");
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
                targetCity = new ModGameEngine().GetCityFlagId(TownList, CountryLiderList[4], FlagId, AIfiend);
                break;
            case "Building":
                commandLider.SetVisibleEventList("Build", true);
                // Add missle
                commandLider.AddMissle(new List<Missle>() { new DictionaryMissle().GetMissle(1) });
                commandLider.AddBomber(new List<Bomber>() { new DictionaryMissle().GetBomber(1) });
                commandLider.AddWarhead(new List<Warhead>() { new DictionaryMissle().GetWarhead(1) });
                commandLider.AddDefenceWeapon(new List<Defence>() { new DictionaryMissle().GetDefenceWeapon() });
                break;
            case "Defence":
                commandLider.SetVisibleEventList("Defence", true);
                break;
            case "Missle":
                commandLider.SetVisibleMissle(true);
                break;
            case "Bomber":
                commandLider.SetVisibleBomber(true);
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
