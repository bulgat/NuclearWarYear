using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.View
{
    public class ViewManageWeapon
    {
        public void ManagerButton(MenuScript menuScript, MainModel _mainModel)
        {
            int flagId = _mainModel.GetCurrenFlagPlayer();
            CountryLider lider = _mainModel.GetLiderOne(flagId);

            menuScript.BomberButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Light bomber ("
                + lider.GetBomberCount() + ")";
            menuScript.BomberButton_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Heavy bomber ("
                + lider.GetBomberSpecCount(2) + ")";

            menuScript.MissleButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Light missle (" + lider.GetMissleCount() + ")";
            menuScript.MissleButton_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Medium missl (" + lider.GetMissleSpecCount(2) + ")";
            menuScript.MissleButton_2.GetComponentInChildren<UnityEngine.UI.Text>().text = "Heavy missle (" + lider.GetMissleSpecCount(3) + ")";
            menuScript.MissleButton_3.GetComponentInChildren<UnityEngine.UI.Text>().text = "S Heavy miss (" + lider.GetMissleSpecCount(4) + ")";

            menuScript.DefenceButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Defence (" + lider.GetDefenceWeapon().Count() + ")";
        }
    }
}
