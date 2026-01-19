using Assets.Scripts.Model.param;
using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace Assets.Scripts.Model.AiTurn
{
    public class ChangeIncident
    {
        public IncidentAttack MutationIncidentCommand(
            CountryLider lider,
            GlobalParam.TypeEvent actionNameCommand,
            int countYear,
            MainModel mainModel)
        {
            //auto command player
            Incident incident = lider.ReleaseCommandList?.FirstOrDefault();

            if (incident == null)
            {
                actionNameCommand = GlobalParam.TypeEvent.Propaganda;
                return new IncidentAttack()
                {
                    TypeEvent = GlobalParam.TypeEvent.Propaganda,
                };
            }
            else
            {
                
                actionNameCommand = incident.GetName();

                List<Incident> lastYeatCommandList = lider.ReleaseCommandList.Where(a => a.GetYear() == countYear - 1).ToList();

                foreach (Incident incid in lastYeatCommandList)
                {
                    
                    Debug.Log("_0055 BAMB " + incident.UnicalId+ "   - " + incident.Id+ "  incid = "+ incid.Name);
                    if (new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupMissleList, incid))
                    {
                        
                        var secondIncident = new DictionaryEssence().BuildIncident(incid.GetTypeWeapon(), mainModel.CountYear);
                        incid.SetSecondIncident(secondIncident);
                        incid.SetTypeWeapon(GlobalParam.TypeEvent.AttackMissle);
                        Debug.Log("0055   SecondIncident = " + incid.SecondIncident);

                        return new IncidentAttack()
                        {
                            TypeEvent = GlobalParam.TypeEvent.AttackMissle,
                            SecondIncident = secondIncident
                        };
                    }
                    if (new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupBomberList, incid))
                    {
                        Debug.Log("_0055 BAMB!  **   - " + incident.Id+ "  SecondIncident = " + incid.SecondIncident);
                        var secondIncident = new DictionaryEssence().BuildIncident(incid.GetTypeWeapon(), mainModel.CountYear);
                        incid.SetSecondIncident(new DictionaryEssence().BuildIncident(incid.GetTypeWeapon(), mainModel.CountYear));
                        incid.SetTypeWeapon(GlobalParam.TypeEvent.AttackBomber);
                        return new IncidentAttack()
                        {
                            TypeEvent = GlobalParam.TypeEvent.AttackMissle,
                            SecondIncident = secondIncident
                        };
                    }
                }

            }
            return null;
        }
    }
}
