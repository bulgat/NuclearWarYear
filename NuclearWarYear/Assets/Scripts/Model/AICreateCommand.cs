using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Model.createCommand;

public class AICreateCommand 
{
	
	public void EstimationSetCommandAi(Action ResetAction,List<CountryLider> CountryLiderList,
		List<CityModel> TownList,int _flagIdPlayer,int FlagIdPlayer){

	
		
		// all country lider
		foreach (CountryLider lider in CountryLiderList) {
			// only fiend
			if(lider.FlagId!=FlagIdPlayer){

				

				if (lider.ReleaseCommandList != null){
					if(lider.ReleaseCommandList.First().GetVisibleMissle() ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, "AttackMissle",lider.FlagId,0));
						continue;
					}
					if(lider.ReleaseCommandList.First().GetVisibleBomber() ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, "AttackBomber",lider.FlagId,0));
						continue;
					}
				}
				//not missle and bomber

				string actionNameCommand = new RandomActionCommand().GetRandomActionCommand();
				lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer,
					actionNameCommand, lider.FlagId,0));

			}
		}
	}

}
