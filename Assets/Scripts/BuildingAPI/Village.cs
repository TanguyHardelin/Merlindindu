using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour {
    [SerializeField]
    protected int villageRefreshRate;

    protected float current_Time = 0f;

    [SerializeField]
    protected Vector2 villageCenter;
    [SerializeField]
    protected Vector2 villageSize;

    [SerializeField]
    protected List<Building> villageBuilding= new List<Building>();

    [SerializeField]
    protected RessourceType ressources;
    [SerializeField]
    protected RessourceType maxRessources;
    
    public Vector2 getSize()
    {
        return villageSize;
    }
    public void setSize(Vector2 new_size)
    {
        villageSize = new_size;
    }

    public Vector2 getCenter()
    {
        return villageCenter;
    }

    public RessourceType getRessources()
    {
        return ressources;
    }

    public RessourceType getRessourcesLimit()
    {
        return maxRessources;
    }

    public void addRessources(RessourceType r)
    {
        ressources.gold += r.gold;
        ressources.stone += r.stone;
        ressources.wood += r.wood;
        //ressources.setToMax(maxRessources);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        current_Time += Time.deltaTime;

        if(current_Time> villageRefreshRate)
        {
            //On applique les regles de gestions

            //Production de ressources
            RessourceType ressourcesProduction = new RessourceType(0, 0, 0, 0, 0, 0,0);
            for (int i=0; i < villageBuilding.Count; i++)
            {
                if(villageBuilding[i].isFinish())
                    ressourcesProduction += villageBuilding[i].getRessourcesProduction();
            }
            //Application des bonus
            RessourceType bonus=new RessourceType(0,0,0,0,0,0,0);
            for (int i = 0; i < villageBuilding.Count; i++)
            {
                if (villageBuilding[i].isFinish())
                    bonus += villageBuilding[i].getRessourcesBonus();
            }
            ressourcesProduction *= bonus;
            //ressources += ressourcesProduction;

            //ressources.setToMax(maxRessources);

            current_Time = 0.0f;
        }
	}

    public void addBuilding(Building b)
    {
        villageBuilding.Add(b);
    }

    public void setRessources(RessourceType new_ressources)
    {
        //ressources = new_ressources;
    }

    public void setRessourcesLimit(RessourceType new_ressources)
    {
        maxRessources = new_ressources;
    }
}
