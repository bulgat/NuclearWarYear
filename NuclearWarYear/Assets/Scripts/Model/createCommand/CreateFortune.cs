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

            /*
            if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
            {


                actionCommand = GlobalParam.ActionCommand.Defectors.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 2.0f) == 1)
            {



                actionCommand = GlobalParam.ActionCommand.Ufo.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 2.0f) == 1)
            {
                actionCommand = GlobalParam.ActionCommand.Baby.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
            {
                actionCommand = GlobalParam.ActionCommand.RocketRich.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
            {
                actionCommand = GlobalParam.ActionCommand.CrazyCow.ToString();
            }
            */
            /*
            if (actionCommand != null)
            {
                commandLider = new CommandLider(actionCommand);
            }
            */
            /*
            switch (actionCommand)
            {
                case "CrazyCow":
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.CrazyCow.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;
                case "RocketRich":
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.RocketRich.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;
                case "Baby":
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Baby.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;
                case "Ufo":
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Ufo.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;



                case "Defectors":
                    commandLider.SetVisibleEventList(GlobalParam.ActionCommand.Defectors.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;

                default:
                    //Debug.LogWarning("Incorrect intelligence level. actionCommand =" + actionCommand);
                    break;
            }
            */
            return commandLider;
        }
    }
}
