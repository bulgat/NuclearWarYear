using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTown
{
    public GameObject GetTownViewWithId(List<GameObject> TownList, CityModel cityModel)
    {
        foreach (GameObject cityTown in TownList)
        {
            
            City city = cityTown.GetComponent<City>();

            if (city.GetId() == cityModel.GetId())
            {
                return cityTown;

            }
        }
        return null;
    }
}
