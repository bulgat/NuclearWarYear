using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHelper 
{
	public CityModel GetTarget(List<CountryLider> CountryLiderList,int FlagId,bool AIturn,
		bool Missle,List<CityModel> TownList,int FlagPlayer) {
		
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList,FlagId);
		
			BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(CountryLiderList,FlagId);
			// target city
	
			CountryLider liderPlayer= new LiderHelperOne().GetLiderOne(CountryLiderList,FlagPlayer);
	
			CityModel targetTownCity = new ModGameEngine().GetCityFlagId(TownList,liderPlayer,FlagId,AIturn);
			
			buildingCentral.SetTargetBomber(targetTownCity);
			return targetTownCity;
		//}	
		//return null;
	}
}
