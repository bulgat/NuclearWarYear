using Assets.Scripts.Model.param;
using Assets.Scripts.Model.weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.turnEvent
{
    internal class CreateMissleIncident
    {
        public string CreateAttackMissle(CountryLider lider, int CountYear, CountryLider enemylider,
            ref Incident CommandIncident, CityModel cityModelTarget, MainModel mainModel)
        {
            string message = "";
            int damageAttackCount = 0;
            if (mainModel.GetCommandLider(CountYear,lider.FlagId).GetNameExecute(GlobalParam.TypeEvent.AttackMissle))
            {

                if (mainModel.GetCommandLider(CountYear, enemylider.FlagId).GetDefence() == false)
                {
                    if (mainModel.GetCommandLider(CountYear, lider.FlagId)._TargetCity != null)
                    {

                        if (mainModel.GetCommandLider(CountYear, lider.FlagId).GetAttackMissle() != null)
                        {
                            damageAttackCount = mainModel.GetCommandLider(CountYear, lider.FlagId).GetAttackMissle().GetDamage();

                        }
                    }

                }
                lider.RemoveMissle();

                message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage() + damageAttackCount +
                    " у " + mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend.Name,
                    mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetName());
                mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                CommandIncident.SetReleaseMessage(
                    GlobalParam.MessageDictionary[mainModel.GetCommandLider(CountYear, lider.FlagId).GetNameCommandFirst()].ShowFiend);
                CommandIncident.SetPopulationEvent(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider));
               lider.SetCommandRealise(CommandIncident);
            }

            return message;
        }
    }
}
