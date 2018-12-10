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
    }
    void OpenMillitary()
    {
        Messenger.Broadcast(GameEvent.MillitaryNeedOpen);
    }
    void OpenRessources()
    {
        Messenger.Broadcast(GameEvent.RessourcesNeedOpen);
    }
    void switchToExploration()
    {
        Messenger.Broadcast(GameEvent.SwitchToExplorationMode);
    }

    void switchToGestion() {
        Messenger.Broadcast(GameEvent.SwitchToGestionMode);
    }
}
