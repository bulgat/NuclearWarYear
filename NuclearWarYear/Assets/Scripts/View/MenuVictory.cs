using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Model.param;

public class MenuVictory : MonoBehaviour
{
    public Button ReturnButton;
	
    void Start()
    {
        ReturnButton.onClick.AddListener(() => ReturnAttackMethod(ReturnButton));
    }
	void ReturnAttackMethod(Button buttonPressed)
	{
        SceneManager.LoadScene(GlobalParam.Scene.MenuStart.ToString(), LoadSceneMode.Single);
    }

    void Update()
    {
        
    }
}
