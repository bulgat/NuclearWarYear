using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGameObjHelper 
{
	public GameObject GetCityCameObjectWithId(List<GameObject> TownList, int CityId) {
		foreach(GameObject city in TownList){
			CityView townCity = city.GetComponent<CityView>();
			if(townCity.GetId() ==CityId){
				return city;
			}
		}
		return null;
	}
}
