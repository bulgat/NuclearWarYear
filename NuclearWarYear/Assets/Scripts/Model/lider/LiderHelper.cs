using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
public class LiderHelper
{
	public CountryLider GetLiderEnemy(List<CountryLider> CountryLiderList,CountryLider lider,
		int CountYear, MainModel mainModel)
    {
		
		if(mainModel.GetCommandLider(CountYear,lider.FlagId)._TargetCity == null){
            return null;
		}
        
        return new LiderHelperOne().GetLiderOne(CountryLiderList,
            mainModel.GetCommandLider(CountYear,lider.FlagId)._TargetCity.TargetCity.FlagId);
	}
}
