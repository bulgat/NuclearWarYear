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

            int flagId = _mainModel.GetCurrenFlagPlayer();
            CountryLider liderPlayer = _mainModel.GetLiderOne(flagId);
   
            var imageLider = gameObject.transform.GetChild(0).GetChild(1);

            Image image = imageLider.GetComponent<Image>();
            if (image != null)
            {
                Debug.Log(liderPlayer.GraphicId + "  liderP  = " + flagId+"__"+ new ViewLiderHelper().GetNumberSpriteLider(liderPlayer.GraphicId, 0));
                image.sprite = LiderImageList[
                    new ViewLiderHelper().GetNumberSpriteLider(liderPlayer.GraphicId, 0)];
            }


            if (gameObject.transform.childCount > 4)
            {
                var childFlag = gameObject.transform.GetChild(0).GetChild(5);
                Transform flagLider = childFlag;
                flagLider.GetComponent<Image>().sprite = FlagImageList[
                     flagId - 1];
            }

            var textLider = gameObject.transform.GetChild(0).GetChild(3);
            if (textLider != null)
            {
                
                textLider.GetComponent<Text>().text = liderPlayer.Name;
            }
 
            SetMessage(
                " population " + _mainModel.GetCountryLiderList()[4].GetAllOwnPopulation()
                + "\n missle " + _mainModel.GetCountryLiderList()[4].GetMissleCount()
                + "\n bomber " + _mainModel.GetCountryLiderList()[4].GetBomberCount()
                );
        }
        
    }
}
