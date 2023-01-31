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
    private int IndexLidet;
    public void Init(List<Sprite> liderImageList,List<Sprite> flagImageList, MainModel mainModel,
        List<Sprite> iconCircleReadyList,int indexLidet)
    {
        this.LiderImageList = liderImageList;
        this.FlagImageList = flagImageList;
        this._mainModel = mainModel;
        this.IndexLidet = indexLidet;
        this.IconCircleReadyList = iconCircleReadyList;
    }
    public void ButtonLiderFrame(int PlayerFlagId)
    {
        var allImage_ar = GetComponentsInChildren<Image>();
        
       
        var LiderImage_1 = allImage_ar[1].GetComponent<Image>();
        var flagImage = allImage_ar[2].GetComponent<Image>();
        var circleReady = allImage_ar[4].GetComponent<Image>();
        Debug.Log("IndexLidet = " + this.IndexLidet + "  PlayerFlagId =" + PlayerFlagId+"   L = "+ this._mainModel.CountryLiderList.Count);

        var moodLider = _mainModel.CountryLiderList[PlayerFlagId].GetMood(this.IndexLidet + 1);
        Debug.Log("   --- L = " + moodLider + "   AIf  hAct   = " + moodLider);
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
        

        Debug.Log("____ mood = " + _mainModel.CountryLiderList[PlayerFlagId].GetMood(this.IndexLidet + 1));
        //LiderImage_1.sprite = LiderImageList[(IndexLidet*8) + _mainModel.CountryLiderList[IndexLidet].GetMood()];
        LiderImage_1.sprite = LiderImageList[(this.IndexLidet * 8) + imageMood];
        flagImage.sprite = FlagImageList[this.IndexLidet];
        circleReady.enabled = false;

        

        if (_mainModel.CountryLiderList[this.IndexLidet].GetCommandLider().GetVisibleBomber())
            
        {
            circleReady.enabled = true;
            circleReady.sprite = IconCircleReadyList[0];
        }
        if (_mainModel.CountryLiderList[this.IndexLidet].GetCommandLider().GetVisibleMissle())
        {
            circleReady.enabled = true;
            circleReady.sprite = IconCircleReadyList[1];
        }


        GetComponentInChildren<UnityEngine.UI.Text>().text = this._mainModel.CountryLiderList[this.IndexLidet].GetName() +
            " (" + this._mainModel.CountryLiderList[this.IndexLidet].GetAllOwnPopulation() + ")";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}
