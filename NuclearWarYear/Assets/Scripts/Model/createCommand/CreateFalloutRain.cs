using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Model.createCommand
{
    public class CreateFalloutRain
    {
        public CreateFalloutRain(MainModel mainModel, List<CountryLider> CountryLiderList) 
        {
            Debug.Log("08108  _Cre "+ mainModel._gameParam.RainList.Count);
            var CountryLiderLifeList = CountryLiderList.Where(a => a.GetDead() == false).ToList();
            var list = new List<string>();
            foreach (string item in mainModel._gameParam.RainList) {
                Debug.Log("08109  Cr L " );
                int index = (int)UnityEngine.Random.Range(0.0f, CountryLiderLifeList.Count());
                CountryLider victim = CountryLiderList[index];
                Debug.Log("8110 iss = " + CountryLiderLifeList[index].Name);
                Debug.Log("8111   Ye = " + item);
                
                CommandLider command = new CommandLider(
                        GlobalParam.TypeEvent.Rain,
                        null,
                        mainModel._gameParam.CountYear,
                        victim.TargetCitySelectPlayer,
                        victim);

                mainModel.MainStackCommandLiderList.Add(command);
            }
            mainModel._gameParam.ResetRain();
        }
    }
}
