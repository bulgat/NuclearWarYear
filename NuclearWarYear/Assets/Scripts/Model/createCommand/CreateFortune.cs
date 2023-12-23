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
            if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
            {


                actionCommand = SwitchActionHelper.ActionCommand.Defectors.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 2.0f) == 1)
            {



                actionCommand = SwitchActionHelper.ActionCommand.Ufo.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 2.0f) == 1)
            {
                actionCommand = SwitchActionHelper.ActionCommand.Baby.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
            {
                actionCommand = SwitchActionHelper.ActionCommand.RocketRich.ToString();
            }
            if ((int)UnityEngine.Random.Range(0.0f, 30.0f) == 1)
            {
                actionCommand = SwitchActionHelper.ActionCommand.CrazyCow.ToString();
            }
            if (actionCommand != null)
            {
                commandLider = new CommandLider(actionCommand);
            }

            switch (actionCommand)
            {
                case "CrazyCow":
                    commandLider.SetVisibleEventList(SwitchActionHelper.ActionCommand.CrazyCow.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;
                case "RocketRich":
                    commandLider.SetVisibleEventList(SwitchActionHelper.ActionCommand.RocketRich.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;
                case "Baby":
                    commandLider.SetVisibleEventList(SwitchActionHelper.ActionCommand.Baby.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;
                case "Ufo":
                    commandLider.SetVisibleEventList(SwitchActionHelper.ActionCommand.Ufo.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;



                case "Defectors":
                    commandLider.SetVisibleEventList(SwitchActionHelper.ActionCommand.Defectors.ToString(), true);
                    commandLider.SetTargetCity(targetCity);
                    break;

                default:
                    //Debug.LogWarning("Incorrect intelligence level. actionCommand =" + actionCommand);
                    break;
            }

            return commandLider;
        }
    }
}
