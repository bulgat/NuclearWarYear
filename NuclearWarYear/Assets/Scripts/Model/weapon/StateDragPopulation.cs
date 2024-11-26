using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.weapon
{
    public class StateDragPopulation:PopulationEvent
    {
        public string Name { get; set; } = "Drag";
        public StateDragPopulation(string message, int population,
            CityModel myCity, 
            CityModel fiendCity,
            CountryLider fiendCountryLider):base(fiendCountryLider) {

            this.Message = message;
            this.MyPopulation = population;
            this.FiendPopulation = -population;
            this.MyCity = myCity;
            this.FiendCity = fiendCity;

            Debug.Log("    an  = " + fiendCountryLider.Name);
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
