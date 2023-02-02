using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHelper 
{
	public CityModel GetTargetRandom(
		List<CountryLider> CountryLiderList,
		int FlagIdPlayer,
		bool AIturn,
		List<CityModel> TownList,
		CountryLider countryLider) 
	{
		
		//CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList, FlagIdPlayer);
		
			BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(CountryLiderList, countryLider.FlagId);
			// target city
	
			//CountryLider liderPlayer= new LiderHelperOne().GetLiderOne(CountryLiderList, FlagIdPlayer);
	
			CityModel targetTownCity = new ModGameEngine().GetCityRandomFlagId(TownList, countryLider, FlagIdPlayer, AIturn);
			
			buildingCentral.SetTargetBomber(targetTownCity);
			return targetTownCity;
		//}	
		//return null;
	}
}
