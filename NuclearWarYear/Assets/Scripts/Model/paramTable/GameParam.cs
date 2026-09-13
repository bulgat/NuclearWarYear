using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.paramTable
{
    public class GameParam
    {
        public int CountYear { private set; get; }
        public List<CityModel> RainList { private set; get; }
        public GameParam(int countYear)
        {
            CountYear = countYear;
            RainList = new List<CityModel>();
        }
        public void IncrementYear()
        {
            CountYear++;
        }
        public void AddRain(CityModel cityFiend)
        {
            Debug.Log("8109  - FIEND Command  tDamagePo GetNameFiendLider  fu    = " + cityFiend.Name);
            RainList.Add(cityFiend);
        }
        public void ResetRain()
        {
            RainList.Clear();
        }
    }
}
