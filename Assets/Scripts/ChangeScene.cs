using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string sceneName = "Main"; // Change the scene name inside the Unity UI
    public GameObject dontDestroyOnLoad=null;
    public bool returnToMainScene = false;

    public Vector3 playerSpwaningPosition;

    void OnTriggerEnter(Collider other)
    {
        /*
        if(dontDestroyOnLoad) DontDestroyOnLoad(dontDestroyOnLoad);
        if (returnToMainScene)
        {
           
        }
        if (other.gameObject.tag == "Player")
        SceneManager.LoadScene(sceneName);
        */
        GameObject player = GameObject.Find("Player");
        player.transform.position = playerSpwaningPosition;

        /*
        if (returnToMainScene)
        {
            
        }
        */
    }
}
