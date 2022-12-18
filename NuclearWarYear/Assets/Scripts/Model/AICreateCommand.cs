using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AICreateCommand 
{
	
	public void EstimationSetCommandAi(Action ResetAction,List<CountryLider> CountryLiderList,
		List<CityModel> TownList,int _flagIdPlayer,int FlagIdPlayer){

		List<string> nameCommandList = new List<string>() {
			"Propaganda","Building","Defence","Missle","Bomber","AttackBomber","AttackMissle"
		};

		foreach(CountryLider lider in CountryLiderList) {
			// only fiend
			if(lider.FlagId!=FlagIdPlayer){
				if(lider.GetCommandLider ()!=null){
					if(lider.GetCommandLider ().VisibleMissle ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, "AttackMissle",lider.FlagId));
						continue;
					}
					if(lider.GetCommandLider ().VisibleBomber ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, "AttackBomber",lider.FlagId));
						continue;
					}
				}
			
				
				int indexCommand = UnityEngine.Random.Range(0, nameCommandList.Count);
		
				lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, 
					nameCommandList[indexCommand],lider.FlagId));
			}
		}
	}
	
}
