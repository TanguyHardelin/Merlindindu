using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public Button play;
	public Button exit;

	// Use this for initialization
	void Start () {
        play.onClick.AddListener(playOnClick);
        exit.onClick.AddListener(exitOnClick);
	}

	void playOnClick(){
		changeScene("MainBackup");
	}

	void exitOnClick(){
		Application.Quit();
	}

	void changeScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
