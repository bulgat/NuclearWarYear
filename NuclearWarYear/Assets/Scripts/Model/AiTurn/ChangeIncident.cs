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
        public GlobalParam.TypeEvent MutationIncidentCommand(
            CountryLider lider,
            GlobalParam.TypeEvent actionNameCommand,
            int countYear,
            MainModel mainModel)
        {
            //auto command player
            var typeCommand = lider.ReleaseCommandList?.FirstOrDefault();

            if (typeCommand == null)
            {
                actionNameCommand = GlobalParam.TypeEvent.Propaganda;

            }
            else
            {
                
                actionNameCommand = typeCommand.GetName();

                List<Incident> lastYeatCommandList = lider.ReleaseCommandList.Where(a => a.GetYear() == countYear - 1).ToList();

                foreach (Incident command in lastYeatCommandList)
                {
                    Debug.Log( "07069 id = "+ command.Id+ "_ command = " + command.GetTypeWeapon());
                    if (new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupMissleList, command))
                    {

                        command.SetSecondIncident(new DictionaryEssence().BuildIncident(command.GetTypeWeapon(), mainModel.CountYear));
                        command.SetTypeWeapon(GlobalParam.TypeEvent.AttackMissle);
                        Debug.Log("07070  "+ command.Id+ "  command    =  " + command.GetTypeWeapon());
                        return GlobalParam.TypeEvent.AttackMissle;
                    }
                    if (new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupBomberList, command))
                    {
                        command.SetSecondIncident(new DictionaryEssence().BuildIncident(command.GetTypeWeapon(), mainModel.CountYear));
                        command.SetTypeWeapon(GlobalParam.TypeEvent.AttackBomber);
                        Debug.Log("07071 "+ command.Id+ " command   =  " + command.GetTypeWeapon());
                        return GlobalParam.TypeEvent.AttackBomber;
                    }
                    
                }

            }
            return actionNameCommand;
        }
    }
}
