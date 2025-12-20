using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using UnityEngine;

namespace Assets.Scripts.Model.createCommand
{
    internal class CreateFortune
    {
        public CommandLider FortuneEvent( bool AIfiend, CountryLider countryLider,int Year)
        {
            if (GlobalParam.TestMode)
            {
                return GetFortuneEvent(countryLider, Year, GlobalParam.EventFortuneIncidentList[4]);
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
            EventFortuneIncident eventFortuneIncident) 
        {
            var kol = countryLider.GetTargetCitySelectPlayer();

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
