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
        public GlobalParam.TypeEvent ChangeIncidentCommand(CountryLider lider, GlobalParam.TypeEvent actionNameCommand, int countYear, MainModel mainModel)
        {
            //auto command player
            var typeCommand = lider.ReleaseCommandList?.FirstOrDefault();

            if (typeCommand == null)
            {
                actionNameCommand = GlobalParam.TypeEvent.Propaganda;

            }
            else
            {
                Debug.Log( "0769  lider __ command = " + typeCommand.GetName());
                actionNameCommand = typeCommand.GetName();

                var lastYeatCommandList0 = lider.ReleaseCommandList.Where(a => a.GetYear() == countYear).ToList();
                var lastYeatCommandList = lider.ReleaseCommandList.Where(a => a.GetYear() == countYear - 1).ToList();

                foreach (var item in lastYeatCommandList)
                {

                    if (item.Type == GlobalParam.TypeEvent.Missle)
                    {
                        item.SetTypeWeapon(GlobalParam.TypeEvent.AttackMissle);

                        return GlobalParam.TypeEvent.AttackMissle;
                    }
                    if (item.Type == GlobalParam.TypeEvent.Bomber)
                    {
                        item.SetTypeWeapon(GlobalParam.TypeEvent.AttackBomber);

                        return GlobalParam.TypeEvent.AttackBomber;
                    }

                }

            }
            return actionNameCommand;
        }
    }
}
