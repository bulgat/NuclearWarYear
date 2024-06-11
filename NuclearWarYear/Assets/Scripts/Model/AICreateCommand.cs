using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class AICreateCommand 
{
	
	public void EstimationSetCommandAiAll(Action ResetAction,List<CountryLider> CountryLiderList,
		List<CityModel> TownList,int _flagIdPlayer,int FlagIdPlayer,int CountYear)
    {

        

        // all country lider
        foreach (CountryLider lider in CountryLiderList) {
			// only fiend
			SetCommandOneLider(lider, ResetAction, CountryLiderList,
        TownList,_flagIdPlayer, FlagIdPlayer, CountYear);

            
		}
	}
	public void SetCommandOneLider(CountryLider lider, Action ResetAction, List<CountryLider> CountryLiderList,
        List<CityModel> TownList, int _flagIdPlayer, int FlagIdPlayer, int CountYear)
	{
        GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

        //if (lider.FlagId != FlagIdPlayer)
        //{


            if (lider.ReleaseCommandList != null)
            {
                actionNameCommand = ChangeIncidentCommand(lider, actionNameCommand, CountYear);
            /*
                Debug.Log("   $ === SET  " + lider.FlagId + " actionC  = ");
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle)
                {

                    lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, FlagIdPlayer, GlobalParam.TypeEvent.AttackMissle, lider.FlagId, 0, CountYear));

                }
                Debug.Log("   Bomber = ");
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber)
                {

                    lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, FlagIdPlayer, GlobalParam.TypeEvent.AttackBomber, lider.FlagId, 0, CountYear));
     
                }
            */
            }
            if (actionNameCommand == GlobalParam.TypeEvent.None)
            {
                if (lider.FlagId != FlagIdPlayer)
                {
                    actionNameCommand = new RandomActionCommand().GetRandomActionCommand();
                } else
                {
                    actionNameCommand = GlobalParam.TypeEvent.Propaganda;
                }
            }
        //}
        //else
        //{
            /*
            //auto command player
            var type = lider.ReleaseCommandList?.FirstOrDefault();



            if (type == null)
            {
                actionNameCommand = GlobalParam.TypeEvent.Propaganda;

            }
            else
            {
                actionNameCommand = type.GetName();

                if (lider.ReleaseCommandList.Last().GetYear() + 1 == CountYear)
                {
                    if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle)
                    {
                        Debug.Log("@@@@@@@@@@@@@@@@@@@@   dI = AttackMissle");
                        actionNameCommand = GlobalParam.TypeEvent.AttackMissle;

                    }
                    if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber)
                    {
                        actionNameCommand = GlobalParam.TypeEvent.AttackBomber;
                    }
                }
                Debug.Log("result year = " + (lider.ReleaseCommandList.Last().GetYear() + 1) + "  CountYear = " + CountYear + "      LAST  type = " + lider.ReleaseCommandList.Last().Type + " =   type =    " + type.GetName());
                Debug.Log("CountYear = " + CountYear + "  DamagePop Year = " + lider.ReleaseCommandList.Last().GetYear() + "   damage = " + lider.ReleaseCommandList.Last().GetDamage());
            }
            */
        //}

        lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction, CountryLiderList, TownList, FlagIdPlayer,
            actionNameCommand, lider.FlagId, CountYear));
    }
    private GlobalParam.TypeEvent ChangeIncidentCommand(CountryLider lider, GlobalParam.TypeEvent actionNameCommand,int count)
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

            if (lider.ReleaseCommandList.Last().GetYear() + 1 == count)
            {
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle)
                {
                    Debug.Log("@@@@@@@@@@@@@@@@@@@@   dI = AttackMissle");
                    actionNameCommand = GlobalParam.TypeEvent.AttackMissle;

                }
                if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber)
                {
                    actionNameCommand = GlobalParam.TypeEvent.AttackBomber;
                }
            }
            Debug.Log("result year = " + (lider.ReleaseCommandList.Last().GetYear() + 1) + "  CountYear = " + count + "      LAST  type = " + lider.ReleaseCommandList.Last().Type + " =   type =    " + type.GetName());
            Debug.Log("CountYear = " + count + "  DamagePop Year = " + lider.ReleaseCommandList.Last().GetYear() + "   damage = " + lider.ReleaseCommandList.Last().GetDamage());
        }
        return actionNameCommand;
    }

}
