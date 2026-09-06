using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ViewCircleReady : MonoBehaviour
{
    public Image CircleReady;
    public TMP_Text Text;
   public void SetParam(bool Visible, Sprite imageCircle, Incident incident)
    {
        Debug.Log("0410    Lider  Propaganda "+ CircleReady);
        gameObject.SetActive(Visible);
        //CircleReady.enabled = Visible;
        CircleReady.sprite = imageCircle;
        //CircleReady.sprite = IconCircleReadyList[IndexImage];
        Debug.Log("0411  CreateComm = "+ imageCircle);
        if (incident != null)
        {
            Text.text = incident.Damage+" mega";
        }
    }
}
