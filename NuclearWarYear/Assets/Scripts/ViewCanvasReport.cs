using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCanvasReport : MonoBehaviour
{
    public Button CanvasReportButtonClose;
    void Start()
    {
        Transform reportButtonClose = gameObject.transform.GetChild(0).GetChild(3);
        CanvasReportButtonClose = reportButtonClose.GetComponent<Button>();
        CanvasReportButtonClose.onClick.AddListener(() => CanvasReportButtonCloseMethod());
    }

    public void SetParam(List<Sprite> IconCardList,int IdMissle)
    {
        
        var iconCard = gameObject.transform.GetChild(0).GetChild(1);
        ViewIconCard viewIconCard = iconCard.GetComponent<ViewIconCard>();
        viewIconCard.SetParam(IconCardList, IdMissle);
        
    }
    public void SetMessage(string message)
    {
    Debug.Log("   = "+ gameObject);    
        var textMessage = gameObject.transform.GetChild(0).GetChild(2);

        textMessage.GetComponent<Text>().text = message;
    }

    void CanvasReportButtonCloseMethod()
    {
        //CanvasReport.SetActive(false);
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}