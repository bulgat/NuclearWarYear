using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class TargetCityModel
    {
        public CityModel TargetCity { get; set; }
        public CountryLider EnemyLider { get; set; }
        public TargetCityModel(CityModel city, CountryLider enemyLider)
        {
            this.TargetCity = city; 
            this.EnemyLider = enemyLider;
        }
    }
}
