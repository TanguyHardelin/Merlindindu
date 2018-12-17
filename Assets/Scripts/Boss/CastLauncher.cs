using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLauncher : MonoBehaviour
{
    public bool isCasting = false;
    public GameObject bossCast;
    public ParticleSystem Earth;
    public ParticleSystem EarthDust;
    public GameObject rArm;
    public GameObject boss;
    public Vector3 targetPosition;
    public float speed = 2f;
    public bool isCasted = false;
    public float dmgMeteor = 60f;
    public GameObject prefab;
    public GameObject parent;

    void Start()
    {
        rArm = GameObject.Find("CATRigRArmPalm");
        boss = GameObject.Find("Boss");
        parent = GameObject.Find("Plane");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCasted) {
            Vector3 blastPosition = rArm.transform.position;
            blastPosition.z -= 0.7f;


            bossCast.transform.position = blastPosition;
            bossCast.transform.rotation = boss.transform.rotation;

            
            bossCast.GetComponent<ParticleSystem>().Emit(1);
            Earth.Emit(1);
            EarthDust.Emit(1);
        }
        else {
            if (Vector3.Distance(bossCast.transform.position, targetPosition) < 1.001f) {
                GameObject test = Instantiate(prefab, bossCast.transform.position, Quaternion.identity, parent.transform);
                Debug.Log(test);
                Destroy(bossCast);
            }
            else {
                moveTo(targetPosition);
            }
        }
    }

    void OnParticleCollision(GameObject other) {
        GameObject.Find("Player").GetComponent<PlayerController>().setHealth(GameObject.Find("Player").GetComponent<PlayerController>().getHealth() - dmgMeteor);
        GameObject test = Instantiate(prefab, bossCast.transform.position, Quaternion.identity, parent.transform);
        Debug.Log(test);
        Destroy(bossCast);
    }

    void moveTo(Vector3 targetPosition) {
        Vector3 direction = targetPosition - bossCast.transform.position;
        direction.y += 1f;
        bossCast.transform.rotation = Quaternion.Slerp(bossCast.transform.rotation, Quaternion.LookRotation(direction), 1f);

        bossCast.transform.position += bossCast.transform.forward * speed * Time.deltaTime;
    }
}
