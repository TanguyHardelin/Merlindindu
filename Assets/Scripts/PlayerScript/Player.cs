using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] protected RessourceType _ressources;
	public float spdTimer = 240;
    public float maxGold = 1000;
	float spdCount = 0;

    public RessourceType getRessources()
    {
        return _ressources;
    }

	// Use this for initialization
	void Start () {
		_ressources.gold = 500;
    }
	
	// Update is called once per frame
	void Update () {
		spdCount += 1;

        if (spdCount >= spdTimer)
        {
            _ressources.gold --;
            spdCount = 0;
        }
	}
}
