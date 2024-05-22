using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class ViewResourcуBase : MonoBehaviour
    {
        public Button ButtonCloseResource;
        public Text textPopulation;
        private void Start()
        {
            ButtonCloseResource.onClick.AddListener(() => ButtonCloseResourceMethod(ButtonCloseResource));
        }
        public virtual void SetResourceMethodTable(MenuScript menuScript, List<Sprite> LiderImageList, List<Sprite> FlagImageList, MainModel _mainModel)
        {

  
        }
        public void SetMessage(string message)
        {
            //var textPopulation = gameObject.transform.GetChild(4);

            textPopulation.GetComponent<Text>().text = message;
        }
        void ButtonCloseResourceMethod(Button buttonCloseResource)
        {
            Destroy(gameObject);
        }
    }
}
