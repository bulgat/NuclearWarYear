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
        Debug.Log("0802  tackBo= "  );
        this.callback(this._incident);
        StartCoroutine(MoveCardToCenter());
    }

    IEnumerator MoveCardToCenter()
    {
        Vector3 startPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, startPos.z);
        Vector3 startScale = gameObject.transform.localScale;
        Vector3 targetScale = UIparam.CardSelectScale;
        Quaternion startRot = gameObject.transform.rotation;
        // Рандомный лёгкий поворот.
        Quaternion targetRot = startRot * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-8f, 8f));

        float duration = 0.3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            // Плавная кривая (ease-out).
            t = 1f - Mathf.Pow(1f - t, 3f);

            gameObject.transform.position = Vector3.Lerp(startPos, targetPos, t);
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            gameObject.transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
            yield return null;
        }

        gameObject.transform.position = targetPos;
        gameObject.transform.localScale = targetScale;
        gameObject.transform.rotation = targetRot;
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
