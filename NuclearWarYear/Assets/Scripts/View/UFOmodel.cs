using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UFOmodel : MonoBehaviour
{
    public GameObject UFO;
    public GameObject Stork;
    public GameObject Cow;
    public GameObject RocketRich;
    public GameObject Defectors;
    public GameObject Missle;
    private void Awake()
    {
    }
    void Start()
    {

    }
    public void SetVisible(string nameId)
    {
        this.UFO.SetActive(false);
        this.Stork.SetActive(false);
        this.Cow.SetActive(false);
        this.RocketRich.SetActive(false);
        this.Defectors.SetActive(false);
        this.Missle.SetActive(false);
        switch (nameId)
        {
            case "Ufo":
            this.UFO.SetActive(true);
            break;
            case "Baby":
            this.Stork.SetActive(true);
            break;
            case "CrazyCow":
                this.Cow.SetActive(true);
                break;
            case "RocketRich":
                this.RocketRich.SetActive(true);
                break;
            case "Defectors":
                this.Defectors.SetActive(true);
                break;
            case "AttackMissle":
                this.Missle.SetActive(true);
                break;
        }
    }
    void Update()
    {
        
    }
}
