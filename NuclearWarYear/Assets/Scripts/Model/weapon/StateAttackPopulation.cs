using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.weapon
{
    public class StateAttackPopulation:PopulationEvent,IStatePopulationEvent
    {
        public string Name { get; set; } = "Attack";
        public StateAttackPopulation(string message,int ChangePopulation,CityModel fiendCity) {
            this.Message = message;
            this.FiendPopulation = ChangePopulation;
            this.FiendCity = fiendCity;
        }
        public CityModel GetMyCity()
        {
            return this.MyCity;
        }
        public CityModel GetFiendCity()
        {
            return this.FiendCity;
        }
        public int GetMyPopulation()
        {
            return this.MyPopulation;
        }

        public int GetFiendPopulation()
        {
            return this.FiendPopulation;
        }
    }
}
