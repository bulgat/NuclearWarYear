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
        public bool DoubleCity { get; protected set; }
        public int MyPopulation { get; protected set; }
        public int FiendPopulation { get; protected set; }

    }
}
