using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.turnEvent
{
    public class CreateSimpleIncident
    {
        public string CreateMessageIncident(TurnEventExecute turnEventExecute,
            CountryLider lider, ref Incident CommandIncident, int CountYear, CountryLider enemylider,
            List<CityModel> TownList, MainModel mainModel)
        {
            string message = "";

            if (turnEventExecute.RemoveDefenceWeapon ||
                turnEventExecute.Airport)
            {
                if (turnEventExecute.Airport)
                {
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage(),
                        mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetName());
                }
                else
                {
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage(), mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetName());
                    lider.RemoveDefenceWeapon();
                }
                CommandIncident.SetReleaseMessage(turnEventExecute.ShowFiend);
                CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider));
                lider.SetCommandRealise(CommandIncident);
            }
            // TODE: нужен рандом по настроению.
            //CityModel cityFiend0 = new DamagePopulationHelper().GetCityLider(lider, CountYear, mainModel).TargetCity;
            CityModel cityFiend = new UtilModelCity().GetFiendCity(TownList, lider);
            
            CityModel myCity = new UtilModelCity().GetCityModel(TownList, lider);

            string report = CommandIncident.GetMessage() + CommandIncident.GetDamage();


            mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, turnEventExecute.NegativeMood);

            if (turnEventExecute.MessageSecond != null)
            {
                report += turnEventExecute.MessageSecond + mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend.Name;

            }
            if (turnEventExecute.Ammunition)
            {
                lider.AddMissle(mainModel.GetCommandLider(CountYear, lider.FlagId).GetMissle());

                if (mainModel.GetCommandLider(CountYear, lider.FlagId)._reportProducedWeaponList != null)
                {
                    List<string> reportProducedWeaponList = mainModel.GetCommandLider(CountYear, lider.FlagId)._reportProducedWeaponList;
                    report = string.Join(", ", reportProducedWeaponList.ToArray());
                }
            }

            switch (CommandIncident.Name)
            {
                case GlobalParam.TypeEvent.Missle:
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage(),
                        mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetName());
                    lider.RemoveMissle();
                    CommandIncident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.AttackMissle:
                    CommandIncident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, CommandIncident.GetDamage(), cityFiend, enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Bomber:
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage(),
                        mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetName());
                    lider.RemoveBomber();
                    CommandIncident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.AttackBomber:
                    CommandIncident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, CommandIncident.GetDamage(), cityFiend, enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Propaganda:
                    CommandIncident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateDragPopulation(message, -CommandIncident.GetDamage(),
                        myCity,
                        cityFiend,
                        enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Build:
                    CommandIncident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateDragPopulation(message, CommandIncident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Ufo:
                    Debug.Log("UFO myCity ="+ myCity.Name+"  f = "+ cityFiend.Name);
                    CommandIncident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateDragPopulation(message,
                        CommandIncident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Baby:
                    CommandIncident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateDragPopulation(message, CommandIncident.GetDamage(),
                        myCity,
                        cityFiend,
                        enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.RocketRich:

                    Debug.Log("0100 myCity = "+ myCity.Name);
                    Debug.Log("0101 cityFiend = " + cityFiend.Name);
                    CommandIncident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateDragPopulation(message,
                        CommandIncident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.CrazyCow:
                    Debug.Log("120  i ortu my city = " + myCity.Name);
                    Debug.Log("121 ==== city fiend = " + cityFiend.Name);
                    CommandIncident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(
                        new StateAddPopulation(message,
                        CommandIncident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Defectors:
        
                    CommandIncident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    CommandIncident.SetPopulationEvent(new StateDragPopulation(
                        message, 
                        CommandIncident.GetDamage(),
                        myCity,
                        cityFiend,
                        enemylider));
                    lider.SetCommandRealise(CommandIncident);
                    break;
                default:
                    break;
            }



            message = lider.SetEventTotalMessageTurn(report, CommandIncident.Name);

            return message;
        }
    }
}
