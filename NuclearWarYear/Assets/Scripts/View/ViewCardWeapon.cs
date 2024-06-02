using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Model.param;
public class ViewCardWeapon : MonoBehaviour,IPointerClickHandler
{
    private int Id;
    public Text Text;
    System.Action<int> callback;
    private List<Sprite> IconCardList;
    public Image SpriteIcon;
    void Start()
    {
        //gameObject.transform.onClick.AddListener(() => DefenceMethod(DefenceButton));
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // OnClick code goes here ...

        this.callback(this.Id);
        gameObject.transform.localScale = new Vector2(2, 2);
    }
    void Update()
    {
        
    }
    public void SetParam(GlobalParam.TypeEvent text, List<Sprite> iconCardList,int id)
    {
        Text.text = text.ToString();

        var iconCard = gameObject.transform.GetChild(1);
        ViewIconCard viewIconCard = iconCard.GetComponent<ViewIconCard>();
        viewIconCard.SetParam(iconCardList,id);

        this.Id = id;
    }
    public void SetCallback(System.Action<int> Callback)
    {
        

        this.callback = Callback;
    }
}
