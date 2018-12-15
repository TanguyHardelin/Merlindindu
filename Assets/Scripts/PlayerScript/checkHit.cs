using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkHit : MonoBehaviour {

	[SerializeField] Animator player;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Monster>() && player.GetBool("isAttacking") && !player.GetComponent<PlayerController>().hasAttacked) {
			Debug.Log(other.tag);
			player.GetComponent<PlayerController>().hasAttacked = true;
			Monster carac = other.gameObject.GetComponent<Monster>();
			carac.setHealth(carac.getHealth() - player.GetComponent<PlayerController>().ATK);
		}
	}
}
