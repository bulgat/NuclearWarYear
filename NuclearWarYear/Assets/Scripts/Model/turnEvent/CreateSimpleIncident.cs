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
                        mainModel.GetCommandLider(CountYear,lider.FlagId).GetIncident().GetName());
                }
                else
                {
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage(), mainModel.GetCommandLider(CountYear,lider.FlagId).GetIncident().GetName());
                    lider.RemoveDefenceWeapon();
                }
                CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), turnEventExecute.ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return message;
            }
            CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider, CountYear, mainModel).TargetCity;

            CityModel liderCityMy = new UtilModelCity().GetCityModel(TownList, lider);

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
                        mainModel.GetCommandLider(CountYear,lider.FlagId).GetIncident().GetName());
                    lider.RemoveMissle();
                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Bomber:
                    message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage(),
                        mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetName());
                    lider.RemoveBomber();
                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Propaganda:
                    Debug.Log("   I n  ");
                    CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(),
                        liderCityMy, 
                        cityFiend, 
                        enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Build:
                    CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(),
                         liderCityMy,
                         cityFiend,
                         enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Ufo:
                    CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(),
                         liderCityMy,
                         cityFiend,
                         enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Baby:
                    CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(),
                        liderCityMy,
                        cityFiend,
                        enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.RocketRich:
                    CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(),
                         liderCityMy,
                         cityFiend,
                         enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Defectors:
                    CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(),
                        liderCityMy, 
                        cityFiend, 
                        enemylider), turnEventExecute.ShowFiend);
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
