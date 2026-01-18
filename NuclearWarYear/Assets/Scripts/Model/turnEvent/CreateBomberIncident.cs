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
        public bool CreateAttackBomber(
            CountryLider lider,
            int CountYear,
            CountryLider enemylider,
            ref Incident CommandIncident,
            CityModel cityModelTarget,
            MainModel mainModel,
            bool VisibleAttackRocketAndBomber,
            string negativeMessage)
        {
            bool deadRocketAndBomber = false;

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
                    //add bomber
                    new RemoveBomber().AddBomber(mainModel, lider);
                }
                Debug.Log("0981 CountYear name =" + lider.Name + " Town  "+ message + " GetTargetBomb =  = " + deadRocketAndBomber);
                


                CommandIncident.SetReleaseMessage(GlobalParam.MessageDictionary[mainModel.GetCommandLider(CountYear, lider.FlagId).GetNameCommand()].ShowFiend);
                CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, deadRocketAndBomber?0:CommandIncident.Damage, cityModelTarget, enemylider));
                lider.SetCommandRealise(CommandIncident);
                Debug.Log("0982 City   Lider "+(deadRocketAndBomber ? 0 : CommandIncident.Damage) +"  selectTarget  deadBomber = " + deadRocketAndBomber+ " CommandIncident = "+ CommandIncident.Message);
            }
            return deadRocketAndBomber;
        }
    }
}
