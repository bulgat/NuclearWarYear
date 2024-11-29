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
            TownList.Add(new CityModel(1, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(1, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(1, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(1, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(1, mainModel.GetIncrementCityId()));

            TownList.Add(new CityModel(2, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(2, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(2, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(2, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(2, mainModel.GetIncrementCityId()));

            TownList.Add(new CityModel(3, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(3, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(3, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(3, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(3, mainModel.GetIncrementCityId()));

            TownList.Add(new CityModel(4, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(4, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(4, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(4, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(4, mainModel.GetIncrementCityId()));

            TownList.Add(new CityModel(5, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(5, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(5, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(5, mainModel.GetIncrementCityId()));
            TownList.Add(new CityModel(5, mainModel.GetIncrementCityId()));

            return TownList;
        }
    }
}
