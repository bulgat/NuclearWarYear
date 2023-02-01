using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHelper 
{
	public CityModel GetTargetRandom(List<CountryLider> CountryLiderList,int FlagIdPlayer,bool AIturn,
		bool Missle,List<CityModel> TownList,int FlagPlayer) {
		
		CountryLider countryLider =new LiderHelperOne().GetLiderOne(CountryLiderList, FlagIdPlayer);
		
			BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(CountryLiderList, FlagIdPlayer);
			// target city
	
			CountryLider liderPlayer= new LiderHelperOne().GetLiderOne(CountryLiderList,FlagPlayer);
	
			CityModel targetTownCity = new ModGameEngine().GetCityRandomFlagId(TownList,liderPlayer, FlagIdPlayer, AIturn);
			
			buildingCentral.SetTargetBomber(targetTownCity);
			return targetTownCity;
		//}	
		//return null;
	}
}
