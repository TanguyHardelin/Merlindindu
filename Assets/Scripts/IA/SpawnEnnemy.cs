using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour {


    public int spawnRate;
    public int respawnTime = 2000;

    public Transform instance;
    private int count = 1950;

    // Use this for initialization
    void Start () {
        if (instance.tag == "Knight") respawnTime = 20;
	}



    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Player" && count > respawnTime)
        {
            if (Random.Range(0, 101) < spawnRate)
            {
                Instantiate(instance, transform.position + new Vector3(0, 0, 0), transform.rotation, GameObject.FindGameObjectWithTag("Monster").transform);
                count = 0;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        count++;
	}
}
