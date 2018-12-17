using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{   
    public GameObject player;
    public Animator animator;
    bool isDead;
    bool isAttacking;
    bool isMoving;
    float health = 200f;
    Vector3 direction = Vector3.zero;
    public float moveRate;
    public bool hasBlasted = false;
    public bool canMove = true;
    public int c = 0;
    public int delayBlast = 400;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (hasBlasted) {
            c++;
            if (c >= delayBlast) {
                c = 0;
                hasBlasted = false;
            }
        }

        updateDatas();
        updateActions();
    }

    void updateDatas() {
        direction = player.transform.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

    void updateActions() {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 4) {
            animator.SetBool("attacking", true);
            Debug.Log("attacking");
        }
        if (Vector3.Distance(player.transform.position, this.transform.position) < 10 && !hasBlasted) {
            
            animator.SetBool("blasting", true);
            Debug.Log("blasting");
            canMove = false;
        }
        else if (Vector3.Distance(player.transform.position, this.transform.position) < 20 && canMove) {
            
            animator.SetBool("isMoving", true);
            Debug.Log("moving");
            transform.position += transform.forward * moveRate * Time.deltaTime;
        }
        else {
            Debug.Log("idling");
            animator.SetBool("isIdle", true);
        }
    }
}
