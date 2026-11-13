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
        Debug.Log("0410    Lider "+ Bomber + "opag " + nameCommand);
        if (Bomber != null && StelsBomber != null)
        {
            Debug.Log("0411    Lider  Pr  " + (nameCommand == GlobalParam.TypeEvent.HeavyBomber));
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
