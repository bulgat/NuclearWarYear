using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class ViewManageWeapon
    {
        public void ManagerButton(MenuScript menuScript, MainModel _mainModel)
        {
            int flagId = _mainModel.GetCurrenFlagPlayer();
            CountryLider lider = _mainModel.GetLiderOne(flagId);
  
        }
    }
}
