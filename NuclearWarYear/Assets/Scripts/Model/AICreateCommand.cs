using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Model.createCommand;
using Assets.Scripts.Model.param;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class AICreateCommand 
{
	
	public void EstimationSetCommandAi(Action ResetAction,List<CountryLider> CountryLiderList,
		List<CityModel> TownList,int _flagIdPlayer,int FlagIdPlayer){

	
		
		// all country lider
		foreach (CountryLider lider in CountryLiderList) {
            // only fiend
            GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

            if (lider.FlagId!=FlagIdPlayer){
                Debug.Log( lider.GetName()+ "    ---- ------------------------------------------ NameComm  =" + lider.FlagId);
                Debug.Log("000 GetDamagePopul = " + lider.ReleaseCommandList);
                if (lider.ReleaseCommandList != null)
				{
                    Debug.Log( "   $ === SET  "+ lider.FlagId + " actionC  = "  );
                    if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, GlobalParam.TypeEvent.AttackMissle,lider.FlagId,0));
						continue;
					}
					Debug.Log("   Bomber = " ) ;
					if(lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber)
                    {
                        
                        lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, GlobalParam.TypeEvent.AttackBomber,lider.FlagId,0));
						continue;
					}
				}
			
				actionNameCommand = new RandomActionCommand().GetRandomActionCommand();
			} 
			else
			{
				
				//auto command player

				if (actionNameCommand == GlobalParam.TypeEvent.None) {
					actionNameCommand = GlobalParam.TypeEvent.Propaganda;

				}
			}

            

            lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer,
				actionNameCommand, lider.FlagId,0));
		}
	}

}
