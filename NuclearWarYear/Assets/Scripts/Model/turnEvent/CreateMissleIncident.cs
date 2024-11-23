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
            ref Incident CommandIncident, CityModel cityModelTarget)
        {
            string message = "";
            int damageAttackCount = 0;
            if (lider.GetCommandLiderOne(CountYear).GetNameExecute(GlobalParam.TypeEvent.AttackMissle))
            {

                if (enemylider.GetCommandLiderOne(CountYear).GetDefence() == false)
                {
                    if (lider.GetCommandLiderOne(CountYear)._TargetCity != null)
                    {

                        if (lider.GetCommandLiderOne(CountYear).GetAttackMissle() != null)
                        {
                            damageAttackCount = lider.GetCommandLiderOne(CountYear).GetAttackMissle().GetDamage();

                        }
                    }

                }
                lider.RemoveMissle();

                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage() + damageAttackCount +
                    " у " + lider.GetCommandLiderOne(CountYear).LiderFiend.Name,
                    lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
                lider.GetStackCommandLider(CountYear).First().LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider),
                    GlobalParam.MessageDictionary[lider.GetCommandLiderOne(CountYear).GetNameCommandFirst()].ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return CommandIncident;
            }

            return message;
        }
    }
}
