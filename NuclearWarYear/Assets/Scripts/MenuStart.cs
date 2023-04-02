using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class MenuStart : MonoBehaviour
{
	public Button StartButton;
    public Button Start2PlayerButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(() => StartMethod(StartButton));
        Start2PlayerButton.onClick.AddListener(() => Start2PlayerMethod(Start2PlayerButton));
    }
	void StartMethod(Button buttonPressed)
	{
		//Application.LoadLevel("SampleScene");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void Start2PlayerMethod(Button buttonPressed)
    {
        SettingPlayer.TwoPlayerGame = true;
        //Application.LoadLevel("SampleScene");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void Update()
    {
        
    }
}
