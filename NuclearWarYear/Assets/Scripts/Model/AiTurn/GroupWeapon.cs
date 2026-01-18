using Assets.Scripts.Model.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Progress;

namespace Assets.Scripts.Model.AiTurn
{
    public class GroupWeapon
    {
        public bool GroupWeaponPresence(List<GlobalParam.TypeEvent> groupWeaponList,Incident command)
        {
            return groupWeaponList.Any(a => a == command.Type);

        }

    }
}
