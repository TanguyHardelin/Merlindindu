using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string sceneName = "Main"; // Change the scene name inside the Unity UI
    public Scene mainScene;

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(sceneName);
    }
}
