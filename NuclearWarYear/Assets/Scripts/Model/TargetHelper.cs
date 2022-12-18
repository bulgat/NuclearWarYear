using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHelper 
{
	public CityModel GetTarget(List<CountryLider> CountryLiderList,int FlagId,bool AI,
		bool Missle,List<CityModel> TownList,int FlagPlayer) {
		
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		int countWeapon;
		if(Missle) {
			countWeapon = countryLider.GetMissleCount();
		} else {
			//bomber
			countWeapon = countryLider.GetBomberCount();
		}
		
		if(countWeapon>0){
			BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(CountryLiderList,FlagId);
			// target city
	
			CountryLider liderPlayer= new LiderHelperOne().GetLiderOne(CountryLiderList,FlagPlayer);
	
			CityModel targetTownCity = new ModGameEngine().GetCityFlagId(TownList,liderPlayer,FlagId,AI);
			/*
			Vector3 targetCity = targetTownCity.transform.position;
			buildingCentral.SetTargetBomber(targetCity);
			return targetTownCity;
			*/
			buildingCentral.SetTargetBomber(targetTownCity);
			return targetTownCity;
		}	
		return null;
	}
}
