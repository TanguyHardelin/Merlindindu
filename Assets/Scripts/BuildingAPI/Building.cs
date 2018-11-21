using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;




public class Building : MonoBehaviour {
    //Paramaters attributes:
    [SerializeField]
    protected RessourceType ressourcesNeeded;

    [SerializeField]
    protected float constructionTime;

    [SerializeField]
    protected StringObject buildingEtapes;

    [SerializeField]
    protected int startingIndex = 0;

    [SerializeField]
    protected Transform parent;

    //Internal attributes:
    protected GameObject current_obj;
    //protected ParticleSystem particule_system;
    protected int current_index;
    protected float current_time = 0;
    protected bool construction_finish = false;


    void Start () {
        //NB: la vérification des ressources nécéssaire n'est pas fait ici !
        //    Ce n'est pas le boulot de cet object.
        //    Cet object a juste pour role de faire l'instantié les différents models succéssif

        //particule_system = GetComponentInChildren<ParticleSystem>();
        current_obj = Instantiate(buildingEtapes[startingIndex.ToString()], transform.position, Quaternion.identity, parent);
        //current_obj.transform.localScale = new Vector3(0.5f, 0.4f, 0.5f);
        current_index = startingIndex;
    }
	
	void Update () {
        if (construction_finish == false)
        {
            //On incrémente la valeur du temps:
            current_time += Time.deltaTime;

            //On vérifie si on ne doit pas update le model:
            if(current_time> constructionTime / (buildingEtapes.Count - 1))
            {
                current_time = 0.0f;
                upgradeModel();
                
            }

            //On vérifie si on a pas fini:
            if (current_index == (buildingEtapes.Count -1))
            {
                construction_finish = true;
            }
        }
        
	}
    
    void upgradeModel()
    {
        //start the particules
        // particule_system.Play();

        //upgrade building models:
        current_index++;
        if (startingIndex < buildingEtapes.Count)
        {
            Destroy(current_obj);
            current_obj = Instantiate(buildingEtapes[startingIndex.ToString()], transform.position, Quaternion.identity,parent);
            //current_obj.transform.localScale = new Vector3(0.5f, 0.4f, 0.5f);
        }
        else
        {
            current_index= buildingEtapes.Count - 1;
        }
       
    }
    void downgradeModel()
    {
        current_index--;
        if (startingIndex < buildingEtapes.Count)
        {
            Destroy(current_obj);
            current_obj = Instantiate(buildingEtapes[startingIndex.ToString()], transform.position, Quaternion.identity, parent);
            //current_obj.transform.localScale = new Vector3(0.5f, 0.4f, 0.5f);
        }
        else
        {
            current_index = 0;
        }
    }
    

    public void setParent(Transform t)
    {
        parent = t;
    }
    
}
