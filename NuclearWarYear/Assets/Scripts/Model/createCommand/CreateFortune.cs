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
        public CommandLider FortuneEvent( bool AIfiend, CountryLider countryLider,int Year)
        {
            CommandLider commandLider = null;
            GlobalParam.TypeEvent actionCommand = GlobalParam.TypeEvent.None;

            foreach(EventFortuneIncident eventFortuneIncident in GlobalParam.EventFortuneIncidentList)
            {
                if ((int)UnityEngine.Random.Range(0.0f, eventFortuneIncident.Random) == 1)
                {
                    UnityEngine.Debug.Log(countryLider.GetTargetCitySelectPlayer()+ "  RESU  PLAYER  = Mutation  =    ");
                    actionCommand = eventFortuneIncident.Name;
                    commandLider = new CommandLider(actionCommand, Year);
                    commandLider.SetVisibleEventList(eventFortuneIncident.Name, true);
                    commandLider.SetTargetCity(countryLider.GetTargetCitySelectPlayer());
                    commandLider.SetTargetLider(countryLider.GetTargetCitySelectPlayer().EnemyLider);
                }
            }
            
            
            return commandLider;
        }
    }
}
