using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
public class LiderHelper
{
	public CountryLider GetLiderEnemy(List<CountryLider> CountryLiderList,CountryLider lider,int CountYear)
    {
		
		if(lider.GetCommandLiderOne(CountYear)._TargetCity == null){
            return null;
		}
		//CityModel city = lider.GetCommandLiderFirst().GetTargetCity().TargetCity;	
		return new LiderHelperOne().GetLiderOne(CountryLiderList,
			lider.GetCommandLiderOne(CountYear)._TargetCity.TargetCity.FlagId);
	}
}
