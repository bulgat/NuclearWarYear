using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.View;

public class ViewLiderButton : MonoBehaviour, IPointerEnterHandler
{
    List<Sprite> LiderImageList;
    List<Sprite> FlagImageList;
    List<Sprite> IconCircleReadyList;
    List<Sprite> IconCardList;
    MainModel _mainModel;
    CountryLider Lider;
    public void Init(List<Sprite> liderImageList,List<Sprite> flagImageList, MainModel mainModel,
        List<Sprite> iconCircleReadyList, List<Sprite> iconCardList,CountryLider Lider)
    {
        this.LiderImageList = liderImageList;
        this.FlagImageList = flagImageList;
        this._mainModel = mainModel;
        this.Lider = Lider;
        this.IconCircleReadyList = iconCircleReadyList;
        this.IconCardList = iconCardList;
    }
    public void ButtonLiderFrame(int PlayerFlagId)
    {
        
        var allImage_ar = GetComponentsInChildren<Image>();


        var LiderImage_1 = allImage_ar[1].GetComponent<Image>();
        var flagImage = allImage_ar[2].GetComponent<Image>();
        var circleReady = allImage_ar[4].GetComponent<Image>();
   
        int indexLider = this.Lider.GraphicId;
        CountryLider countryLider = _mainModel.CountryLiderList[indexLider];
        int moodLider = countryLider.GetMood(PlayerFlagId);
        
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

        //ViewLiderHelper kol;

       // LiderImage_1.sprite = LiderImageList[(indexLider * 8) + imageMood];
        LiderImage_1.sprite = LiderImageList[new ViewLiderHelper().GetNumberSpriteLider(indexLider, imageMood)];
        flagImage.sprite = FlagImageList[indexLider];
        circleReady.enabled = false;

        

        if (countryLider.GetCommandLider().GetVisibleBomber())
        {
            if (this.IconCircleReadyList!=null)
            {
                circleReady.enabled = true;
                circleReady.sprite = this.IconCircleReadyList[0];
            }
        }
        if (countryLider.GetCommandLider().GetVisibleMissle())
        {
            if (this.IconCircleReadyList != null)
            {
                circleReady.enabled = true;
                circleReady.sprite = IconCircleReadyList[1];
            }
        }


        GetComponentInChildren<UnityEngine.UI.Text>().text = this.Lider.GetName() +
            " (" + this.Lider.GetAllOwnPopulation() + ")";
        
        Debug.Log( "  L  =" + countryLider.GetEventTotalTurn().IdEvent);

        if (this.IconCardList != null)
        {
            var imageEvent = gameObject.transform.GetChild(6).GetComponent<Image>();
            imageEvent.sprite = this.IconCardList[countryLider.GetEventTotalTurn().IdEvent];
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}
