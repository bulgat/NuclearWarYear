using Assets.Scripts.Model.param;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLiderScene : MonoBehaviour
{
    public Button StartButton;
    void Start()
    {
        StartButton.onClick.AddListener(() => StartMethod());
    }
    void StartMethod() {
        Debug.Log("0657  Crea  = ");
        SceneManager.LoadScene(GlobalParam.Scene.GameScene.ToString(), LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
