using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuVictory : MonoBehaviour
{
    public Button ReturnButton;
	
    void Start()
    {
        ReturnButton.onClick.AddListener(() => ReturnAttackMethod(ReturnButton));
    }
	void ReturnAttackMethod(Button buttonPressed)
	{
        SceneManager.LoadScene("MenuStart", LoadSceneMode.Single);
    }

    void Update()
    {
        
    }
}
