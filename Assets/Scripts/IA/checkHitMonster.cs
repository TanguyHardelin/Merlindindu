using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkHitMonster : MonoBehaviour {

	[SerializeField] Animator monster;

	void Awake() {
		monster = gameObject.GetComponentInParent<Animator>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !monster.GetComponent<Monster>().hasAttacked && monster.GetBool("isAttacking")) {
			monster.GetComponent<Monster>().hasAttacked = true;
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.setHealth(player.getHealth() - monster.GetComponent<Monster>().damageAmount);
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume = 1;
            var audioClip = Resources.Load<AudioClip>("Sounds/Player/onHit");
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = audioClip;
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
        }
	}
}
