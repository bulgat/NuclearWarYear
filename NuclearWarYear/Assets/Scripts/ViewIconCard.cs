using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewIconCard : MonoBehaviour
{
    private int Id;
    private List<Sprite> IconCardList;
    public Image SpriteIcon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetParam(List<Sprite> iconCardList, int id)
    {
        Debug.Log(iconCardList.Count+"  DoneMoveMadeCurrentPlayer = Ufo     =" + id);
        this.IconCardList = iconCardList;
        SpriteIcon.sprite = iconCardList[id];
        this.Id = id;
    }
}
