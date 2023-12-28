using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class ViewResourceMethod : MonoBehaviour
    {
        public Button ButtonCloseResource;
        private void Start()
        {
            ButtonCloseResource.onClick.AddListener(() => ButtonCloseResourceMethod(ButtonCloseResource));
        }
        public void SetResourceMethodTable(MenuScript menuScript, List<Sprite> LiderImageList, List<Sprite> FlagImageList, MainModel _mainModel)
        {

            int flagId = _mainModel.GetCurrenFlagPlayer();
            CountryLider liderPlayer = _mainModel.GetLiderOne(flagId);
            //int indexLider = liderPlayer.GraphicId;

            var imageLider = gameObject.transform.GetChild(1);

            imageLider.GetComponent<Image>().sprite = LiderImageList[
                new ViewLiderHelper().GetNumberSpriteLider(liderPlayer.GraphicId, 0)];

            var flagLider = gameObject.transform.GetChild(5);

            flagLider.GetComponent<Image>().sprite = FlagImageList[
                flagId-1];

            var textLider = gameObject.transform.GetChild(3);

            textLider.GetComponent<Text>().text = liderPlayer.GetName();

            var textPopulation = gameObject.transform.GetChild(4);

            textPopulation.GetComponent<Text>().text =
                " population " + _mainModel.GetCountryLiderList()[4].GetAllOwnPopulation()
                + "\n missle " + _mainModel.GetCountryLiderList()[4].GetMissleCount()
                + "\n bomber " + _mainModel.GetCountryLiderList()[4].GetBomberCount()
                ;
        }
        void ButtonCloseResourceMethod(Button buttonCloseResource)
        {
            //CanvasResourcePlayer.SetActive(false);
            //GameObject CanResPlayer = Instantiate(CanResPlayerPrefabs, new Vector2(100, 100), Quaternion.identity);
            //CanResPlayer.transform.parent = panelMain.transform;
            Destroy(gameObject);
        }
    }
}
