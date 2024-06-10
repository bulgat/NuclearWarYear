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
		List<CityModel> TownList,int _flagIdPlayer,int FlagIdPlayer,int CountYear)
    {

        

        // all country lider
        foreach (CountryLider lider in CountryLiderList) {
            // only fiend
            GlobalParam.TypeEvent actionNameCommand = GlobalParam.TypeEvent.None;

            if (lider.FlagId!=FlagIdPlayer){
                
                
                if (lider.ReleaseCommandList != null)
				{
                    Debug.Log( "   $ === SET  "+ lider.FlagId + " actionC  = "  );
                    if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle ){
						
						lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, GlobalParam.TypeEvent.AttackMissle,lider.FlagId,0,CountYear));
						continue;
					}
					Debug.Log("   Bomber = " ) ;
					if(lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber)
                    {
                        
                        lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer, GlobalParam.TypeEvent.AttackBomber,lider.FlagId,0, CountYear));
						continue;
					}
				}
			
				actionNameCommand = new RandomActionCommand().GetRandomActionCommand();
			} 
			else
			{
				//auto command player
				var type = lider.ReleaseCommandList?.FirstOrDefault();

                

                if (type == null) {
					actionNameCommand = GlobalParam.TypeEvent.Propaganda;

				} else
				{
					actionNameCommand  = type.GetName();

					if (lider.ReleaseCommandList.Last().GetYear()+1== CountYear)
					{
						if(lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Missle)
						{
                            actionNameCommand = GlobalParam.TypeEvent.AttackMissle;

                        }
						if (lider.ReleaseCommandList.Last().Type == GlobalParam.TypeEvent.Bomber) 
						{
                            actionNameCommand = GlobalParam.TypeEvent.AttackBomber;
                        }
					}
					Debug.Log("      LAST  type = "+ lider.ReleaseCommandList.Last().Type +"& =   type =    " + type.GetName());
                    Debug.Log("CountYear = "+CountYear +"  DamagePop Year = " + lider.ReleaseCommandList.Last().GetYear()+"   damage = "+ lider.ReleaseCommandList.Last().GetDamage());
                }
				
			}

            

            lider.SetCommandLider(new SwitchActionHelper().SwitchAction(ResetAction,CountryLiderList, TownList,FlagIdPlayer,
				actionNameCommand, lider.FlagId,0,CountYear));
		}
	}

}
