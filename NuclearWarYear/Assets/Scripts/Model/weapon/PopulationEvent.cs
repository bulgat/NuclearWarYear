using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.weapon
{
    public class PopulationEvent
    {
        public CityModel City { get; private set; }
        public CityModel FiendCity { get; private set; }
        public bool DoubleCity { get; private set; }
        public int ChangePopulation { get; private set; }

        public PopulationEvent(int ChangePopulation, CityModel City, CityModel fiendCity, bool doubleCity)
        {
            this.ChangePopulation = ChangePopulation;
            this.City = City;
            this.FiendCity = fiendCity;
            this.DoubleCity = doubleCity;
        }
    }
}
