using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour {


    public int spawnRate;

	// Use this for initialization
	void Start () {
		
	}

    public Transform instance;
    private int count = 1950;

    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Player" && count > 2000)
        {
            if (Random.Range(0, 101) < spawnRate)
            {
                Instantiate(instance, transform.position + new Vector3(10, 0, 10), transform.rotation, GameObject.FindGameObjectWithTag("Monster").transform);
                count = 0;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        count++;
	}
}
