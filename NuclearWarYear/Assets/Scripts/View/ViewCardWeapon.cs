using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
Debug.Log("      turnB > "  );
        this.callback(this.Id);
        gameObject.transform.localScale = new Vector2(2, 2);
    }
    void Update()
    {
        
    }
    public void SetParam(string text, List<Sprite> iconCardList,int id)
    {
        Text.text = text;
        this.IconCardList = iconCardList;
        SpriteIcon.sprite = iconCardList[id];
        this.Id = id;
    }
    public void SetCallback(System.Action<int> Callback)
    {
        

        this.callback = Callback;
    }
}
