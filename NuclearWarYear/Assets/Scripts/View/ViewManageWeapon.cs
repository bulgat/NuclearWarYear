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
            /*
            menuScript.BomberButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Light bomber ("
                + lider.GetBomberCount() + ")";
            menuScript.BomberButton_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Heavy bomber ("
                + lider.GetBomberSpecCount(2) + ")";
            */
            /*
            var WingMissle = Instantiate(menuScript.CardWeapon, Vector3.zero, Quaternion.identity);
            WingMissle.transform.parent = menuScript.panelMain.transform;
            */
            /*
            if (lider.GetMissleCount()>0) {
                menuScript.MissleButton.gameObject.SetActive(true);
                menuScript.MissleButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Light missle (" + lider.GetMissleCount() + ")";
            } else
            {
                menuScript.MissleButton.gameObject.SetActive(false);
            }
            if (lider.GetMissleSpecCount(2)>0) {
                menuScript.MissleButton_1.gameObject.SetActive(true);
                menuScript.MissleButton_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Medium missl (" + lider.GetMissleSpecCount(2) + ")";
            } else
            {
                menuScript.MissleButton_1.gameObject.SetActive(false);
            }
            if (lider.GetMissleSpecCount(3) > 0)
            {
                menuScript.MissleButton_2.gameObject.SetActive(true);
                menuScript.MissleButton_2.GetComponentInChildren<UnityEngine.UI.Text>().text = "Heavy missle (" + lider.GetMissleSpecCount(3) + ")";
            } else
            {
                menuScript.MissleButton_2.gameObject.SetActive(false);
            }
            if (lider.GetMissleSpecCount(4) > 0)
            {
                menuScript.MissleButton_3.gameObject.SetActive(true);
                menuScript.MissleButton_3.GetComponentInChildren<UnityEngine.UI.Text>().text = "S Heavy miss (" + lider.GetMissleSpecCount(4) + ")";
            } else
            {
                menuScript.MissleButton_3.gameObject.SetActive(false);
            }
            */
            menuScript.DefenceButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Defence (" + lider.GetDefenceWeapon().Count() + ")";
        }
    }
}
