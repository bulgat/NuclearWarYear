using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject iconCard;

    void Start()
    {
        
    }
    public void SetMessage(string PrintMessage) {
        messageText.text = PrintMessage;
    }
    public void SetParam(List<Sprite> IconCardList, int IdMissle)
    {

        //var iconCard = gameObject.transform.GetChild(0).GetChild(1);
        ViewIconCard viewIconCard = iconCard.GetComponent<ViewIconCard>();
        viewIconCard.SetParam(IconCardList, IdMissle);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
