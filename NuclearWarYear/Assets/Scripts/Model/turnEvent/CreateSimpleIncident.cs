using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //if (CommandIncident.Name == key)
            //{

            if (CommandIncident.Name == GlobalParam.TypeEvent.Missle)
            {
                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
                lider.RemoveMissle();
                CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), turnEventExecute.ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return CommandIncident;
                return message;
            }
            if (CommandIncident.Name == GlobalParam.TypeEvent.Bomber)
            {
                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage(), lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
                lider.RemoveBomber();
                CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, CommandIncident.GetDamage(), null, enemylider), turnEventExecute.ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return CommandIncident;
                return message;
            }

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
                //return CommandIncident;
                return message;
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
                Debug.Log(" --------------- ##  Lider  m  =" + lider.GetCommandLiderOne(CountYear)._reportProducedWeaponList);
                if (lider.GetCommandLiderOne(CountYear)._reportProducedWeaponList != null)
                {
                    List<string> reportProducedWeaponList = lider.GetCommandLiderOne(CountYear)._reportProducedWeaponList;
                    report = string.Join(", ", reportProducedWeaponList.ToArray());
                }
            }

            message = lider.SetEventTotalMessageTurn(report, CommandIncident.Name);

            if (GlobalParam.TypeEvent.Propaganda == CommandIncident.Name ||
            GlobalParam.TypeEvent.Defectors == CommandIncident.Name)
            {

                CommandIncident.SetReleaseMessage(new StateDragPopulation(message, CommandIncident.GetDamage(),
                    liderCityMy, cityFiend, enemylider), turnEventExecute.ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return CommandIncident;
                return message;
            }
            bool doubleCity = CommandIncident.Name == GlobalParam.TypeEvent.Propaganda || CommandIncident.Name == GlobalParam.TypeEvent.Defectors;

            CommandIncident.SetReleaseMessage(new StateAddPopulation(message, -CommandIncident.GetDamage(),
                liderCityMy, enemylider), turnEventExecute.ShowFiend);
            lider.SetCommandRealise(CommandIncident);
            //return CommandIncident;
            return message;

            //}
            return message;
        }
    }
}
