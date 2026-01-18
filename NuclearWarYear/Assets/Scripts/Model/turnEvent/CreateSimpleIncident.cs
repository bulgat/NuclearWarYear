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
        public string CreateMessageIncident(
            TurnEventExecute turnEventExecute,
            CountryLider lider,
            ref Incident incident,
            int CountYear,
            CountryLider enemylider,
            List<CityModel> TownList,
            MainModel mainModel)
        {
            string message = "";

            if (turnEventExecute.RemoveDefenceWeapon ||
                turnEventExecute.Airport)
            {
                if (turnEventExecute.Airport)
                {
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetMessage(),
                        mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetName());
                }
                else
                {
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetMessage(), mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetName());
                    lider.RemoveDefenceWeapon();
                }
                incident.SetReleaseMessage(turnEventExecute.ShowFiend);
                incident.SetPopulationEvent(new StateAttackPopulation(message, incident.GetDamage(), null, enemylider));
                lider.SetCommandRealise(incident);
            }
            // TODE: нужен рандом по настроению.
            CityModel cityFiend = new UtilModelCity().GetFiendCity(TownList, lider);
            
            CityModel myCity = new UtilModelCity().GetCityModel(TownList, lider);

            string report = incident.GetMessage() + incident.GetDamage();


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

            switch (incident.Name)
            {
                case GlobalParam.TypeEvent.Missle:
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetMessage(),
                        mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetName());                   
                    incident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateAttackPopulation(message, incident.GetDamage(), null, enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.AttackMissle:
                    Debug.Log("0055 INCIDENT AttackMissle Lider "+ incident.Id + " Target  "+ incident.UnicalId + " CommandIncident  SEC = " + incident.SecondIncident);
                    incident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateAttackPopulation(message, incident.SecondIncident.Damage, cityFiend, enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.Bomber:
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetMessage(),
                        mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetName());
                    incident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateAttackPopulation(message, incident.GetDamage(), null, enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.AttackBomber:
                    Debug.Log("0055 NuclearExplode  "+ incident.Id+ "  Po "+ incident.UnicalId + " AttackBomber  =   SEC = " + incident.SecondIncident);
                    incident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateAttackPopulation(message, incident.SecondIncident.GetDamage(), cityFiend, enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.Propaganda:
                    incident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateDragPopulation(message, -incident.GetDamage(),
                        myCity,
                        cityFiend,
                        enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.Build:
                    incident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateDragPopulation(message, incident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.Ufo:
                    incident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateDragPopulation(message,
                        incident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.Baby:
                    incident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateDragPopulation(message, incident.GetDamage(),
                        myCity,
                        cityFiend,
                        enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.RocketRich:
                    incident.SetReleaseMessage( turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateDragPopulation(message,
                        incident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.CrazyCow:
                    incident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(
                        new StateAddPopulation(message,
                        incident.GetDamage(),
                         myCity,
                         cityFiend,
                         enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                case GlobalParam.TypeEvent.Defectors:
        
                    incident.SetReleaseMessage(turnEventExecute.ShowFiend);
                    incident.SetPopulationEvent(new StateDragPopulation(
                        message, 
                        incident.GetDamage(),
                        myCity,
                        cityFiend,
                        enemylider));
                    lider.SetCommandRealise(incident);
                    break;
                default:
                    break;
            }

            message = lider.SetEventTotalMessageTurn(report, incident.Name);

            return message;
        }
    }
}
