using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static Assets.Scripts.Model.param.GlobalParam;
using Assets.Scripts.Model;

public class AICreateCommand
{

    public void EstimationSetCommandAiAll(Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, int FlagIdPlayer, int CountYear)
    {
        foreach (CountryLider lider in CountryLiderList)
        {
            Debug.Log(lider.Name+"  all country lider   TargetCity  L =" + CountryLiderList.Count);
            // only fiend
            if (lider.FlagId != FlagIdPlayer)
            {
                SetCommandOneLider(lider, ResetAction, CountryLiderList,
            TownList, _flagIdPlayer, FlagIdPlayer, CountYear);
            }
        }
    }
    public void SetCommandOneLider(CountryLider lider, Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, int FlagIdPlayer, int CountYear)
    {
        GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

        if (lider.ReleaseCommandList != null)
        {
            actionNameCommand = ChangeIncidentCommand(lider, actionNameCommand, CountYear);
        }

        if (actionNameCommand == GlobalParam.TypeEvent.None)
        {
            if (lider.FlagId != FlagIdPlayer)
            {
                actionNameCommand = new RandomActionCommand().GetRandomActionCommand();
            }
            else
            {
                actionNameCommand = GlobalParam.TypeEvent.Propaganda;
            }
        }

        CountryLider fiendLider1 = lider._RelationFeind.GetHighlyHatredLiderRandom();

        TargetCityModel targetCityModel
            = new TargetCityModel(new TargetHelper().GetTargetRandom(CountryLiderList, FlagIdPlayer,
            lider.FlagId != FlagIdPlayer, TownList, lider, fiendLider1), fiendLider1);

        if (lider.FlagId != _flagIdPlayer)
        {
            lider.SetTargetCity(targetCityModel);
        }

        // Счастливая карта!
        CommandLider commandLiderFortune = new CreateFortune().FortuneEvent(
            lider.FlagId != FlagIdPlayer, lider, CountYear);

        CommandLider commandLider = new CommandLider(actionNameCommand, fiendLider1, CountYear, targetCityModel);
        ResetAction();
        List<CommandLider> commandLidersList = new SwitchActionHelper().SwitchAction(CountryLiderList,
            TownList, FlagIdPlayer,
            commandLider, lider,
            CountYear, fiendLider1,
             commandLiderFortune);

        Debug.Log("00 etDamagePopula   commandLidersList L = " + commandLidersList.Count + " population = " );
        lider.AddCommandLiderList(commandLidersList);
    }


    private GlobalParam.TypeEvent ChangeIncidentCommand(CountryLider lider, GlobalParam.TypeEvent actionNameCommand, int countYear)
    {
        //auto command player
        var type = lider.ReleaseCommandList?.FirstOrDefault();



        if (type == null)
        {
            actionNameCommand = GlobalParam.TypeEvent.Propaganda;

        }
        else
        {
            actionNameCommand = type.GetName();

            if (lider.ReleaseCommandList.Last().GetYear() == countYear - 1)
            {
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle)
                {
                    lider.ReleaseCommandList.Last().SetTypeWeapon(GlobalParam.TypeEvent.AttackMissle);
                }
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber)
                {
                    lider.ReleaseCommandList.Last().SetTypeWeapon(GlobalParam.TypeEvent.AttackBomber);
                }
            }


        }
        return actionNameCommand;
    }

}
