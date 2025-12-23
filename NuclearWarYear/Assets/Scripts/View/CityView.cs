using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CityView : MonoBehaviour
{
    public GameObject NuclearTown;
    GameObject NuclearExplode;
    public GameObject Shield;
    public GameObject AttackTarget;
    public List<Sprite> FlagImageList;

    public int FlagId = 0;
    [Header("View Id")]
    public int ViewId;
    private int _IdCity;
    private List<CityView> _TownList;
    private bool _visibleLabel;

    private Action<int> _SelectCityTargetIdPlayer;
    private List<Sprite> _TownSpriteList;
    public CityModel CityTownModel;

    public Animator animator;
    private void Await()
    {

    }

    void Start()
    {
        AttackTarget.SetActive(false);
        NuclearExplode = gameObject.transform.GetChild(3).gameObject;
        animator = gameObject.transform.GetChild(3).GetComponent<Animator>();

        if (animator)
        {
            animator.enabled = false;
        }
        NuclearExplode.SetActive(false);
    }

    void Update()
    {
        ChangeViewTown();
        Shield.SetActive(false);
    }
    void OnMouseDown()
    {
    }
    public void SetCityModelView(CityModel cityModel)
    {
        this.CityTownModel = cityModel;
        this.FlagId = cityModel.FlagId - 1;
    }
    public void ChangeViewTown()
    {
        var spriteTown = NuclearTown.GetComponent<SpriteRenderer>();
        if (CityTownModel != null)
        {
            if (CityTownModel.GetPopulation() >= 40)
            {
                spriteTown.sprite = _TownSpriteList[0];
                return;
            }
            if (CityTownModel.GetPopulation() > 30)
            {
                spriteTown.sprite = _TownSpriteList[1];
                return;
            }
            if (CityTownModel.GetPopulation() > 20)
            {
                spriteTown.sprite = _TownSpriteList[2];
                return;
            }
            if (CityTownModel.GetPopulation() > 10)
            {
                spriteTown.sprite = _TownSpriteList[3];
                return;
            }
            if (CityTownModel.GetPopulation() > 0)
            {
                spriteTown.sprite = _TownSpriteList[4];
                return;
            }
            spriteTown.sprite = _TownSpriteList[5];
        }
    }
    public void ClearTargetAim()
    {
        AttackTarget.SetActive(false);
    }
    public void SetTargetAim(bool Visible)
    {
        AttackTarget.SetActive(Visible);
    }
    public int GetId()
    {
        return _IdCity;

    }
    public void SetId(int Id, Action<int> SelectCityTargetIdPlayer, List<Sprite> TownSpriteList)
    {
        _IdCity = Id;
        _SelectCityTargetIdPlayer = SelectCityTargetIdPlayer;

        _TownSpriteList = TownSpriteList;

    }

    public void SetVisibleLabel(bool Visible)
    {
        _visibleLabel = Visible;
    }
    public void SetVisibleExplode(bool Visible)
    {



        NuclearExplode.SetActive(Visible);
        animator.enabled = Visible;
        if (Visible)
        {
            Debug.Log("078 NuclearExplode Population   = ");
            animator.Play(0);
        }
    }
    public void SetVisibleShild(bool Visible)
    {
        Shield.SetActive(Visible);
    }
}
