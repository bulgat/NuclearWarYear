using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.weapon
{
    public class StateAddPopulation:PopulationEvent,IStatePopulationEvent
    {
        public string Name { get; set; } = "Add";
        public StateAddPopulation(string message,int population,CityModel myCity) {
            this.Message = message;
            this.MyPopulation = population;
            this.MyCity= myCity;
        }

        public CityModel GetMyCity()
        {
            return this.MyCity;
        }
        public CityModel GetFiendCity()
        {
            return this.FiendCity;
        }
        public CountryLider GetFiendLider()
        {
            return this.FiendCountryLider;
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
