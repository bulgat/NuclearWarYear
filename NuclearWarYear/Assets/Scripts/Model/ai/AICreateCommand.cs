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
using Assets.Scripts.Model.AiTurn;

public class AICreateCommand
{

    public void EstimationCreateCommandAiAll(
        Action ResetAction,
        List<CountryLider> CountryLiderList,
        List<CityModel> TownList,
        int _flagIdPlayer,
        int FlagIdPlayer,
        int CountYear,
        MainModel mainModel)
    {
        foreach (CountryLider lider in CountryLiderList)
        {
            // only fiend
            if (lider.FlagId != FlagIdPlayer)
            {
                List<CommandLider> commandList = new CreateCommandLider()
                    .CommandOneLider(lider, ResetAction, CountryLiderList,
            TownList, _flagIdPlayer, FlagIdPlayer, CountYear, mainModel);

                mainModel.MainStackCommandLiderList.AddRange(commandList);
            }
        }
    }
}
