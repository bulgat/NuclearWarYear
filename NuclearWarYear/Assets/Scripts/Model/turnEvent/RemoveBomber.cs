using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model.turnEvent
{
    public class RemoveBomber
    {
        public void AddBomber(MainModel mainModel, CountryLider lider)
        {
            
            foreach (CommandLider commandLider in mainModel.GetCommandLiderList(mainModel._gameParam.CountYear, lider))
            {


                if (commandLider.IncidentCommand.SecondIncident != null)
                {
                    lider.AddMissle(new List<Incident>() { commandLider.IncidentCommand.SecondIncident });
                } else
                {
                    //throw new Exception("Error Second incident null");
                }
 
            }

        }
    }
}
