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
        public CommandLider FortuneEvent(CityModel targetCity, int MissleId, int FlagId, bool AIfiend, List<CityModel> TownList,
        List<CountryLider> CountryLiderList, CountryLider countryLider)
        {
            CommandLider commandLider = null;
            string actionCommand = null;

            foreach(EventFortuneIncident eventFortuneIncident in GlobalParam.EventFortuneIncidentList)
            {
                if ((int)UnityEngine.Random.Range(0.0f, eventFortuneIncident.Random) == 1)
                {
                    actionCommand = eventFortuneIncident.Name.ToString();
                    commandLider = new CommandLider(actionCommand);
                    commandLider.SetVisibleEventList(eventFortuneIncident.Name.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                }
            }

            
            return commandLider;
        }
    }
}
