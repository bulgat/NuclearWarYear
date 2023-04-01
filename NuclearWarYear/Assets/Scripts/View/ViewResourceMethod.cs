using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class ViewResourceMethod
    {
        public void ViewResourceMethodTable(MenuScript menuScript, List<Sprite> LiderImageList, List<Sprite> FlagImageList, MainModel _mainModel)
        {
            menuScript.CanvasResourcePlayer.SetActive(true);
            int flagId = _mainModel.GetCurrenFlagPlayer();
            CountryLider liderPlayer = _mainModel.GetLiderOne(flagId);
            int indexLider = liderPlayer.GraphicId;

            menuScript.CanvasResourcePlayerImageLider.sprite = LiderImageList[
                new ViewLiderHelper().GetNumberSpriteLider(indexLider, 0)];

            menuScript.CanvasResourceFlagImageLider.sprite = FlagImageList[
                flagId-1];

            menuScript.CanvasResourcePlayerTextLider.text = liderPlayer.GetName();

            menuScript.CanvasResourcePlayerPopulation.text =
                " population " + _mainModel.GetCountryLiderList()[4].GetAllOwnPopulation()
                + "\n missle " + _mainModel.GetCountryLiderList()[4].GetMissleCount()
                + "\n bomber " + _mainModel.GetCountryLiderList()[4].GetBomberCount()
                ;
        }
    }
}
