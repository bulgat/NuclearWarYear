using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHelperList 
{
	public List<CityModel> GetListCityFlagId(List<CityModel> TownList,int FlagId){
		List<CityModel> targetCityList = new List<CityModel>();
		foreach(CityModel townCity in TownList){
			//City townCity = city.GetComponent<City>();
			if(townCity.FlagId ==FlagId){
				if(townCity.GetPopulation()>0) {
					targetCityList.Add(townCity);
				}
				
			}

		}
		return targetCityList;
	}
}
