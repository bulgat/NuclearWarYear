using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTown
{
    public GameObject GetTownViewWithId(List<GameObject> TownList, CityModel cityModel)
    {
        foreach (GameObject cityTown in TownList)
        {
            
            CityView city = cityTown.GetComponent<CityView>();

            if (city.GetId() == cityModel.GetId())
            {
                return cityTown;

            }
        }
        return null;
    }
}
