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
    int IndexLidet;
    public void Init(List<Sprite> liderImageList,List<Sprite> flagImageList, MainModel mainModel,
        List<Sprite> iconCircleReadyList,int indexLidet)
    {
        this.LiderImageList = liderImageList;
        this.FlagImageList = flagImageList;
        this._mainModel = mainModel;
        this.IndexLidet = indexLidet;
        this.IconCircleReadyList = iconCircleReadyList;
    }
    public void ButtonLiderFrame()
    {
        var allImage_ar = GetComponentsInChildren<Image>();
        
       
        var LiderImage_1 = allImage_ar[1].GetComponent<Image>();
        var flagImage = allImage_ar[2].GetComponent<Image>();
        var circleReady = allImage_ar[4].GetComponent<Image>();
        LiderImage_1.sprite = LiderImageList[(IndexLidet*8) + _mainModel.CountryLiderList[IndexLidet].Mood];
        flagImage.sprite = FlagImageList[IndexLidet];
        circleReady.enabled = false;

        

        if (_mainModel.CountryLiderList[IndexLidet].GetCommandLider().GetVisibleBomber())
            
        {
            circleReady.enabled = true;
            circleReady.sprite = IconCircleReadyList[0];
        }
        if (_mainModel.CountryLiderList[IndexLidet].GetCommandLider().GetVisibleMissle())
        {
            circleReady.enabled = true;
            circleReady.sprite = IconCircleReadyList[1];
        }


        GetComponentInChildren<UnityEngine.UI.Text>().text = this._mainModel.CountryLiderList[IndexLidet].GetName() +
            " (" + _mainModel.CountryLiderList[IndexLidet].GetAllOwnPopulation() + ")";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}
