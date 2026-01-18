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
            bool deadRocketAndBomber = false;
            //deadRocketAndBomber = true;

            if (VisibleAttackRocketAndBomber)
            {
                if (mainModel.GetCommandLider(CountYear, enemylider.FlagId).GetDefence())
                {
                    //new RemoveBomber().DeadOrAddBomber(mainModel, lider, true);
                    Debug.Log("0980   commandLider DEAD ROCKET AND BOMBER  =  "  );
                    deadRocketAndBomber = true;
                }

                string message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetMessage()  
                    + " у " + mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend.Name, mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetName());
                mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                if (deadRocketAndBomber)
                {
                    message = negativeMessage;
                } else
                {
                    if (missle == false)
                    {
                        //add bomber
                        new RemoveBomber().AddBomber(mainModel, lider);
                    }

                Debug.Log("0981 DEAD  untYear name =" + lider.Name + " Town  "+ message + " GetTargetBomb =  = " + deadRocketAndBomber+" inc = "+ CommandIncident.Name);
                }

                if (CommandIncident.SecondIncident != null)
                {

                    CommandIncident.SetReleaseMessage(GlobalParam.MessageDictionary[mainModel.GetCommandLider(CountYear, lider.FlagId).GetNameCommand()].ShowFiend);
                    CommandIncident.SetPopulationEvent(
                        new StateAttackPopulation(message,
                        //deadRocketAndBomber?0:CommandIncident.Damage,
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
