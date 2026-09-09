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
        public List<string> RainList { private set; get; }
        public GameParam(int countYear)
        {
            CountYear = countYear;
            RainList = new List<string>();
        }
        public void IncrementYear()
        {
            CountYear++;
        }
        public void AddRain(string name)
        {
            Debug.Log("8109  - FIEND Command  tDamagePo GetNameFiendLider  fu    = " + name);
            RainList.Add(name);
        }
        public void ResetRain()
        {
            RainList.Clear();
        }
    }
}
