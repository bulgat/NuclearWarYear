﻿using System.Collections;
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
        List<CityModel> TownList, int _flagIdPlayer, int FlagIdPlayer, int CountYear, MainModel mainModel)
    {
        foreach (CountryLider lider in CountryLiderList)
        {
            
            // only fiend
            if (lider.FlagId != FlagIdPlayer)
            {
                List<CommandLider> commandList = SetCommandOneLider(lider, ResetAction, CountryLiderList,
            TownList, _flagIdPlayer, FlagIdPlayer, CountYear, mainModel);
                Debug.Log(lider.Name+ "  co   lider commandList L = " + commandList .Count()+ "getCity  L =" + CountryLiderList.Count);
                mainModel.MainStackCommandLiderList.AddRange(commandList);
            }
        }
    }
    public List<CommandLider> SetCommandOneLider(CountryLider lider, Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, int FlagIdPlayer, int CountYear,MainModel mainModel)
    {
        GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

        if (lider.ReleaseCommandList != null)
        {
            Debug.Log("FullMessage  @@ =  ^^   li  Year  ame = "  );
            actionNameCommand = ChangeIncidentCommand(lider, actionNameCommand, CountYear,mainModel);
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

        Debug.Log("D commandLiderFortune = " + commandLiderFortune);

        CommandLider commandLider = new CommandLider(actionNameCommand, fiendLider1, CountYear, targetCityModel, lider.FlagId);
        ResetAction();
        List<CommandLider> commandLidersList = new SwitchActionHelper().SwitchAction(CountryLiderList,
            TownList, FlagIdPlayer,
            commandLider, lider,
            CountYear, fiendLider1,
             commandLiderFortune);

        Debug.Log("00 etDamagePopula   commandLidersList L = " + commandLidersList.Count + " population = " );
        //mainModel.MainStackCommandLiderList.AddRange(commandLidersList);
        return commandLidersList;
    }


    private GlobalParam.TypeEvent ChangeIncidentCommand(CountryLider lider, GlobalParam.TypeEvent actionNameCommand, int countYear, MainModel mainModel)
    {
        //auto command player
        var type = lider.ReleaseCommandList?.FirstOrDefault();

        Debug.Log(" =  country tePopulationEvent  = " + type);

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
