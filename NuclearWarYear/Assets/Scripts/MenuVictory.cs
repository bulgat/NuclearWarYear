using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuVictory : MonoBehaviour
{
    public Button ReturnButton;
	
    // Start is called before the first frame update
    void Start()
    {
        ReturnButton.onClick.AddListener(() => ReturnAttackMethod(ReturnButton));
    }
	void ReturnAttackMethod(Button buttonPressed)
	{
		//Application.LoadLevel("MenuStart");
        SceneManager.LoadScene("MenuStart", LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
