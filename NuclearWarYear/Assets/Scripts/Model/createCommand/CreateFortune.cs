using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.createCommand
{
    internal class CreateFortune
    {
        public CommandLider FortuneEvent( bool AIfiend, CountryLider countryLider,int Year, bool testMode)
        {
            if (testMode)
            {
                return GetFortuneEvent(countryLider, Year, GlobalParam.EventFortuneIncidentList[3]);
            }



            EventFortuneIncident eventFortuneIncidentRandom = null;

            foreach (EventFortuneIncident eventFortuneIncident in GlobalParam.EventFortuneIncidentList)
            {
                if ((int)UnityEngine.Random.Range(0.0f, eventFortuneIncident.Random) == 1)
                {
                    eventFortuneIncidentRandom = eventFortuneIncident;
                    break;

                }
            }
            if (eventFortuneIncidentRandom!=null)
            {
                return GetFortuneEvent(countryLider, Year, eventFortuneIncidentRandom);
            }

            
            return null;
        }
        private CommandLider GetFortuneEvent(CountryLider countryLider, int Year, 
            EventFortuneIncident eventFortuneIncident) {
            GlobalParam.TypeEvent actionCommand = GlobalParam.TypeEvent.None;
                            actionCommand = eventFortuneIncident.Name;
                    CommandLider commandLider = null;
                    commandLider = new CommandLider(actionCommand,
                        countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                        Year,
                        countryLider.GetTargetCitySelectPlayer(),
                        countryLider.FlagId);
                    commandLider.SetVisibleEventList(eventFortuneIncident.Name, true);
                    commandLider.SetTargetLider(countryLider.GetTargetCitySelectPlayer().EnemyLider);
            return commandLider;
        }
    }
}
