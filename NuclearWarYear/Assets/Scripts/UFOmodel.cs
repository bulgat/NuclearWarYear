using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UFOmodel : MonoBehaviour
{
    private GameObject UFO;
    private GameObject Stork;
    private GameObject Cow;
    private GameObject RocketRich;
    private GameObject Defectors;
    private GameObject Missle;
    private void Awake()
    {
        Debug.Log("T  I  ------- ind > " );
        this.UFO = gameObject.transform.GetChild(0).gameObject;
        this.Stork = gameObject.transform.GetChild(1).gameObject;
        this.Cow = gameObject.transform.GetChild(2).gameObject;
        this.RocketRich = gameObject.transform.GetChild(3).gameObject;
        this.Defectors = gameObject.transform.GetChild(4).gameObject;
        this.Missle = gameObject.transform.GetChild(5).gameObject;
    }
    void Start()
    {
        
        
    }
    public void SetVisible(string Id)
    {
        this.UFO.SetActive(false);
        this.Stork.SetActive(false);
        this.Cow.SetActive(false);
        this.RocketRich.SetActive(false);
        this.Defectors.SetActive(false);
        this.Missle.SetActive(false);
        switch (Id)
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
    // Update is called once per frame
    void Update()
    {
        
    }
}