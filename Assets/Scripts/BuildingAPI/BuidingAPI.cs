using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuidingAPI : MonoBehaviour {
    [SerializeField]
    protected string villageTag;
    [SerializeField]
    protected Village villageReference;
    [SerializeField]
    protected Transform buildingParents;

    [SerializeField]
    protected Material material_good;

    [SerializeField]
    protected Material material_bad;


    protected All3DObjects all3DObjectsScript;
    protected EnvironnementGenerator environnementGenerator;
    protected MapGenerator mapGenerator;
    protected GameObject ghostObject;
    protected Quaternion currentRotation = Quaternion.identity;

    public bool[,] caseIsOccuped ;

    // Use this for initialization
    void Start () {
        all3DObjectsScript = GameObject.FindGameObjectWithTag(villageTag).GetComponentInChildren<All3DObjects>();
        villageReference = GameObject.FindGameObjectWithTag(villageTag).GetComponentInChildren<Village>();

        environnementGenerator = FindObjectOfType<EnvironnementGenerator>();
        mapGenerator = FindObjectOfType<MapGenerator>();

        caseIsOccuped = new bool[mapGenerator.mapWidth, mapGenerator.mapHeight];


        for(int i = 0; i < mapGenerator.mapWidth; i++)
        {
            for(int j=0;j< mapGenerator.mapHeight; j++)
            {
                caseIsOccuped[i, j] = false;
            }
        }

        chooseBuildingPosition(new Vector2(2,2));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 caseToCoordonnate(Vector3 p)
    {
        p[0] = (p[0] - mapGenerator.mapWidth / 2.0f) * 5.0f;
        p[2] = (p[2] - mapGenerator.mapHeight / 2.0f) *5.0f;
        return p;
    }

    public bool couldInstantiateBuilding(RessourceType ressourceNeeded, RessourceType currentRessources)
    {
        if (currentRessources.gold - ressourceNeeded.gold < 0)
        {
            return false;
        }
        if (currentRessources.wood - ressourceNeeded.wood < 0)
        {
            return false;
        }
        if (currentRessources.cold - ressourceNeeded.cold < 0)
        {
            return false;
        }
        if (currentRessources.silver - ressourceNeeded.silver < 0)
        {
            return false;
        }
        if (currentRessources.citizen - ressourceNeeded.citizen < 0)
        {
            return false;
        }
        if (currentRessources.food - ressourceNeeded.food < 0)
        {
            return false;
        }
        if (currentRessources.stone - ressourceNeeded.stone < 0)
        {
            return false;
        }
        return true;
    }
    public bool couldSpawn(Vector2 size,Vector3 position)
    {
        //Vérification si la case est occupée
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                if(caseIsOccuped[(int)position.x + i, (int)position.z + j] == true)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public BuildingAPIError spawnBuilding(Vector3 position)
    {
        Vector3 building_position = new Vector3(environnementGenerator.getIndexFromCoordinate(position.x), position.y, environnementGenerator.getIndexFromCoordinate(position.z));
        Building new_building = all3DObjectsScript.getCurrentObject();
        //Gestions des erreurs eventueles:
        BuildingAPIError retour;
        retour.alreadyOccuped = false;
        retour.ressourcesMissing = false;
        retour.other = false;

        if (new_building)
        {
            Vector2 building_size = new_building.getSize();

            if (couldSpawn(building_size, building_position))
            {
                if (couldInstantiateBuilding(new_building.getRessourcesNeeded(), villageReference.getRessources()))
                {
                    GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().volume = (float)0.7;
                    var audioClip = Resources.Load<AudioClip>("Sounds/constructBuildMP3");
                    GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().clip = audioClip;
                    GameObject.FindGameObjectWithTag("UISound").GetComponent<AudioSource>().Play();

                    //On détruit le fantome:
                    destroyGhostObject();

                    //On s'occupe de ne pas pouvoir placer 2 objs au meme endroit
                    for (int i = 0; i < building_size[0]; i++)
                    {
                        for (int j = 0; j < building_size[1]; j++)
                        {
                            caseIsOccuped[(int)building_position.x + i, (int)building_position.z + j] = true;
                        }
                    }

                    //On instantie le batiment
                    building_position = caseToCoordonnate(building_position);
                    new_building.setParent(buildingParents);
                    villageReference.addBuilding(new_building);

                    Building tmp=Instantiate(new_building, building_position, Quaternion.identity, buildingParents);
                    tmp.initialize(currentRotation);

                    //On update les ressources
                    villageReference.setRessources(villageReference.getRessources() - new_building.getRessourcesNeeded());
                    
                }
                else
                {
                    retour.ressourcesMissing = true;
                }
            }
            else
            {
                retour.alreadyOccuped = true;
            }
        }
        else
        {
            retour.other = true;
        }
        return retour;
    }
    public void spawnGhostBuilding(Vector3 position)
    {
        //On détruit l'ancien ghost:
        if (ghostObject) Destroy(ghostObject);
        //Position taille ect ...
        Vector3 building_position = new Vector3(environnementGenerator.getIndexFromCoordinate(position[0]), position[1], environnementGenerator.getIndexFromCoordinate(position[2]));
        Building new_building = all3DObjectsScript.getCurrentObject();
        if (new_building)
        {
            Vector2 building_size = new_building.getSize();

            Material ghostMaterial;

            if (couldSpawn(building_size, building_position))
            {
                if (couldInstantiateBuilding(new_building.getRessourcesNeeded(), villageReference.getRessources()))
                {
                    ghostObject = new_building.getLastModelOfBuilding();
                    ghostMaterial = material_good;
                }
                else
                {
                    ghostMaterial = material_bad;

                }
            }
            else
            {
                ghostMaterial = material_bad;
            }
            //On instantie le ghost
            building_position = caseToCoordonnate(building_position);
            new_building.setParent(buildingParents);
            if (ghostObject)
            {
                ghostObject = Instantiate(ghostObject, building_position, Quaternion.identity, buildingParents);
                ghostObject.transform.rotation = currentRotation;
                //On change le material de l'object avec le valid pour dire que c'est bon:
                Renderer[] all_renderer = ghostObject.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < all_renderer.Length; i++)
                {
                    Material[] tmp = all_renderer[i].materials;
                    for (int j = 0; j < tmp.Length; j++)
                    {
                        tmp[j] = ghostMaterial;
                    }
                    all_renderer[i].materials = tmp;


                }
            }
        }
        
    }

    public Vector2 chooseBuildingPosition(Vector2 size)
    {
        Vector2 r=new Vector2(0,0);
        bool ko = true;
        int i = 0;
        while (ko)
        {
            int x = Random.Range((int)villageReference.getCenter()[0]- (int)villageReference.getSize()[0], (int)villageReference.getCenter()[0] + (int)villageReference.getSize()[0]);
            int z = Random.Range((int)villageReference.getCenter()[1] - (int)villageReference.getSize()[1], (int)villageReference.getCenter()[1] + (int)villageReference.getSize()[1]);

            if (couldSpawn(new Vector3(size[0], 0, size[1]),new Vector3(x, 0, z)))
            {
                r[0] = x;
                r[1] = z;
                ko = false;
            }
            else
            {
                if (i > 100)
                {
                    villageReference.setSize(villageReference.getSize() * 2);
                }
            }
            i++;
        }
        return r;
    }

    public void destroyGhostObject()
    {
        if (ghostObject) Destroy(ghostObject);
    }

    public void stopBuilding()
    {
        all3DObjectsScript.setCurrentObjToNull();
    }

    public void apply90RotationDegree()
    {

        //currentRotation *= new Quaternion(0, 0.707f, 0, 0.707f);
    }
}


public struct BuildingAPIError
{
    public bool alreadyOccuped;
    public bool ressourcesMissing;
    public bool other;
}