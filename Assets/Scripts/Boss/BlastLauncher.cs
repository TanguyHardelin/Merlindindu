using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastLauncher : MonoBehaviour
{   
    public bool isBlasting = false;
    public ParticleSystem bossBlast;
    public ParticleSystem LavaGround;
    public ParticleSystem LavaPuff;
    public ParticleSystem LavaStart;
    public GameObject mouth;
    public GameObject boss;
    public float ticDmg = 2f;

    // Start is called before the first frame update
    void Start()
    {
        bossBlast = GameObject.Find("BossBlast").GetComponent<ParticleSystem>();
        mouth = GameObject.Find("CATRigHub003Bone001");
        boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlasting) {
            Vector3 blastPosition = mouth.transform.position;
            blastPosition.y -= 0.05f;
            blastPosition.z += 0.5f;


            bossBlast.transform.position = blastPosition;
            bossBlast.transform.rotation = boss.transform.rotation;

            
            bossBlast.Emit(1);
            LavaGround.Emit(1);
            LavaPuff.Emit(1);
            LavaStart.Emit(1);
        }

        else {
            bossBlast.Stop(true);
        }
    }

    void OnParticleCollision(GameObject other) {
        if (other.tag == "Player") {
            GameObject.Find("Player").GetComponent<PlayerController>().setHealth(GameObject.Find("Player").GetComponent<PlayerController>().getHealth() - ticDmg);
        }
    }
}
