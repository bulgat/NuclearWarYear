using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCentralHelper
{
    public BuildingCentral GetBuildingCentral(CountryLider CountryLider)
    {

        return CountryLider.PropagandaBuilding.GetComponent<BuildingCentral>();

    }
    public CountryLider GetFiendLider(List<CountryLider> CountryLiderList, int FlagId)
    {
        foreach (CountryLider CountryLider in CountryLiderList)
        {
            if (CountryLider.FlagId == FlagId)
            {

                return CountryLider;
            }
        }
        return null;
    }
}
