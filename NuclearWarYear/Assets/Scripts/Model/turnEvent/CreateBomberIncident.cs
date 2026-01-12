using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.turnEvent
{
    public class CreateBomberIncident
    {
        public void CreateAttackBomber(CountryLider lider, int CountYear, CountryLider enemylider,
            ref Incident CommandIncident, CityModel cityModelTarget, MainModel mainModel, bool VisibleAttackBomber, string negativeMessage)
        {
            bool deadBomber = false;
            // attack bomber
            if (VisibleAttackBomber)
            {
                if (mainModel.GetCommandLider(CountYear, enemylider.FlagId).GetDefence())
                {
                    //dead bomber
                    lider.RemoveBomber();
                    deadBomber = true;
                }

                string message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetMessage()  
                    + " у " + mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend.Name, mainModel.GetCommandLider(CountYear, lider.FlagId).IncidentCommand.GetName());
                mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                if (deadBomber)
                {
                    message = negativeMessage;
                }

                CommandIncident.SetReleaseMessage(GlobalParam.MessageDictionary[mainModel.GetCommandLider(CountYear, lider.FlagId).GetNameCommandFirst()].ShowFiend);
                CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, deadBomber?0:CommandIncident.Damage, cityModelTarget, enemylider));
                lider.SetCommandRealise(CommandIncident);
            }
        }
    }
}
