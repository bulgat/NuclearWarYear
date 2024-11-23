using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class SearchTownObject
    {
        public GameObject GetTownViewWithId(CityModel cityModel, List<GameObject> TownList)
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
}
