using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ViewCardWeapon : MonoBehaviour,IPointerClickHandler
{
    public Text Text;
    System.Action<int> callback;
    void Start()
    {
        //gameObject.transform.onClick.AddListener(() => DefenceMethod(DefenceButton));
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // OnClick code goes here ...
Debug.Log("      turnBo > "  );
        this.callback(1);

    }
    void Update()
    {
        
    }
    public void SetText(string text)
    {
        Text.text = text;
    }
    public void SetCallback(System.Action<int> Callback)
    {
        this.callback = Callback;
    }
}
