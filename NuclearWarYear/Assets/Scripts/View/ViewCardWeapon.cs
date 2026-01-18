using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model;

public class ViewCardWeapon : MonoBehaviour,IPointerClickHandler
{
    private Incident _incident;
    public Text Text;
    System.Action<Incident> callback;
    private List<Sprite> IconCardList;
    public Image SpriteIcon;
    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        this.callback(this._incident);
        gameObject.transform.localScale = new Vector2(2, 2);
    }
    void Update()
    {
        
    }
    public void SetParam( List<Sprite> iconCardList, Incident incident)
    {

        if (incident != null)
        {
            Text.text = incident.GetName().ToString();

            var iconCard = gameObject.transform.GetChild(1);
            ViewIconCard viewIconCard = iconCard.GetComponent<ViewIconCard>();

            viewIconCard.SetParam(iconCardList, incident.GetImageId());

            this._incident = incident;
        }
    }
    public void SetCallback(System.Action<Incident> Callback)
    {
        

        this.callback = Callback;
    }
}
