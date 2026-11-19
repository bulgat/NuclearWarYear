using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model.param;
public class ViewNucklearBomber : MonoBehaviour
{
    GameObject Bomber;
    GameObject StelsBomber;
    // Start is called before the first frame update
    void Awake()
    {
        Bomber = gameObject.transform.GetChild(0).gameObject;
        StelsBomber = gameObject.transform.GetChild(1).gameObject;
        StelsBomber.SetActive(false);
        
    }

    public void SetParam(GlobalParam.TypeEvent nameCommand)
    {
        
        if (Bomber != null && StelsBomber != null)
        {

            Bomber.SetActive(false);
            StelsBomber.SetActive(false);
            if (nameCommand == GlobalParam.TypeEvent.Bomber)
            {
                Bomber.SetActive(true);
            }
            if (nameCommand == GlobalParam.TypeEvent.HeavyBomber)
            {
                StelsBomber.SetActive(true);
            }
        }
    }
    void Update()
    {
        
    }
}
