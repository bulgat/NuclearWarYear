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
                CityView city = cityTown.GetComponent<CityView>();
                if (city != null && cityModel!=null)
                {
                    if (city.GetId() == cityModel.GetId())
                    {
                        return cityTown;

                    }
                }
            }
            return null;
        }
    }
}
