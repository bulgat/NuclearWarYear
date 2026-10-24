using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Model.paramTable;
public class TempMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject iconCard;

    void Start()
    {
        
    }
    public void SetMessage(string PrintMessage) {
        messageText.text = PrintMessage;
        StartCoroutine(HideMessage());
    }
    public void SetParam(List<Sprite> IconCardList, int IdMissle)
    {
        ViewIconCard viewIconCard = iconCard.GetComponent<ViewIconCard>();
        viewIconCard.SetParam(IconCardList, IdMissle);

    }

    private IEnumerator HideMessage() {
        yield return new WaitForSeconds(UIparam.lifeMessage);
        gameObject.SetActive(false);
    }
    void Update()
    {
        
    }
}
