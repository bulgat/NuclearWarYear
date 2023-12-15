using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownCardInfo : MonoBehaviour
{
    private int Population { get; set; }

    private City TownCity { get; set; }
    private List<Sprite> FlagImageList { get; set; }
    Text infoText;
    public TownCardInfo()
    {
        this.Population = 44;
    }
    public void SetParam(List<Sprite> FlagImageList, City townCity)
    {
        this.TownCity = townCity;
        this.FlagImageList = FlagImageList;
    }
    void Start()
    {
        Image flagImage = gameObject.transform.GetChild(1).GetComponent<Image>();
        flagImage.sprite = this.FlagImageList[this.TownCity.FlagId];
        this.infoText = gameObject.transform.GetChild(2).GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        infoText.text = this.TownCity.CityTownModel.GetPopulation().ToString();
    }
}
