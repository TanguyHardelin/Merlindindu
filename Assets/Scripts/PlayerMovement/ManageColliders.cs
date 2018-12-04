using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageColliders : MonoBehaviour {
    [SerializeField]
    protected ExplorationUI _explorationUI;

    // Use this for initialization
    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
		
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
        }
        else if (other.tag == "village")
        {
            _explorationUI.setInfoPanelVisibility(true);
            _explorationUI.setInfoText("-Press M to manage-");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.P) && (other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone"))
        {
            Collectable script = (Collectable)other.GetComponent(typeof(Collectable));
            script.PickRessources();
            if (script.getIsEmpty()) _explorationUI.setInfoText("-No more ressources-");
            else
            {
                _explorationUI.setInfoPanelVisibility(true);
                _explorationUI.setInfoText("-Press P to gather-");
            }
        }
        else if (Input.GetKeyDown(KeyCode.M) && (other.tag == "village"))
        {
            _explorationUI.setInfoPanelVisibility(true);
            Messenger.Broadcast(GameEvent.SwitchToGestionMode);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone")
        {
            _explorationUI.setInfoPanelVisibility(false);
        }
        else if (other.tag == "village")
        {
            _explorationUI.setInfoPanelVisibility(false);
        }
    }
}
