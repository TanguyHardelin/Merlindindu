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

        Debug.Log(this.getSubChunkElement(x_center, y_center));

        if (!initialized)
        {
            chunk_objects.Add(this.getSubChunkElement(x_center, y_center));

            chunk_objects.Add(this.getSubChunkElement(x_center + 1, y_center + 1));
            chunk_objects.Add(this.getSubChunkElement(x_center - 1, y_center + 1));
            chunk_objects.Add(this.getSubChunkElement(x_center + 1, y_center - 1));
            chunk_objects.Add(this.getSubChunkElement(x_center - 1, y_center - 1));

            chunk_objects.Add(this.getSubChunkElement(x_center, y_center + 1));
            chunk_objects.Add(this.getSubChunkElement(x_center, y_center - 1));
            chunk_objects.Add(this.getSubChunkElement(x_center + 1, y_center));
            chunk_objects.Add(this.getSubChunkElement(x_center - 1, y_center));

            initialized = true;
        }
    }
    
    public void Instantiate()
    {
        if (initialized == true)
        {
            SpawnChunkObject(0, x_center, y_center);

            SpawnChunkObject(1, x_center + 1, y_center + 1);
            SpawnChunkObject(2, x_center - 1, y_center + 1);
            SpawnChunkObject(3, x_center + 1, y_center - 1);
            SpawnChunkObject(4, x_center - 1, y_center - 1);


            SpawnChunkObject(5, x_center, y_center + 1);
            SpawnChunkObject(6, x_center, y_center - 1);
            SpawnChunkObject(7, x_center + 1, y_center);
            SpawnChunkObject(8, x_center - 1, y_center);

            instantiate = true;
        }
    }

    public void Destroy()
    {
        if (instantiate == true)
        {
            for (int i = 0; i < instantied_objects.Count; i++)
            {
                GameObject.Destroy(instantied_objects[i]);
            }
            instantiate = false;
        }
    }

    public void SpawnChunkObject(int index,int x,int y)
    {
        Debug.Log("Instantiate to x= " + (x - mapWidth / 2.0f) * 5.0f + " y= " + (y - mapHeight / 2.0f) * 5.0f);
        instantied_objects.Add(GameObject.Instantiate(chunk_objects[index].gameobject, new Vector3((x - mapWidth / 2.0f) * 5.0f, mapGenerator.evaluateHeight(chunk_objects[index].spawning_height), (y - mapHeight / 2.0f) * 5.0f), Quaternion.identity));
    }

    protected SubChunkElement getSubChunkElement(int x,int y)
    {
        float currentHeight = noiseMap[x, y];

        SubChunkElement tmp=new SubChunkElement();
        for (int i = 0; i < environnement_type.Length; i++)
        {

            if (currentHeight >= environnement_type[i].startingHeightOfTerrain && currentHeight < environnement_type[i].endingHeightOfTerrain && environnement_type[i].spawningObject.Length > 0)
            { 
                int indexOfSpawn = (int)UnityEngine.Random.Range(0.0f, environnement_type[i].spawningObject.Length - 1);
                //Debug.Log("indexOfSpawn " + indexOfSpawn);
                tmp.gameobject= environnement_type[i].spawningObject[indexOfSpawn];
                tmp.spawning_height = environnement_type[i].spawningHeight;
                return tmp;
            }

        }
        tmp.gameobject = null;
        tmp.spawning_height = 0.0f;
        Debug.Log("Default");
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
}