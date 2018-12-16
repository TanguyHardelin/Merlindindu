using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionButtonScript : MonoBehaviour {
    [SerializeField]
    protected string _role;

    protected Button _button;

	// Use this for initialization
	void Start () {
        _button = GetComponent<Button>();

        if (_role == "village")
        {
            _button.onClick.AddListener(() => OpenVillage());
        }
        else if (_role == "millitary")
        {
            _button.onClick.AddListener(() => OpenMillitary());
        }
        else if (_role == "ressources")
        {
            _button.onClick.AddListener(() => OpenRessources());
        }
        else if (_role == "switchToExploration")
        {
            _button.onClick.AddListener(() => switchToExploration());
        }
        else if (_role == "switchToGestion")
        {
            _button.onClick.AddListener(() => switchToGestion());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OpenVillage()
    {
        Messenger.Broadcast(GameEvent.VillageNeedOpen);
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().volume = 1;
        var audioClip = Resources.Load<AudioClip>("Sounds/click");
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().clip = audioClip;
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().Play();
    }
    void OpenMillitary()
    {
        Messenger.Broadcast(GameEvent.MillitaryNeedOpen);
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().volume = 1;
        var audioClip = Resources.Load<AudioClip>("Sounds/click");
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().clip = audioClip;
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().Play();
    }
    void OpenRessources()
    {
        Messenger.Broadcast(GameEvent.RessourcesNeedOpen);
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().volume = 1;
        var audioClip = Resources.Load<AudioClip>("Sounds/click");
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().clip = audioClip;
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().Play();
    }
    void switchToExploration()
    {
        Messenger.Broadcast(GameEvent.SwitchToExplorationMode);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = (float)0.15;
        GameObject.FindGameObjectWithTag("ManageCamera").GetComponent<AudioSource>().volume = 0;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().volume = 1;
        var audioClip = Resources.Load<AudioClip>("Sounds/click");
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().clip = audioClip;
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().Play();
    }

    void switchToGestion() {
        Messenger.Broadcast(GameEvent.SwitchToGestionMode);
        GameObject.FindGameObjectWithTag("ManageCamera").GetComponent<AudioSource>().volume = (float)0.12;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = 0;
        GameObject.FindGameObjectWithTag("ManageCamera").GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().volume = 1;
        var audioClip = Resources.Load<AudioClip>("Sounds/click");
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().clip = audioClip;
        GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().Play();
    }
}
