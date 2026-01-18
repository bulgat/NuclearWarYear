using Assets.Scripts.Model.paramTable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownCardInfo : MonoBehaviour
{

    private CityView TownCity { get; set; }
    private List<Sprite> FlagImageList { get; set; }
    Text InfoText;
    private TMP_Text NameCityText { get; set; }

    public TownCardInfo()
    {
    }
    public void SetParam(List<Sprite> FlagImageList, CityView townCity)
    {
        this.TownCity = townCity;
        this.FlagImageList = FlagImageList;
    }
    void Start()
    {
        Image flagImage = gameObject.transform.GetChild(1).GetComponent<Image>();
        flagImage.sprite = this.FlagImageList[this.TownCity.FlagId];
        this.InfoText = gameObject.transform.GetChild(2).GetComponent<Text>();
        this.NameCityText = gameObject.transform.GetChild(3).GetComponent<TMP_Text>();
        gameObject.transform.localScale = new Vector2(UIparam.TownCardSize.x, UIparam.TownCardSize.y);
    }

    void Update()
    {
        InfoText.text = this.TownCity.CityTownModel.GetPopulation().ToString();
        this.NameCityText.text = this.TownCity.CityTownModel.Name;    
    }
}
