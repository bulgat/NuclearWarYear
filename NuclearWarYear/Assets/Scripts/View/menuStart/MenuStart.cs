using Assets.Scripts;
using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuStart : MonoBehaviour
{
	public Button StartButton;
    public Button Start2PlayerButton;

    void Start()
    {
        StartButton.onClick.AddListener(() => StartMethod());
        Start2PlayerButton.onClick.AddListener(() => Start2PlayerMethod());
    }
	void StartMethod()
	{
        SettingPlayer.TwoPlayerGame = false;
        SceneManager.LoadScene(GlobalParam.Scene.SelectLiderScene.ToString(), LoadSceneMode.Single);
    }
    void Start2PlayerMethod()
    {
        SettingPlayer.TwoPlayerGame = true;
        SceneManager.LoadScene(GlobalParam.Scene.GameScene.ToString(), LoadSceneMode.Single);
    }
    void Update()
    {
        
    }
}
