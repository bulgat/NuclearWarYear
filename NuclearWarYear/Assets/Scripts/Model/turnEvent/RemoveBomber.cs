using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace Assets.Scripts.Model.turnEvent
{
    public class RemoveBomber
    {
        public void AddBomber(MainModel mainModel, CountryLider lider)
        {
            
            foreach (CommandLider commandLider in mainModel.GetCommandLiderList(mainModel.CountYear, lider.FlagId))
            {

                Debug.Log("0905  AddBomber    "+ commandLider.IncidentCommand.Id + "  remove SECOND = " + commandLider.IncidentCommand.SecondIncident);
 

                    Debug.Log("0906  ADD   missle = "+ commandLider.IncidentCommand.Name);
                if (commandLider.IncidentCommand.SecondIncident != null)
                {
                    lider.AddMissle(new List<IWeapon>() { commandLider.IncidentCommand.SecondIncident });
                } else
                {
                    //throw new Exception("Error Second incident null");
                }
 
            }

        }
    }
}
