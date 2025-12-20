using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.View
{
    public class BindCity
    {
        public List<CityModel> GetBindCity( MainModel mainModel) {
            List<CityModel> TownList = new List<CityModel>();

            for(int indexFlag = 1; indexFlag < 6; indexFlag++)
            {
                for (int z = 0; z < 5; z++)
                {
                    
                    TownList.Add(new CityModel(indexFlag, mainModel.GetIncrementCityId(), GlobalParam.ParamLiderList[indexFlag - 1].NameCity[z]));
                }
            }


            return TownList;
        }
    }
}
