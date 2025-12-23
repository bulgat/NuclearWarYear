using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class CityHelperList 
{
	public List<CityModel> GetListCityFlagId(List<CityModel> TownList,int FlagId, bool myCity){
		List<CityModel> targetCityList = new List<CityModel>();
		foreach(CityModel townCity in TownList){
			//if(townCity.FlagId == FlagId){
			if (GetCondition(townCity.FlagId, FlagId, myCity)) { 
				if(townCity.GetPopulation()>0) {
					targetCityList.Add(townCity);
				}
				
			}

		}
		return targetCityList;
	}
	private bool  GetCondition(int ArrayFlagId, int FlagId, bool myCity)
	{
		if (myCity) {
			if (ArrayFlagId == FlagId)
			{
				return true;
			}
			return false;
        } else
		{
            if (ArrayFlagId != FlagId)
            {
                return true;
            }
            return false;
        }
	}
}
