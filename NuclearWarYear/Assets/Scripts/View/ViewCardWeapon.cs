using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model;

public class ViewCardWeapon : MonoBehaviour,IPointerClickHandler
{
    private IWeapon Id;
    public Text Text;
    System.Action<IWeapon> callback;
    private List<Sprite> IconCardList;
    public Image SpriteIcon;
    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        this.callback(this.Id);
        gameObject.transform.localScale = new Vector2(2, 2);
    }
    void Update()
    {
        
    }
    public void SetParam( List<Sprite> iconCardList, IWeapon item)
    {
        Text.text = item.GetName().ToString();

        var iconCard = gameObject.transform.GetChild(1);
        ViewIconCard viewIconCard = iconCard.GetComponent<ViewIconCard>();
        
        viewIconCard.SetParam(iconCardList, item.GetImageId());

        this.Id = item;
    }
    public void SetCallback(System.Action<IWeapon> Callback)
    {
        

        this.callback = Callback;
    }
}
