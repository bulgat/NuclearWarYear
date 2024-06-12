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

public class AICreateCommand
{

    public void EstimationSetCommandAiAll(Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, int FlagIdPlayer, int CountYear)
    {
        // all country lider
        foreach (CountryLider lider in CountryLiderList)
        {
            // only fiend
            SetCommandOneLider(lider, ResetAction, CountryLiderList,
        TownList, _flagIdPlayer, FlagIdPlayer, CountYear);

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
        Debug.Log(lider.GetName() + "  a  = " + actionNameCommand);
        if (actionNameCommand == GlobalParam.TypeEvent.None)
        {
            if (lider.FlagId != FlagIdPlayer)
            {
                actionNameCommand = new RandomActionCommand().GetRandomActionCommand();

            }
            else
            {
                Debug.Log(lider.GetName() + "  RESULT PLAYER  = MutationD  =    ");
                actionNameCommand = GlobalParam.TypeEvent.Propaganda;
            }
        }

        CommandLider commandLider = new CommandLider(actionNameCommand, CountYear);
        ResetAction();
        lider.AddCommandLiderList(new SwitchActionHelper().SwitchAction( CountryLiderList, TownList, FlagIdPlayer,
            commandLider, lider, CountYear));
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

            if (lider.ReleaseCommandList.Last().GetYear() == countYear - 2)
            {
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle)
                {
                    Debug.Log("@@@@@@@@@@@@@@@@@@@@   dI = AttackMissle");
                    //actionNameCommand = GlobalParam.TypeEvent.AttackMissle;
                    lider.ReleaseCommandList.Last().SetTypeWeapon(GlobalParam.TypeEvent.AttackMissle);
                }
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber)
                {
                    //actionNameCommand = GlobalParam.TypeEvent.AttackBomber;
                    lider.ReleaseCommandList.Last().SetTypeWeapon(GlobalParam.TypeEvent.AttackBomber);
                }
            }
            Debug.Log(lider.GetName() + "  result (" + lider.ReleaseCommandList.Last().GetYear() + ") year = "
                + lider.ReleaseCommandList.Last().GetYear() + "  CountYear = " + countYear +
                "      LAST  type = " + lider.ReleaseCommandList.Last().Type + " =   type =    " + type.GetName());
            Debug.Log("CountYear = " + countYear + "  DamagePop Year = " + lider.ReleaseCommandList.Last().GetYear() + "   damage = " + lider.ReleaseCommandList.Last().GetDamage());
        }
        return actionNameCommand;
    }

}
