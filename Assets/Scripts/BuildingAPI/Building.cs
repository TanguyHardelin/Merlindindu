using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;




public class Building : MonoBehaviour {
    [Header("Paramétrage de l'UI")]
    [SerializeField]
    protected string _name;

    [SerializeField]
    protected string _description;

    [SerializeField]
    protected string _building_tag;

    [SerializeField]
    protected Texture _building_icon;


    [Header("Informations sur le bâtiment")]
    [SerializeField]
    protected float constructionTime;

    [SerializeField]
    protected StringObject buildingEtapes;

    [SerializeField]
    protected int startingIndex = 0;

    [SerializeField]
    protected Vector2 size = new Vector2(1, 1);


    [Header("Ressource nécéssaire pour construire bâtiments")]
    [SerializeField]
    protected RessourceType ressourcesNeeded;

    [Header("Production du bâtiment")]
    [SerializeField]
    protected RessourceType ressourcesProduction;

   // [Header("Production du bâtiment (une seule fois à sa construction)")]
   // [SerializeField]
    protected RessourceType ressourcesProductionOneShot;

   // [Header("Bonus apportés par le bâtiment sur les ressources")]
   // [SerializeField]
    protected RessourceType ressourcesBonus;

    

    [Header("Parent dans le jeu")]
    [SerializeField]
    protected Transform parent;

    //Internal attributes:
    protected GameObject current_obj;
    protected int current_index;
    protected float current_time = 0;
    protected bool construction_finish = false;
    protected Quaternion currentRotation = Quaternion.identity;
    protected bool isInitialized = false;


    void Start () {
        
    }
    public void initialize(Quaternion q)
    {
        //NB: la vérification des ressources nécéssaire n'est pas fait ici !
        //    Ce n'est pas le boulot de cet object.
        //    Cet object a juste pour role de faire l'instantié les différents models succéssif

        currentRotation = q;
        current_obj = Instantiate(buildingEtapes[startingIndex.ToString()], transform.position, currentRotation, parent);
        current_index = startingIndex;
        
        isInitialized = true;
    }
	
	void Update () {
        if (construction_finish == false && isInitialized)
        {
            //On incrémente la valeur du temps:
            current_time += Time.deltaTime;

            //On vérifie si on ne doit pas update le model:
            if (current_time> constructionTime / (buildingEtapes.Count))
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

    public bool isFinish()
    {
        return construction_finish;
    }
    
    void upgradeModel()
    {
        //start the particules
        // particule_system.Play();

        //upgrade building models:
        current_index++;
        if (current_index < buildingEtapes.Count)
        {
            Destroy(current_obj);
            current_obj = Instantiate(buildingEtapes[current_index.ToString()], transform.position, currentRotation, parent);
        }
        else
        {
            current_index= buildingEtapes.Count - 1;
        }
       
    }
    void downgradeModel()
    {
        current_index--;
        if (current_index > 0)
        {
            Destroy(current_obj);
            current_obj = Instantiate(buildingEtapes[current_index.ToString()], transform.position, currentRotation, parent);
        }
        else
        {
            current_index = 0;
        }
    }
    public GameObject getLastModelOfBuilding()
    {
        return buildingEtapes[(buildingEtapes.Count - 1).ToString()];
    }
    

    public void setParent(Transform t)
    {
        parent = t;
    }

    public string getTag()
    {
        return _building_tag;
    }

    public string getName()
    {
        return _name;
    }

    public string getDescription()
    {
        return _description;
    }

    public Texture getIcon()
    {
        return _building_icon;
    }

    public RessourceType getRessourcesNeeded()
    {
        return ressourcesNeeded;
    }

    public RessourceType getRessourcesProduction()
    {
        return ressourcesProduction;
    }

    public RessourceType getRessourcesProductionOnShot()
    {
        return ressourcesProductionOneShot;
    }

    public RessourceType getRessourcesBonus()
    {
        return ressourcesBonus;
    }

    public Vector2 getSize()
    {
        return size;
    }

    
}
