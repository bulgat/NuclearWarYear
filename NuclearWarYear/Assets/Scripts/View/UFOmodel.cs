using Assets.Scripts.Model.param;
using UnityEngine;

public class UFOmodel : MonoBehaviour
{
    public GameObject UFO;
    public GameObject Stork;
    public GameObject Cow;
    public GameObject RocketRich;
    public GameObject Defectors;
    public GameObject Rain;
    public GameObject Missle;
    public GameObject HeavyMissle;
    public GameObject SuperMissle;
    public GameObject MegaMissle;

    private void Awake()
    {
    }
    void Start()
    {

    }
    public void SetVisible(GlobalParam.TypeEvent nameId)
    {
        this.UFO.SetActive(false);
        this.Stork.SetActive(false);
        this.Cow.SetActive(false);
        this.RocketRich.SetActive(false);
        this.Defectors.SetActive(false);
        this.Rain.SetActive(false);
        this.Missle.SetActive(false);
        this.HeavyMissle.SetActive(false);
        this.SuperMissle.SetActive(false);
        this.MegaMissle.SetActive(false);

        switch (nameId)
        {
            case GlobalParam.TypeEvent.Ufo:
                this.UFO.SetActive(true);
                break;
            case GlobalParam.TypeEvent.Baby:
                this.Stork.SetActive(true);
                break;
            case GlobalParam.TypeEvent.CrazyCow:
                this.Cow.SetActive(true);
                break;
            case GlobalParam.TypeEvent.RocketRich:
                this.RocketRich.SetActive(true);
                break;
            case GlobalParam.TypeEvent.Defectors:
                this.Defectors.SetActive(true);
                break;
            case GlobalParam.TypeEvent.Rain:
                this.Rain.SetActive(true);
                break;
            case GlobalParam.TypeEvent.Missle:
                this.Missle.SetActive(true);
                break;
            case GlobalParam.TypeEvent.HeavyMissle:
                this.HeavyMissle.SetActive(true);
                break;
            case GlobalParam.TypeEvent.SuperMissle:
                this.SuperMissle.SetActive(true);
                break;
            case GlobalParam.TypeEvent.MegaMissle:
                this.MegaMissle.SetActive(true);
                break;
        }
    }
    void Update()
    {

    }
}
