using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModGameEngine 
{
	public CityModel GetCityFlagId(List<CityModel> TownList,CountryLider Lider,
		int FlagIdOwnerAI,
		bool AI) {

		List<CityModel> TargetCityList = new List<CityModel>();
		foreach(CityModel townCity in TownList){
			//City townCity = city.GetComponent<City>();
			if(AI==false){
				if(townCity.FlagId ==Lider.FlagIdAttack){
					if(townCity.GetPopulation()>0){
						TargetCityList.Add(townCity);
						
					}
					
				}
			} else {
				if(townCity.FlagId !=FlagIdOwnerAI){
					if(townCity.GetPopulation()>0){
						TargetCityList.Add(townCity);
					}
				}
			}
		}
		int indexTownBomber = Random.Range(0, TargetCityList.Count);
		CityModel target = TargetCityList[indexTownBomber];

		return target;
	}
}
