using Assets.Scripts.Model.param;
using UnityEngine;

public class UFOmodel : MonoBehaviour
{
    public GameObject UFO;
    public GameObject Stork;
    public GameObject Cow;
    public GameObject RocketRich;
    public GameObject Defectors;
    public GameObject Missle;
    public GameObject Rain;
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
        this.Missle.SetActive(false);
        this.Rain.SetActive(false);
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
            case GlobalParam.TypeEvent.AttackMissle:
                this.Missle.SetActive(true);
                break;
            case GlobalParam.TypeEvent.Rain:
                this.Rain.SetActive(true);
                break;
        }
    }
    void Update()
    {

    }
}
