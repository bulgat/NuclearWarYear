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
            List<CityModel> TownList)
        {
            string message = "";


            if (turnEventExecute.RemoveDefenceWeapon ||
                turnEventExecute.Airport)
            {
                if (turnEventExecute.Airport)
                {
                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
                }
                else
                {
                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
                    lider.RemoveDefenceWeapon();
                }
                CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), turnEventExecute.ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return message;
            }
            CityModel cityFiend = new DamagePopulationHelper().GetCityLider(lider, CountYear).TargetCity;

            CityModel liderCityMy = new UtilModelCity().GetCityModel(TownList, lider);

            string report = CommandIncident.GetMessage() + CommandIncident.GetDamage();


            lider.GetCommandLiderOne(CountYear).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, turnEventExecute.NegativeMood);

            if (turnEventExecute.MessageSecond != null)
            {
                report += turnEventExecute.MessageSecond + lider.GetCommandLiderOne(CountYear).LiderFiend.Name;

            }
            if (turnEventExecute.Ammunition)
            {
                lider.AddMissle(lider.GetCommandLiderOne(CountYear).GetMissle());
                
                if (lider.GetCommandLiderOne(CountYear)._reportProducedWeaponList != null)
                {
                    List<string> reportProducedWeaponList = lider.GetCommandLiderOne(CountYear)._reportProducedWeaponList;
                    report = string.Join(", ", reportProducedWeaponList.ToArray());
                }
            }


            switch (CommandIncident.Name)
            {
                case GlobalParam.TypeEvent.Missle:
                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
                    lider.RemoveMissle();
                    CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), turnEventExecute.ShowFiend);
                    lider.SetCommandRealise(CommandIncident);
                    break;
                case GlobalParam.TypeEvent.Bomber:
                    message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
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

            //bool doubleCity = CommandIncident.Name == GlobalParam.TypeEvent.Propaganda || CommandIncident.Name == GlobalParam.TypeEvent.Defectors;

            //CommandIncident.SetReleaseMessage(new StateAddPopulation(message, -CommandIncident.GetDamage(),
               // liderCityMy, enemylider), turnEventExecute.ShowFiend);
            //lider.SetCommandRealise(CommandIncident);
            return message;
        }
    }
}
