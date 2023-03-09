using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ViewLiderButton : MonoBehaviour, IPointerEnterHandler
{
    List<Sprite> LiderImageList;
    List<Sprite> FlagImageList;
    List<Sprite> IconCircleReadyList;
    MainModel _mainModel;
    //private int IndexLidet;
    CountryLider Lider;
    public void Init(List<Sprite> liderImageList,List<Sprite> flagImageList, MainModel mainModel,
        List<Sprite> iconCircleReadyList,CountryLider Lider)
    {
        this.LiderImageList = liderImageList;
        this.FlagImageList = flagImageList;
        this._mainModel = mainModel;
        //this.IndexLidet = indexLidet;
        this.Lider = Lider;
        this.IconCircleReadyList = iconCircleReadyList;
    }
    public void ButtonLiderFrame(int PlayerFlagId)
    {
        var allImage_ar = GetComponentsInChildren<Image>();
        
       
        var LiderImage_1 = allImage_ar[1].GetComponent<Image>();
        var flagImage = allImage_ar[2].GetComponent<Image>();
        var circleReady = allImage_ar[4].GetComponent<Image>();
   
        int indexLider = new LiderCountryHelper().GetLiderIndexWithFlag(this.Lider.FlagId);
        int moodLider = _mainModel.CountryLiderList[indexLider].GetMood(PlayerFlagId);
        
        var imageMood = 0;
        if (moodLider > 90)
        {
            imageMood = 2;
        }
        if (moodLider < 50)
        {
            imageMood = 5;
        }
        if (moodLider < 20)
        {
            imageMood = 7;
        }
        

        LiderImage_1.sprite = LiderImageList[(indexLider * 8) + imageMood];
        flagImage.sprite = FlagImageList[indexLider];
        circleReady.enabled = false;

        

        if (_mainModel.CountryLiderList[indexLider].GetCommandLider().GetVisibleBomber())
            
        {
            circleReady.enabled = true;
            circleReady.sprite = IconCircleReadyList[0];
        }
        if (_mainModel.CountryLiderList[indexLider].GetCommandLider().GetVisibleMissle())
        {
            circleReady.enabled = true;
            circleReady.sprite = IconCircleReadyList[1];
        }


        GetComponentInChildren<UnityEngine.UI.Text>().text = this.Lider.GetName() +
            " (" + this.Lider.GetAllOwnPopulation() + ")";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}
