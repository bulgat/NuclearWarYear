using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Model.createCommand
{
    internal class CreateFortune
    {
        public CommandLider FortuneEvent( bool AIfiend, CountryLider countryLider,int Year)
        {
            if (GlobalParam.TestMode)
            {
                return GetFortuneEvent(countryLider, Year, DictionaryEssence.GetFortune().FirstOrDefault(a=>a.Id == 13).Fortune);
            }

            EventFortuneIncident eventFortuneIncidentRandom = null;

            foreach (var eventFortuneIncident in DictionaryEssence.GetFortune())
            {
                if ((int)UnityEngine.Random.Range(0.0f, eventFortuneIncident.Fortune.Random) == 1)
                {
                    eventFortuneIncidentRandom = eventFortuneIncident.Fortune;
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
            var kol = countryLider.TargetCitySelectPlayer;

            GlobalParam.TypeEvent actionCommand = GlobalParam.TypeEvent.None;
                            actionCommand = eventFortuneIncident.Name;
                    CommandLider commandLider = null;
                    commandLider = new CommandLider(actionCommand,
                        countryLider._RelationFeind.GetHighlyHatredLiderRandom(),
                        Year,
                        countryLider.TargetCitySelectPlayer,
                        countryLider.FlagId);
                    commandLider.SetVisibleEventList(eventFortuneIncident.Name, true);
                    commandLider.SetTargetLider(countryLider.TargetCitySelectPlayer.EnemyLider);
            return commandLider;
        }
    }
}
