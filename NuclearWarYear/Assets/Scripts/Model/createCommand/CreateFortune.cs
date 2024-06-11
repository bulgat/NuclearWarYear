using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.createCommand
{
    internal class CreateFortune
    {
        public CommandLider FortuneEvent(TargetCityModel targetCityModel, int FlagId, bool AIfiend, List<CityModel> TownList,
        List<CountryLider> CountryLiderList, CountryLider countryLider,int Year)
        {
            CommandLider commandLider = null;
            GlobalParam.TypeEvent actionCommand = GlobalParam.TypeEvent.None;

            foreach(EventFortuneIncident eventFortuneIncident in GlobalParam.EventFortuneIncidentList)
            {
                if ((int)UnityEngine.Random.Range(0.0f, eventFortuneIncident.Random) == 1)
                {
                    actionCommand = eventFortuneIncident.Name;
                    commandLider = new CommandLider(actionCommand, Year);
                    commandLider.SetVisibleEventList(eventFortuneIncident.Name, true);
                    commandLider.SetTargetCity(targetCityModel);
                    commandLider.SetTargetLider(targetCityModel.EnemyLider);
                }
            }

            
            return commandLider;
        }
    }
}
