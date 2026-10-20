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
            var CountryLiderLifeList = CountryLiderList.Where(a => a.GetDead() == false).ToList();
            var list = new List<string>();
            foreach (CityModel item in mainModel._gameParam.RainList) {

                int index = (int)UnityEngine.Random.Range(0.0f, CountryLiderLifeList.Count());
                CountryLider victim = CountryLiderList[index];

                
                CommandLider command = new CommandLider(
                        GlobalParam.TypeEvent.Rain,
                        CountryLiderLifeList[index],
                        mainModel._gameParam.CountYear,
                        victim.TargetCitySelectPlayer,
                        victim);

                // Сохраняем город, из которого прилетело облако радиоактивного дождя.
                command.IncidentCommand.SetRainFromCity(item);

                mainModel.MainStackCommandLiderList.Add(command);
            }
            mainModel._gameParam.ResetRain();
        }
    }
}
