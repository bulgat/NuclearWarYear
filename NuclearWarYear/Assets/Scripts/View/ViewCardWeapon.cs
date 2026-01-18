using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model;
using Assets.Scripts.Model.paramTable;

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
        gameObject.transform.localScale = UIparam.CardSelectScale;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, Screen.height/UIparam.CardSelectCoefHeight);

    }
    void Update()
    {
        
    }
    public void SetParam( List<Sprite> iconCardList, Incident incident)
    {

        if (incident != null)
        {
            Text.text = incident.GetName().ToString() +(incident.Damage>0 ? " Damage: " + incident.Damage:"");

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
