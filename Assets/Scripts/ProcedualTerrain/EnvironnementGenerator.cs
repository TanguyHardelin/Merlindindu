using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnvironnementGenerator : MonoBehaviour {
    [SerializeField]
    protected Vector2 chunkSize;
    [SerializeField]
    protected EvironnementType[] environnementType;
    [SerializeField]
    protected InterestingElement[] allInterestingElement;

    protected MapGenerator mapGenerator; 

    protected int mapWidth = 0;
    protected int mapHeight = 0;

    protected float[,] noiseMap;

    protected bool isInitialized = false;

    //Internal atribrut usefull for optimisation:
    protected int oldX = 1000;
    protected int oldY = 1000;


    protected List<InstantiedChunk> instantiedChunk = new List<InstantiedChunk>();

    protected bool[,] canSpwan;

    // Use this for initialization
    void Start () {
        mapGenerator = FindObjectOfType<MapGenerator>();
        mapWidth = mapGenerator.mapWidth;
        mapHeight = mapGenerator.mapHeight;
        noiseMap = mapGenerator.noiseMap;

        canSpwan = new bool[mapWidth, mapHeight];

        //initialize canSpawn array
        for(int i=0;i< mapWidth; i++)
        {
            for(int j=0;j< mapHeight; j++)
            {
                canSpwan[i, j] = true;
            }
        }
        for (int l = 0; l < allInterestingElement.Length; l++)
        {
            
            Vector2 center = allInterestingElement[l].position;
            int size = allInterestingElement[l].size;

            for (int i = Mathf.CeilToInt(center[0] - size / 2 + mapWidth/2); i < Mathf.CeilToInt(center[0] + size / 2 + mapWidth / 2) +1; i++)
            {
                for (int j = Mathf.CeilToInt(center[1] - size / 2 + mapHeight / 2); j < Mathf.CeilToInt(center[1] + size / 2 + mapHeight / 2) +1 ; j++)
                {
                    canSpwan[i, j] = false;
                }
            }
        }
        //Set interesting Point
    }
    
	// Update is called once per frame
	void Update () {
        
    }

    public bool isInColliders(int x,int z)
    {
        //On verifie que les 4 cotés du chunks ne sont pas dedans:
        Vector2 pos0 = new Vector2((x + (int)chunkSize[0] / 2 - mapWidth / 2.0f) * 5.0f,  (z + (int)chunkSize[1] / 2 - mapHeight / 2.0f) * 5.0f);
        Vector2 pos1 = new Vector2((x + (int)chunkSize[0] / 2 - mapWidth / 2.0f) * 5.0f,  (z - (int)chunkSize[1] / 2 - mapHeight / 2.0f) * 5.0f);
        Vector2 pos2 = new Vector2((x - (int)chunkSize[0] / 2 - mapWidth / 2.0f) * 5.0f,  (z + (int)chunkSize[1] / 2 - mapHeight / 2.0f) * 5.0f);
        Vector2 pos3 = new Vector2((x - (int)chunkSize[0] / 2 - mapWidth / 2.0f) * 5.0f,  (z - (int)chunkSize[1] / 2 - mapHeight / 2.0f) * 5.0f);

        for (int i = 0; i < allInterestingElement.Length; i++)
        {
            if (Vector2.Distance(pos0, allInterestingElement[i].position) <= allInterestingElement[i].size || Vector2.Distance(pos1, allInterestingElement[i].position) <= allInterestingElement[i].size || Vector2.Distance(pos2, allInterestingElement[i].position) <= allInterestingElement[i].size || Vector2.Distance(pos3, allInterestingElement[i].position) <= allInterestingElement[i].size)
            {
                //Debug.Log("Impossible");
                return true;
            }
        }
        return false;
    }

    public void GenerateAroundPlayer(int x, int z)
    {
        //On vérifie si on doit instancié un chunk
        List<ChunkNeeded> is_chunk_need = new List<ChunkNeeded>();
        
        if (canSpwan[x + (int)chunkSize[0] / 2, z - (int)chunkSize[1] / 2] == true) is_chunk_need.Add(isChunkNeeded(x + (int)chunkSize[0]/2, z - (int)chunkSize[1]/2 ));
        if (canSpwan[x + (int)chunkSize[0] / 2, z + (int)chunkSize[1] / 2] == true) is_chunk_need.Add(isChunkNeeded(x + (int)chunkSize[0]/2, z + (int)chunkSize[1]/2));
        if (canSpwan[x - (int)chunkSize[0] / 2, z - (int)chunkSize[1] / 2] == true) is_chunk_need.Add(isChunkNeeded(x - (int)chunkSize[0]/2, z - (int)chunkSize[1]/2));
        if (canSpwan[x - (int)chunkSize[0] / 2, z + (int)chunkSize[1] / 2] == true) is_chunk_need.Add(isChunkNeeded(x - (int)chunkSize[0]/2, z + (int)chunkSize[1]/2));

        if (canSpwan[x + (int)chunkSize[0] / 2, z] == true) is_chunk_need.Add(isChunkNeeded(x + (int)chunkSize[0]/2, z));
        if (canSpwan[x, z + (int)chunkSize[1] / 2] == true) is_chunk_need.Add(isChunkNeeded(x, z + (int)chunkSize[1]/2));
        if (canSpwan[x - (int)chunkSize[0] / 2, z] == true) is_chunk_need.Add(isChunkNeeded(x - (int)chunkSize[0]/2, z));
        if (canSpwan[x, z + (int)chunkSize[1] / 2] == true) is_chunk_need.Add(isChunkNeeded(x, z + (int)chunkSize[1]/2));

        if (canSpwan[x, z]) is_chunk_need.Add(isChunkNeeded(x, z));

        bool chunk_instantied = false;
        for(int l = 0; l < is_chunk_need.Count; l++)
        {
            if (is_chunk_need[l].needed == true)
            {
                //On créer un nouveau chunk:
                chunk_instantied = true;
                InstantiedChunk new_chunk = new InstantiedChunk();
                new_chunk.centerX = is_chunk_need[l].centerX;
                new_chunk.centerZ = is_chunk_need[l].centerZ;
                new_chunk.obj = new List<GameObject>();

                
                noiseMap = mapGenerator.noiseMap;

                for (int i = is_chunk_need[l].centerX - (int)chunkSize[0]/2; i < is_chunk_need[l].centerX + (int)chunkSize[0]/2; i++)
                {
                    for (int j = is_chunk_need[l].centerZ - (int)chunkSize[1]/2; j < is_chunk_need[l].centerZ + (int)chunkSize[1]/2; j++)
                    {
                        if (canSpwan[i, j] == true)
                        {
                            float currentHeight = noiseMap[i, j];
                            for (int k = 0; k < environnementType.Length; k++)
                            {
                                if (currentHeight > environnementType[k].startingHeightOfTerrain && currentHeight < environnementType[k].endingHeightOfTerrain && environnementType[k].spawningObject.Length > 0)
                                {
                                    int indexOfSpawn = (int)((currentHeight - environnementType[k].startingHeightOfTerrain) / (environnementType[k].endingHeightOfTerrain - environnementType[k].startingHeightOfTerrain) * environnementType[k].spawningObject.Length);
                                    
                                    new_chunk.obj.Add(Instantiate(environnementType[k].spawningObject[indexOfSpawn], new Vector3((i - mapWidth / 2.0f) * 5, mapGenerator.evaluateHeight(environnementType[k].spawningHeight), (j - mapHeight / 2.0f) * 5), Quaternion.identity, environnementType[k].parent));
                                    break;
                                }
                            }
                        }
                        
                    }
                }
                instantiedChunk.Add(new_chunk);
            }
        }

        if (chunk_instantied == true)
        {
            //Et on supprime tous les autres chunks inutiles:
            int size = instantiedChunk.Count;
            for (int i = 0; i < size; i++)
            {
                Vector2 distance = new Vector2();

                distance[0] = x - instantiedChunk[i].centerX;
                distance[1] = z - instantiedChunk[i].centerZ;

                if (Mathf.Abs(distance[0]) > 2 * (int)chunkSize[0]/2 || Mathf.Abs(distance[1]) > 2 * (int)chunkSize[1]/2)
                {
                    for (int j = 0; j < instantiedChunk[i].obj.Count; j++)
                    {
                        Destroy(instantiedChunk[i].obj[j]);
                    }
                    instantiedChunk.RemoveAt(i);
                }
            }
        }
        
      
    }
    public bool isPlayerIsInChunk(int x,int z)
    {
        for(int i = 0; i < instantiedChunk.Count; i++)
        {
            if(x<=instantiedChunk[i].centerX+(int)chunkSize[0]/2 && x >= instantiedChunk[i].centerX - (int)chunkSize[0]/2 && z <= instantiedChunk[i].centerZ + (int)chunkSize[1]/2 && z >= instantiedChunk[i].centerZ - (int)chunkSize[1]/2){
                return true;
            }
        }
        return false;
    }
    public Vector2 distanceFromInstantiedChunk(int x, int z)
    {
        Vector2 distance = new Vector2();

        for (int i = 0; i < instantiedChunk.Count; i++)
        {
            Vector2 d = new Vector2(x - instantiedChunk[i].centerX, z - instantiedChunk[i].centerZ);
            if (d.sqrMagnitude < distance.sqrMagnitude)
            {
                distance = d;
            }
        }

        return distance;
    }
    public ChunkNeeded isChunkNeeded(int x, int z)
    {
        ChunkNeeded c = new ChunkNeeded();

        //On détermine si un nouveau chunk est nécéssaire:
        if (isPlayerIsInChunk(x, z)==false)
        {
            c.needed = true;
            //Si oui on regarde quelle est le chunk le plus proche:
            Vector2 distance = new Vector2(10000,10000);
            InstantiedChunk iC = new InstantiedChunk();

            for (int i = 0; i < instantiedChunk.Count; i++)
            {
                Vector2 d = new Vector2(x - instantiedChunk[i].centerX, z - instantiedChunk[i].centerZ);
                if (d.sqrMagnitude < distance.sqrMagnitude)
                {
                    distance = d;
                    iC = instantiedChunk[i];
                }
            }

            if (instantiedChunk.Count == 0)
            {
                c.centerX = x;
                c.centerZ = z;
            }
            else
            {
                if (Mathf.Abs(distance[0]) > Mathf.Abs(distance[1]))
                {
                    if (distance[0] < 0)
                    {
                        c.centerX = iC.centerX - (int)chunkSize[0] / 2;
                        c.centerZ = iC.centerZ;
                    }
                    else
                    {
                        c.centerX = iC.centerX + (int)chunkSize[0] / 2;
                        c.centerZ = iC.centerZ;
                    }
                }
                else if (Mathf.Abs(distance[0]) < Mathf.Abs(distance[1]))
                {
                    if (distance[1] < 0)
                    {
                        c.centerX = iC.centerX;
                        c.centerZ = iC.centerZ - (int)chunkSize[1] / 2;
                    }
                    else
                    {
                        c.centerX = iC.centerX;
                        c.centerZ = iC.centerZ + (int)chunkSize[1] / 2;
                    }
                }
                else
                {
                    if (distance[0] < 0 && distance [1]>0)
                    {
                        c.centerX = iC.centerX - (int)chunkSize[0] / 2;
                        c.centerZ = iC.centerZ + (int)chunkSize[1] / 2;
                    }
                    else if (distance[0] < 0 && distance[1] < 0)
                    {
                        c.centerX = iC.centerX - (int)chunkSize[0] / 2;
                        c.centerZ = iC.centerZ - (int)chunkSize[1] / 2;
                    }
                    else if (distance[0] > 0 && distance[1] < 0)
                    {
                        c.centerX = iC.centerX + (int)chunkSize[0] / 2;
                        c.centerZ = iC.centerZ - (int)chunkSize[1] / 2;
                    }
                    else if (distance[0] > 0 && distance[1] > 0)
                    {
                        c.centerX = iC.centerX + (int)chunkSize[0] / 2;
                        c.centerZ = iC.centerZ + (int)chunkSize[1] / 2;
                    }
                }
            }
        }
        else
        {
            c.needed = false;
        }



        return c;
    }

    public int getIndexFromCoordinate(float value)
    {
        return Mathf.CeilToInt(Mathf.Round((5 * 100 + value) / 5.0f));
    }
}

[System.Serializable]
public struct EvironnementType
{
    public string name;
    public float startingHeightOfTerrain;
    public float endingHeightOfTerrain;
    public float spawningHeight;
    public GameObject[] spawningObject;
    public Transform parent;
}

[System.Serializable]
public struct InstantiedChunk
{
    public int centerX;
    public int centerZ;
    public List<GameObject> obj;
}

[System.Serializable]
public struct ChunkNeeded
{
    public bool needed;
    public int centerX;
    public int centerZ;
}

[System.Serializable]
public struct InterestingElement{
    public string name;
    public Vector2 position;
    public int size;
};