using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiderHelper 
{
	public CountryLider GetLiderEnemy(List<CountryLider> CountryLiderList,CountryLider lider){
		
		if(lider.GetCommandLiderFirst()._TargetCity == null){
            return null;
		}
		//CityModel city = lider.GetCommandLiderFirst().GetTargetCity().TargetCity;	
		return new LiderHelperOne().GetLiderOne(CountryLiderList,
			lider.GetCommandLiderFirst()._TargetCity.TargetCity.FlagId);
	}
}
