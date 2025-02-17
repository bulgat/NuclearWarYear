using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

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
        Debug.Log("   " + message + "  For = " );
        var textMessage = gameObject.transform.GetChild(0).GetChild(2);

        textMessage.GetComponent<Text>().text = message;
    }

    void CanvasReportButtonCloseMethod()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
