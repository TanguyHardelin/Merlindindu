using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkHit : MonoBehaviour {

	[SerializeField] Animator player;
    private GameObject floatTextPrefab;

    void Start()
    {
        floatTextPrefab = GameObject.FindGameObjectWithTag("floatTxt");
    }
    void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Monster>() && player.GetBool("isAttacking") && !player.GetComponent<PlayerController>().hasAttacked) {
			Debug.Log(other.tag);
			player.GetComponent<PlayerController>().hasAttacked = true;
			Monster carac = other.gameObject.GetComponent<Monster>();
			carac.setHealth(carac.getHealth() - player.GetComponent<PlayerController>().ATK);
            showFloatTxtRessources("-"+player.GetComponent<PlayerController>().ATK, new Color32(205, 5, 5, 255));
            carac.setIsAttacked(true);
        }
	}

    private void showFloatTxtRessources(string txt, Color color)
    {
        GameObject newTxtFloat = Instantiate(floatTextPrefab, floatTextPrefab.transform.position + new Vector3(0, 150, 0), Quaternion.identity);
        newTxtFloat.SetActive(true);
        newTxtFloat.GetComponent<FloatTextController>().SetTextandMove(txt, color);
        Destroy(newTxtFloat.gameObject, (float)0.6);
    }
}
