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
		CountryLider countryLider,
        CountryLider fiendLider1
        ) 
	{
		

            BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(fiendLider1);
			// target city

			CityModel targetTownCity = new ModGameEngine().GetCityRandomFlagId(TownList, countryLider, FlagIdPlayer, AIturn);
			
			buildingCentral.SetTargetBomber(targetTownCity);
			return targetTownCity;

	}
}
