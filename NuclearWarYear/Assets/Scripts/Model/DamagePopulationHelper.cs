using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopulationHelper
{
	public TargetCityModel GetCityLider(CountryLider lider,int CountYear, MainModel mainModel)
	{
		return mainModel.GetCommandLider(CountYear,lider.FlagId)._TargetCity;

    }
    public void SetDamagePopulation( CityModel cityModel,int Damage){
        
        
        if (cityModel != null){
            
			int population = cityModel.GetPopulation() - Damage;
			if(population<0)
			{
				population =0;
			}
            cityModel.SetFuturePopulation(population);
            cityModel.SetPresentlyPopulation();
            //if (Explode){

            //	return true;
            //}
            
        }
	}
	
}
