using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AICreateCommand 
{
	
	public void EstimationSetCommandAi(Action ResetAction,List<CountryLider> CountryLiderList,
		List<CityModel> TownList,int _flagIdPlayer,int FlagIdPlayer){

	
		
		// all country lider
		foreach (CountryLider lider in CountryLiderList) {
			// only fiend
			if(lider.FlagId!=FlagIdPlayer){

				
				
				if (lider.GetCommandLider ()!=null){
					if(lider.GetCommandLider ().GetVisibleMissle() ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, "AttackMissle",lider.FlagId,0));
						continue;
					}
					if(lider.GetCommandLider ().GetVisibleBomber() ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, "AttackBomber",lider.FlagId,0));
						continue;
					}
				}


				string actionNameCommand = GetRandomActionCommand();




				lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer,
					actionNameCommand, lider.FlagId,0));
			}
		}
	}
	private string GetRandomActionCommand()
    {
		List<string> nameCommandList = new List<string>() {
			"Propaganda",
			"Building",
			"Defence",
			"Missle",
			"Bomber"
		};
		int indexCommand = UnityEngine.Random.Range(0, nameCommandList.Count);
		return nameCommandList[indexCommand];
	}
	
}
