using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
