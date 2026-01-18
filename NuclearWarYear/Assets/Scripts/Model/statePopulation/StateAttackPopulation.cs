using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.weapon
{
    public class StateAttackPopulation:PopulationEvent
    {
        public string Name { get; set; } = "Attack";
        public StateAttackPopulation(
            string message,
            int ChangeDamagePopulation,
            CityModel fiendCity,
            CountryLider fiendCountryLider
            ):base(fiendCountryLider) 
        {

            this.Message = message;
            this.FiendPopulation = ChangeDamagePopulation;
            this.FiendCity = fiendCity;
            this.GreatTarget = fiendCity;
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
