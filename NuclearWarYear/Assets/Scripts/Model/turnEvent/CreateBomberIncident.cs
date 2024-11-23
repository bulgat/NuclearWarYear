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
        public string CreateAttackBomber(CountryLider lider, int CountYear, CountryLider enemylider,
            ref Incident CommandIncident, CityModel cityModelTarget)
        {
            string message = "";
            int damageAttackCount = 0;
            // attack bomber
            if (lider.GetCommandLiderOne(CountYear).GetVisibleAttackBomber())
            {
                if (enemylider.GetCommandLiderOne(CountYear).GetDefence())
                {
                    //dead bomber
                    lider.RemoveBomber();
                }
                else
                {
                    //bool explode0;
                    if (lider.GetCommandLiderOne(CountYear)._TargetCity != null)
                    {
                        if (lider.GetCommandLiderOne(CountYear).GetAttackBomber() != null)
                        {
                            damageAttackCount = lider.GetCommandLiderOne(CountYear).GetAttackBomber().GetDamage();

                        }
                    }


                }

                message = lider.SetEventTotalMessageTurn(lider.GetCommandLiderOne(CountYear).GetIncident().GetMessage() + damageAttackCount + " у " + lider.GetCommandLiderOne(CountYear).LiderFiend.Name, lider.GetCommandLiderOne(CountYear).GetIncident().GetName());
                lider.GetCommandLiderOne(CountYear).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider),
                    GlobalParam.MessageDictionary[lider.GetCommandLiderOne(CountYear).GetNameCommandFirst()].ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return CommandIncident;
            }
            return message;
        }
    }
}
