using Assets.Scripts.Model.param;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Model.AiTurn
{
    public class GroupWeapon
    {
        public bool GroupWeaponPresence(List<GlobalParam.TypeEvent> groupWeaponList,Incident command)
        {
            return groupWeaponList.Any(a => a == command.Name);

        }

    }
}
