using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCentralHelper 
{
   public BuildingCentral GetBuildingCentral(List<CountryLider> CountryLiderList,int FlagId){
	
		foreach(CountryLider CountryLider in CountryLiderList)
		{
			if(CountryLider.FlagId==FlagId)
			{
				
				return CountryLider.PropagandaBuilding.GetComponent<BuildingCentral>();
			}
		}
		return null;

	}
}
