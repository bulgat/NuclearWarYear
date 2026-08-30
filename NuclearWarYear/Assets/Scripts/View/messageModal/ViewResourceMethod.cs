using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class ViewResourceMethod:ViewResourcуBase
    {
        public override void SetResourceMethodTable(
            MenuScript menuScript,
            List<Sprite> LiderImageList,
            List<Sprite> FlagImageList,
            MainModel _mainModel)
        {

            Debug.Log("0055  -   inc flagId = " + _mainModel.GetCurrenFlagPlayer().Name);
            //int flagId = _mainModel.GetCurrenFlagPlayer().FlagId;
            CountryLider liderPlayer = _mainModel.GetCurrenFlagPlayer();
   
            var imageLider = gameObject.transform.GetChild(0).GetChild(1);

            Image image = imageLider.GetComponent<Image>();
            if (image != null)
            {
                image.sprite = LiderImageList[
                    new ViewLiderHelper().GetNumberSpriteLider(liderPlayer.GetIndexLider(), 0)];
            }
            Debug.Log("0056   SecondIncide  = " + FlagImageList.Count);

            //if (gameObject.transform.childCount > 4)
            //{
            var childFlag = gameObject.transform.GetChild(0).GetChild(5);
                Transform flagLider = childFlag;
                flagLider.GetComponent<Image>().sprite = FlagImageList[
                     liderPlayer.FlagId-1];
            //}

            var textLider = gameObject.transform.GetChild(0).GetChild(3);
            if (textLider != null)
            {
                
                textLider.GetComponent<Text>().text = liderPlayer.Name;
            }
 
            SetMessage(
                " population " + _mainModel.GetCountryLiderList()[liderPlayer.GetIndexLider()].GetAllOwnPopulation()
                + "\n missle " + _mainModel.GetCountryLiderList()[liderPlayer.GetIndexLider()].GetMissleCount()
                + "\n bomber " + _mainModel.GetCountryLiderList()[liderPlayer.GetIndexLider()].GetBomberCount()
                + "\n defence " + _mainModel.GetCountryLiderList()[liderPlayer.GetIndexLider()].GetDefenceWeapon().Count()
                );
        }
        
    }
}
