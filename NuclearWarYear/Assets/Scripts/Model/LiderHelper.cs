using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiderHelper 
{
	public CountryLider GetLiderEnemy(List<CountryLider> CountryLiderList,CountryLider lider){
		
		if(lider.GetCommandLider().GetTargetCity()==null){
			return null;
		}
		CityModel city = lider.GetCommandLider ().GetTargetCity();	
		return new LiderHelperOne().GetLiderOne(CountryLiderList,city.FlagId);
	}
}
