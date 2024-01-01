using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.turnEvent
{
   public class UtilModelCity
    {
        public CityModel GetCityModel(List<CityModel> TownList, CountryLider lider)
        {
            List<CityModel> liderCityList = new CityHelperList().GetListCityFlagId(TownList, lider.FlagId);
            int indexTown = UnityEngine.Random.Range(0, liderCityList.Count);


            CityModel liderCityMy = liderCityList[indexTown];
            return liderCityMy;
        }
    }
}
