using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.weapon
{
    public class PopulationEvent

    {
        public string Message { get; protected set; }
        public CityModel MyCity { get; protected set; }
        public CityModel FiendCity { get; protected set; }
        public CountryLider FiendCountryLider { get; private set; }
        public bool DoubleCity { get; protected set; }
        public int MyPopulation { get; protected set; }
        public int FiendPopulation { get; protected set; }
        public CityModel GreatTarget { get; protected set; }

        public PopulationEvent(CountryLider fiendCountryLider)
        {
            if (fiendCountryLider == null)
            {
                throw new Exception("fiend error");
            }

            FiendCountryLider = fiendCountryLider;
        }

    }
}
