using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public Button play;
	public Button exit;
    public string sceneNane;

	// Use this for initialization
	void Start () {
        play.onClick.AddListener(playOnClick);
        exit.onClick.AddListener(exitOnClick);
	}

	void playOnClick(){
		changeScene(sceneNane);
	}

	void exitOnClick(){
		Application.Quit();
	}

	void changeScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
