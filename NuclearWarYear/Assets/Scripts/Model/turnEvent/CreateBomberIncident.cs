using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace Assets.Scripts.Model.turnEvent
{
    public class CreateBomberIncident
    {
        public bool CreateAttackMissleBomber(
            CountryLider lider,
            int CountYear,
            CountryLider enemylider,
            ref Incident CommandIncident,
            CityModel cityModelTarget,
            MainModel mainModel,
            bool VisibleAttackRocketAndBomber,
            string negativeMessage,
            bool missle
            )
        {
            bool deadRocketAndBomber = mainModel.GetCommandLider(CountYear, enemylider).GetDefence();

            if (VisibleAttackRocketAndBomber)
            {

                string message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider).IncidentCommand.GetMessage()  
                    + " у " + mainModel.GetCommandLider(CountYear, lider).LiderFiend.Name, mainModel.GetCommandLider(CountYear, lider).IncidentCommand.GetName());
                mainModel.GetCommandLider(CountYear, lider).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                if (deadRocketAndBomber)
                {
                    message = negativeMessage;

                    if (missle == false)
                    {
                        //add bomber
                        new RemoveBomber().AddBomber(mainModel, lider);
                        CommandIncident.SetMessage("Бомбардировщики сбиты");
                    } else
                    {
                        CommandIncident.SetMessage("Ракеты сбиты");
                    }

                        
                }

                if (CommandIncident.SecondIncident != null)
                {

                    CommandIncident.SetReleaseMessage(GlobalParam.MessageDictionary[mainModel.GetCommandLider(CountYear, lider).GetNameCommand()].ShowFiend);
                    CommandIncident.SetPopulationEvent(
                        new StateAttackPopulation(
                            message,
                            deadRocketAndBomber ? 0 : CommandIncident.SecondIncident.Damage,
                            cityModelTarget,
                            enemylider));
                }
                lider.SetCommandRealise(CommandIncident);

            }
            return deadRocketAndBomber;
        }
    }
}
