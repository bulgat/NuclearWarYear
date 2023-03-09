using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
	public Button StartButton;
	
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(() => StartAttackMethod(StartButton));
    }
	void StartAttackMethod(Button buttonPressed)
	{
		//Application.LoadLevel("SampleScene");
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    void Update()
    {
        
    }
}
