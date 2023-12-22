using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiderHelper 
{
	public CountryLider GetLiderEnemy(List<CountryLider> CountryLiderList,CountryLider lider){
		
		if(lider.GetCommandLiderFirst().GetTargetCity()==null){
			return null;
		}
		CityModel city = lider.GetCommandLiderFirst().GetTargetCity();	
		return new LiderHelperOne().GetLiderOne(CountryLiderList,city.FlagId);
	}
}
