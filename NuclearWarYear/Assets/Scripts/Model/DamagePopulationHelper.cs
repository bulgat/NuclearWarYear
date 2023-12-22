using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopulationHelper
{
	public CityModel GetCityLider(CountryLider lider)
	{
		return lider.GetCommandLiderFirst().GetTargetCity();

    }
    public bool SetDamagePopulation( CityModel cityModel,int Damage,bool Explode){

		//CityModel cityModel = GetCityLider(lider);


        if (cityModel != null){
			int population = cityModel.GetPopulation() - Damage;
			if(population<0)
			{
				population =0;
			}
            cityModel.SetFuturePopulation(population);
            cityModel.SetPresentlyPopulation();
			if (Explode){
				
				return true;
			}
		}
		return false;
	}
	
}
