using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;

public class AICreateCommand 
{
	
	public void EstimationSetCommandAi(Action ResetAction,List<CountryLider> CountryLiderList,
		List<CityModel> TownList,int _flagIdPlayer,int FlagIdPlayer){

	
		
		// all country lider
		foreach (CountryLider lider in CountryLiderList) {
            // only fiend
            GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

            if (lider.FlagId!=FlagIdPlayer){

				if (lider.ReleaseCommandList != null){
					if(lider.ReleaseCommandList.First().GetVisibleMissle() ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, GlobalParam.TypeEvent.AttackMissle,lider.FlagId,0));
						continue;
					}
					if(lider.ReleaseCommandList.First().GetVisibleBomber() ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, GlobalParam.TypeEvent.AttackBomber,lider.FlagId,0));
						continue;
					}
				}
			
				actionNameCommand = new RandomActionCommand().GetRandomActionCommand();
			}
			//auto command player
			if (actionNameCommand == GlobalParam.TypeEvent.None) {
				actionNameCommand = GlobalParam.TypeEvent.Propaganda;

            }


			lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer,
				actionNameCommand, lider.FlagId,0));
		}
	}

}
