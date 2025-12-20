using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetHelper 
{
	public CityModel GetTargetRandom(
		List<CountryLider> CountryLiderList,
        CountryLider fiendLider1,
		bool setTarget,
        CityModel targetTownCity
        ) 
	{
            BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(fiendLider1);

        if (setTarget)
		{
			buildingCentral.buildingCentralModel = new BuildingCentralModel(targetTownCity);
		}

        return targetTownCity;

	}
	public CityModel GetRandomCity(List<CityModel> TownList,
        CountryLider countryLider, int FlagIdPlayer,
        bool AIturn)
	{
return new ModGameEngine().GetCityRandomFlagId(TownList, countryLider, FlagIdPlayer, AIturn);
	}
}
