using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetHelper
{
    public void SetTargetBuilding(
        List<CountryLider> CountryLiderList,
        CountryLider fiendLider1,
        bool setTarget,
        CityModel myCity,
        CityModel targetTownCity
        )
    {
        BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(fiendLider1);

        if (setTarget)
        {
            buildingCentral.SetTargetModel(new TargetModel(myCity), new TargetModel(targetTownCity));
        }



    }
   


    public CityModel GetRandomCity(List<CityModel> TownList,
        CountryLider countryLider, int FlagIdPlayer,
        bool AIturn)
    {
        return new ModGameEngine().GetCityRandomFlagId(TownList, countryLider, FlagIdPlayer, AIturn);
    }
}
