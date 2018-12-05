using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject player;

    public float currentGold;
    public Image current_gold;
    private float maxGold = 100000;
    private bool freeze = false;

	// Use this for initialization
	void Start () {
        currentGold = player.GetComponent<Player>().getRessources().gold * 100;
        maxGold = player.GetComponent<Player>().maxGold * 100;
        UpdateHealthBar();
    }
	
	// Update is called once per frame
	void UpdateHealthBar() {

        float ratio = currentGold / maxGold;
        current_gold.rectTransform.localScale = new Vector3(ratio, 1, 1);
	}

    void Update()
    {   
        currentGold = player.GetComponent<Player>().getRessources().gold * 100;
        maxGold = player.GetComponent<Player>().maxGold * 100;
        UpdateHealthBar();
	}

    public void setFreeze(bool frz)
    {
        freeze = frz;
    }
}
