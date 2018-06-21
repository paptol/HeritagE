using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour {

    public Button startButton, quitButton;


    // Use this for initialization
    void Start () {
        Button startBtn = startButton.GetComponent<Button>();
        startBtn.onClick.AddListener(MainMenu);
        Button quitBtn = startButton.GetComponent<Button>();
        quitBtn.onClick.AddListener(MainMenu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MainMenu ()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Hello");
    }

    void Quit ()
    {
        Application.Quit();
    }
}
