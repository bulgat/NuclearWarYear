using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.weapon
{
    public interface IStatePopulationEvent
    {
        public CityModel GetMyCity();
        public CityModel GetFiendCity();
        public CountryLider GetFiendLider();
        public int GetMyPopulation();
        public int GetFiendPopulation();
    }
}
