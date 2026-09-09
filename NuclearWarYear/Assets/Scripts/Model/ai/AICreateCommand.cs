using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Model;
using Assets.Scripts.Model.AiTurn;

public class AICreateCommand
{

    public void EstimationCreateCommandAiAll(
        List<CountryLider> CountryLiderList,
        List<CityModel> TownList,
        CountryLider flagPlayer,
        int CountYear,
        MainModel mainModel)
    {
        foreach (CountryLider lider in CountryLiderList)
        {
            // only fiend, skip destroyed countries (no cities left)
            if (lider.FlagId != flagPlayer.FlagId && !lider.GetDead())
            {
                List<CommandLider> commandList = new CreateCommandLider()
                    .GetCommandOneLiderList(
                        lider,
                        CountryLiderList,
                        TownList, 
                        flagPlayer, 
                        CountYear, 
                        mainModel);

                mainModel.MainStackCommandLiderList.AddRange(commandList);
            }
        }
    }
}
