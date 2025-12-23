using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVictory 
{
	private bool _endGame;
   public CheckVictory(List<CountryLider> CountryLiderList,List<CityModel> TownList) {
		
		CountryLider liveLider;
		//count
		foreach(CountryLider lider in CountryLiderList){
			List<CityModel> targetCityList = new CityHelperList().GetListCityFlagId(TownList,lider.FlagId,true);
			int population =0;
			foreach(CityModel city in targetCityList){
				population+=city.GetPopulation();
			}
			
			if(population ==0){
				lider.SetDead();
			}
			
		}
		List<CountryLider> CountryLiveLider = new LiveLider().GetLiveLider(CountryLiderList,false);
		if(CountryLiveLider.Count<=1){
			
			if(CountryLiveLider.Count==1){
				liveLider = CountryLiveLider[0];
			}
			_endGame = true;
			
		}
		
	}
	public bool GetEndGame()
    {
		return _endGame;

	} 
	
}
