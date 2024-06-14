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

        //ResetAction();

        //bool AIfiend = countryLider.FlagId != FlagIdPlayer;

        //CountryLider fiendLider1 = new BuildingCentralHelper().GetFiendLider(CountryLiderList, countryLider.FlagId);
        //CountryLider fiendLider1 = countryLider._RelationFeind.GetHighlyHatredLiderRandom();

        /*
        TargetCityModel targetCityModel
            = new TargetCityModel(new TargetHelper().GetTargetRandom(CountryLiderList, FlagIdPlayer,
            countryLider.FlagId != FlagIdPlayer, TownList, countryLider, fiendLider1), fiendLider1);
        */
        //Change Ai Command
        if (countryLider.FlagId != FlagIdPlayer)
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
            Debug.Log( "    ---- ---- ------ ---- -- -------- N  =" + commandLider._TargetCity?.TargetCity);
            //add auto target city
            //commandLider._TargetCity?.TargetCity
            if (commandLider._TargetCity == null)
            {
                
                AiAddTargetCity(targetCityModel, commandLider, fiendLider1);
Debug.Log("0000-- ---- -- --- -- targetCityModel = " + targetCityModel + " -------- N  =" + commandLider._TargetCity?.TargetCity);
            }
        }


        /*
        // Счастливая карта!
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(targetCityModel, countryLider.FlagId,
            countryLider.FlagId != FlagIdPlayer, TownList, CountryLiderList, countryLider, Year);
        */

        this.TreatmentCommand(commandLider.GetNameCommandFirst(), commandLider,  countryLider.FlagId,
            countryLider.FlagId != FlagIdPlayer, TownList,
        CountryLiderList, countryLider);

        if (countryLider.GetTargetCitySelectPlayer() != null)
        {
            commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
        }
        Debug.Log(targetCityModel+"    Li = "+ countryLider .GetName()+ " Rel TargetCity =" + targetCityModel.TargetCity);

        commandLider.SetTargetLider(CountryLiderList.Where(a => a.FlagId == targetCityModel.TargetCity.FlagId).FirstOrDefault());

        commandLiderList.Add(commandLider);
        if (commandLiderFortune != null)
        {

            commandLiderList.Add(commandLiderFortune);
        }
        Debug.Log(Year+"    ---- ---- ------ ---- -- -------- Name   =" + commandLiderList.First().GetNameCommandFirst());
        return commandLiderList;
    }
    private void AiAddTargetCity(TargetCityModel targetCityModel, CommandLider commandLider, CountryLider enemyLider)
    {
        commandLider.SetTargetCity(targetCityModel);
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
                Debug.Log(countryLider.GetName()+ " =  countryLider.FiendLider  = " + countryLider);
                Debug.Log( " 0000  countryLider.FiendLider  = " + countryLider.FiendLider);
                var target = countryLider.GetTargetCitySelectPlayer();
                Debug.Log(" 0001  countryLider.FiendLider  = " + countryLider.GetTargetCitySelectPlayer());
                Debug.Log(" 0002  countryLider.FiendLider  = " + countryLider.GetTargetCitySelectPlayer().TargetCity);
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
                    commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
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
                    commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
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
