﻿using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject player;

    public float currentGold;
    public Image current_gold;
    public float currentLife;
    public Image current_life;
    private float maxGold = 100000;
    private float maxLife = 100;
    private bool freeze = false;

	// Use this for initialization
	void Start () {
        currentGold = player.GetComponent<Player>().getRessources().gold * 100;
        currentLife = player.GetComponent<PlayerController>().getHealth();
        maxGold = player.GetComponent<Player>().maxGold * 100;
        maxLife = player.GetComponent<PlayerController>().getMaxHealth();
        UpdateGoldBar();
        UpdateLifeBar();
    }
	
	// Update is called once per frame
	void UpdateGoldBar() {

        float ratio = currentGold / maxGold;
        current_gold.rectTransform.localScale = new Vector3(ratio, 1, 1);
	}
    void UpdateLifeBar()
    {

        float ratio2 = currentLife / maxLife;
        current_life.rectTransform.localScale = new Vector3(ratio2, 1, 1);
    }

    void Update()
    {   
        currentGold = player.GetComponent<Player>().getRessources().gold * 100;
        maxGold = player.GetComponent<Player>().maxGold * 100;
        UpdateGoldBar();
        currentLife = player.GetComponent<PlayerController>().getHealth();
        maxLife = player.GetComponent<PlayerController>().getMaxHealth();
        UpdateLifeBar();
    }

    public void setFreeze(bool frz)
    {
        freeze = frz;
    }
}