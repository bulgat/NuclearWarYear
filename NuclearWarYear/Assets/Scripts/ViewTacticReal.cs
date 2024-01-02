using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewTacticReal : MonoBehaviour
{
    List<Sprite> _IconCardList;
    List<Sprite> _FlagImageList;
    public void Init(List<Sprite> FlagImageList,List<Sprite> IconCardList)
    {
        this._FlagImageList = FlagImageList;
        this._IconCardList = IconCardList;
    }
 
    public void CanvasTacticRealSetText(string InfoText, int FlagIndex,int IdImage, List<Sprite> LiderImageList, MainModel mainModel, int indexLider)
    {
        
        gameObject.transform.GetChild(0).GetChild(1).GetComponentInChildren<UnityEngine.UI.Text>().text = InfoText;
        ViewIconCard viewIconCard = gameObject.transform.GetChild(0).GetChild(2).GetComponent<ViewIconCard>();
        viewIconCard.SetParam(this._IconCardList, IdImage);

        ViewIconCard viewIconCard0 = gameObject.transform.GetChild(0).GetChild(3).GetComponent<ViewIconCard>();
        viewIconCard0.SetParam(this._FlagImageList, FlagIndex);

        
        
        ViewLiderButton viewLiderButton = gameObject.transform.GetChild(0).GetChild(4).GetComponent<ViewLiderButton>();
        

        viewLiderButton.Init(LiderImageList, this._FlagImageList, mainModel,
            null, 
            null,
            mainModel.GetCountryLiderList()[indexLider]);

        viewLiderButton.ButtonLiderFrame(mainModel.GetCurrentPlayerFlag());
    }
}
