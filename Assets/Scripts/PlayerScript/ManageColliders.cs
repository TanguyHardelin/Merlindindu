using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageColliders : MonoBehaviour {
    [SerializeField]
    protected ExplorationUI _explorationUI;

    [SerializeField]
    protected GameObject _buttonSwitchMode;

    protected string mode = "gestion";

    bool hasPicked = false;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.P)) {
            hasPicked = false;
        }
	}

    private void Awake()
    {
        Messenger.AddListener(GameEvent.SwitchToExplorationMode, goToExplorationMode);
        Messenger.AddListener(GameEvent.SwitchToGestionMode, goToGestionMode);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SwitchToExplorationMode, goToExplorationMode);
        Messenger.RemoveListener(GameEvent.SwitchToGestionMode, goToGestionMode);
    }


    public void goToExplorationMode()
    {
        mode = "exploration";
        _buttonSwitchMode.SetActive(true);
    }

    public void goToGestionMode()
    {
        mode = "gestion";
        _buttonSwitchMode.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone")
        {
            Collectable script = (Collectable)other.GetComponent(typeof(Collectable));
            if (script.getIsEmpty())
            {
                _explorationUI.setInfoPanelVisibility(true);
                _explorationUI.setInfoText("-No more ressources-");
            }
            else
            {
                _explorationUI.setInfoPanelVisibility(true);
                _explorationUI.setInfoText("-Press P to gather-");
            }

            if (Input.GetKeyDown(KeyCode.P) && !hasPicked) {
                Collectable scriptObj = (Collectable)other.GetComponent(typeof(Collectable));
                scriptObj.PickRessources();
                hasPicked = true;
                Debug.Log("pickingRessources");
            }
        }
        else if (other.tag == "village" && mode == "exploration")
        {
            _buttonSwitchMode.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if ((other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone") && !hasPicked) {
                Collectable script = (Collectable)other.GetComponent(typeof(Collectable));
                script.PickRessources();
                hasPicked = true;
                if (script.getIsEmpty()) {
                    _explorationUI.setInfoText("-No more ressources-");
                }
                else
                {
                    _explorationUI.setInfoPanelVisibility(true);
                    _explorationUI.setInfoText("-Press P to gather-");
                }
            }
        }
        else if (other.tag == "village" && mode == "exploration")
        {
            _buttonSwitchMode.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone")
        {
            _explorationUI.setInfoPanelVisibility(false);
        }
        else if (other.tag == "village" && mode == "exploration")
        {
            _buttonSwitchMode.SetActive(false);
        }
    }
}