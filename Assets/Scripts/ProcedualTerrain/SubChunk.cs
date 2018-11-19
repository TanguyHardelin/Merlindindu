using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubChunk{
    protected MapGenerator mapGenerator;

    //Map informations
    protected int mapWidth = 0;
    protected int mapHeight = 0;
    protected float[,] noiseMap;
    protected EvironnementType[] environnement_type;

    //Object in chunks:
    public List<SubChunkElement> chunk_objects = new List<SubChunkElement>();
    protected List<GameObject> instantied_objects = new List<GameObject>();

    public int x_center;
    public int y_center;

    //Control variable:
    public bool instantiate = false;
    public bool initialized = false;

    public SubChunk (int xCenter, int yCenter, EvironnementType[] environnementType)
    {
        x_center = xCenter;
        y_center = yCenter;
        environnement_type = environnementType;

        mapGenerator = GameObject.FindObjectOfType<MapGenerator>();
        mapWidth = mapGenerator.mapWidth;
        mapHeight = mapGenerator.mapHeight;
        noiseMap = mapGenerator.noiseMap;

        if (!initialized)
        {
          
            initialized = true;
        }
    }
    
    public void Instantiate()
    {
        if (initialized == true && instantiate == false)
        {
          

            instantiate = true;
        }
        else
        {
            int size = instantied_objects.Count;
            for (int i = 0; i < size; i++)
            {
                instantied_objects[i].SetActive(true);
            }
        }
    }

    public void Destroy()
    {
        if (instantiate)
        {
            int size = instantied_objects.Count;
            for (int i = 0; i < size; i++)
            {
                /*
                GameObject.Destroy(instantied_objects[0]);
                instantied_objects.RemoveAt(0);
                */
                instantied_objects[i].SetActive(false);
            }

            //instantiate = false;
        }
        
        
    }

    public void SpawnChunkObject(int index,int x,int y)
    {
        if (!instantiate)
        {
            
            instantied_objects.Add(GameObject.Instantiate(chunk_objects[index].gameobject, new Vector3((x - mapWidth / 2.0f) * 5.0f, mapGenerator.evaluateHeight(chunk_objects[index].spawning_height), (y - mapHeight / 2.0f) * 5.0f), Quaternion.identity, chunk_objects[index].parent));

        }
    }

    protected SubChunkElement getSubChunkElement(int x,int y)
    {
        float currentHeight = noiseMap[x, y];

        SubChunkElement tmp=new SubChunkElement();
        for (int i = 0; i < environnement_type.Length; i++)
        {
            if (currentHeight < environnement_type[i].endingHeightOfTerrain && environnement_type[i].spawningObject.Length > 0)
            { 
                int indexOfSpawn = (int)UnityEngine.Random.Range(0.0f, environnement_type[i].spawningObject.Length - 1);
                //Debug.Log("x = " + x + " y = " + y + "rX=" + (x - mapWidth / 2.0f) * 5.0f + " rY=" + (y - mapHeight / 2.0f) * 5.0f+ "currentHeight= "+ currentHeight+" Type =" + environnement_type[i].name);
                tmp.gameobject= environnement_type[i].spawningObject[indexOfSpawn];
                tmp.spawning_height = environnement_type[i].spawningHeight;
                tmp.parent = environnement_type[i].parent;
                return tmp;
             
            }

        }
        //Debug.Log("x = " + x + " y= " + y);
       /// Debug.Log("currentHeight = " + currentHeight);
     

        tmp.gameobject = environnement_type[environnement_type.Length - 1].spawningObject[0];
        tmp.spawning_height = 0.0f;
        tmp.parent = environnement_type[environnement_type.Length - 1].parent;
        return tmp;

    }

    void Start()
    {
       

        
    }
}


public struct SubChunkElement
{
    public GameObject gameobject;
    public float spawning_height;
    public Transform parent;
}