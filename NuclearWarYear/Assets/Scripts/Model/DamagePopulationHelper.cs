using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopulationHelper
{
	public bool SetDamagePopulation(CountryLider lider,int Damage,bool Explode){
		
		if(lider.GetCommandLider ().GetTargetCity()!=null){
			int population = lider.GetCommandLider ().GetTargetCity().GetPopulation() - Damage;
			if(population<0)
			{
				population =0;
			}
			lider.GetCommandLider ().GetTargetCity().SetFuturePopulation(population);
			lider.GetCommandLider ().GetTargetCity().SetPresentlyPopulation();
			if (Explode){
				//lider.GetCommandLider ().GetTargetCity().SetVisible(true);
				return true;
			}
		}
		return false;
	}
	
}
