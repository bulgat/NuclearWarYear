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
            ref Incident CommandIncident, CityModel cityModelTarget, MainModel mainModel)
        {
            string message = "";
            int damageAttackCount = 0;
            // attack bomber
            if (mainModel.GetCommandLider(CountYear, lider.FlagId).GetVisibleAttackBomber())
            {
                if (mainModel.GetCommandLider(CountYear, enemylider.FlagId).GetDefence())
                {
                    //dead bomber
                    lider.RemoveBomber();
                }
                else
                {
                    //bool explode0;
                    if (mainModel.GetCommandLider(CountYear, lider.FlagId)._TargetCity != null)
                    {
                        if (mainModel.GetCommandLider(CountYear, lider.FlagId).GetAttackBomber() != null)
                        {
                            damageAttackCount = mainModel.GetCommandLider(CountYear, lider.FlagId).GetAttackBomber().GetDamage();

                        }
                    }


                }

                message = lider.SetEventTotalMessageTurn(mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetMessage() + damageAttackCount 
                    + " у " + mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend.Name, mainModel.GetCommandLider(CountYear, lider.FlagId).GetIncident().GetName());
                mainModel.GetCommandLider(CountYear, lider.FlagId).LiderFiend._RelationFeind.SetNegativeMood(lider.FlagId, 25);

                CommandIncident.SetReleaseMessage(new StateAttackPopulation(message, damageAttackCount, cityModelTarget, enemylider),
                    GlobalParam.MessageDictionary[mainModel.GetCommandLider(CountYear, lider.FlagId).GetNameCommandFirst()].ShowFiend);
                lider.SetCommandRealise(CommandIncident);
                //return CommandIncident;
            }
            return message;
        }
    }
}
