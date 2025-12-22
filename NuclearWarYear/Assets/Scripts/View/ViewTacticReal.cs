using Assets.Scripts.Model.param;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ViewTacticReal : MonoBehaviour
{
    List<Sprite> _IconCardList;
    List<Sprite> _FlagImageList;
    List<GameObject> _TownViewList;
    List<GameObject> _UICardTownList;
int canvasIndex = 1;
    public void Init(List<Sprite> FlagImageList,List<Sprite> IconCardList, List<GameObject> TownViewList, List<GameObject> UICardTownList)
    {
        this._FlagImageList = FlagImageList;
        this._IconCardList = IconCardList;
        this._TownViewList = TownViewList;
        this._UICardTownList = UICardTownList;
    }
 
    public void CanvasTacticRealSetText(string InfoText, int FlagIndex,int IdImage,
        List<Sprite> LiderImageList, MainModel mainModel, int indexLider)
    {

        gameObject.transform.GetChild(canvasIndex).GetChild(1).GetComponentInChildren<UnityEngine.UI.Text>().text = InfoText;
        ViewIconCard viewIconCard = gameObject.transform.GetChild(canvasIndex).GetChild(2).GetComponent<ViewIconCard>();
        viewIconCard.SetParam(this._IconCardList, IdImage);

        ViewIconCard viewIconCard0 = gameObject.transform.GetChild(canvasIndex).GetChild(3).GetComponent<ViewIconCard>();
        viewIconCard0.SetParam(this._FlagImageList, FlagIndex);
        
        ViewLiderButton viewLiderButton = gameObject.transform.GetChild(canvasIndex).GetChild(4).GetComponent<ViewLiderButton>();

        viewLiderButton.gameObject.SetActive(FlagIndex != GlobalParam.StartTurnIdFlag);

        viewLiderButton.Init(LiderImageList, this._FlagImageList, mainModel,
            null, 
            null,
            mainModel.GetCountryLiderList()[indexLider]);

        viewLiderButton.ButtonLiderFrame(mainModel.GetCurrentPlayerFlag());
    }
    private void Update()
    {
        DrawTownInfoList();
    }
    void DrawTownInfoList()
    {
        float h = gameObject.GetComponent<RectTransform>().rect.height;
        int count = 0;

        if (this._TownViewList != null)
        {
            foreach (var town in this._TownViewList)
            {
                Vector3 coordinates = Camera.main.WorldToScreenPoint(town.transform.position);
                this._UICardTownList[count].transform.SetParent(gameObject.transform.GetChild(0));
                this._UICardTownList[count].transform.position = new Vector3(coordinates.x, coordinates.y - h / 10, coordinates.z);

                //Population
                count++;
            }
        }

    }
}
