using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveLider 
{
   public List<CountryLider> GetLiveLider(List<CountryLider> CountryLiderList,bool All) {
		List<CountryLider> countryLiderList = new List<CountryLider>();
		foreach(CountryLider lider in CountryLiderList){
			if(All==false) {
				if(lider.GetDead()==false){
					countryLiderList.Add(lider);
				}
			} else {
				
			}
		}
		return countryLiderList;
	}
}
