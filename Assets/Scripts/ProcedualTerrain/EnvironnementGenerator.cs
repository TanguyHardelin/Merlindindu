using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnvironnementGenerator : MonoBehaviour {
    protected MapGenerator mapGenerator;
    [SerializeField]
    protected List<Chunk> _chunks=new List<Chunk>();

    [SerializeField]
    protected List<Chunk> _instantied_chunks = new List<Chunk>();
    [SerializeField]
    protected EvironnementType[] environnementType;

    protected int mapWidth = 0;
    protected int mapHeight = 0;

    protected float[,] noiseMap;

    protected bool isInitialized = false;

    //Internal atribrut usefull for optimisation:
    protected int oldX = 1000;
    protected int oldY = 1000;


    protected List<InstantiedChunk> instantiedChunk = new List<InstantiedChunk>();

    // Use this for initialization
    void Start () {
        mapGenerator = FindObjectOfType<MapGenerator>();
        mapWidth = mapGenerator.mapWidth;
        mapHeight = mapGenerator.mapHeight;
        noiseMap = mapGenerator.noiseMap;
        
        
        
        
    }
    
	
	// Update is called once per frame
	void Update () {
        if (mapGenerator.isInitialized == true && isInitialized ==false)
        {
            /*
            noiseMap = mapGenerator.noiseMap;
            Debug.Log(noiseMap);
            Debug.Log(mapGenerator.isInitialized);

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    float currentHeight = noiseMap[x, y];
                    for (int i = 0; i < environnementType.Length; i++)
                    {
                        if (currentHeight < environnementType[i].endingHeightOfTerrain && environnementType[i].spawningObject.Length > 0)
                        {
                            int indexOfSpawn = (int)UnityEngine.Random.Range(0.0f, environnementType[i].spawningObject.Length - 1);
                            //Debug.Log("x = " + x + " y = " + y + "rX=" + (x - mapWidth / 2.0f) * 5.0f + " rY=" + (y - mapHeight / 2.0f) * 5.0f+ "currentHeight= "+ currentHeight+" Type =" + environnement_type[i].name);
                            Instantiate(environnementType[i].spawningObject[indexOfSpawn]);

                        }

                    }
                }
            }
            isInitialized = true;
            */
        }
    }

    public void GenerateAroundPlayer(int x, int z)
    {
        //On vérifie si on doit instancié un chunk
        List<ChunkNeeded> is_chunk_need = new List<ChunkNeeded>();
        is_chunk_need.Add(isChunkNeeded(x + 6, z - 4 ));
        is_chunk_need.Add(isChunkNeeded(x + 6, z + 4));
        is_chunk_need.Add(isChunkNeeded(x - 6, z - 4));
        is_chunk_need.Add(isChunkNeeded(x - 6, z + 4));

        is_chunk_need.Add(isChunkNeeded(x + 6, z));
        is_chunk_need.Add(isChunkNeeded(x, z + 4));
        is_chunk_need.Add(isChunkNeeded(x - 6, z));
        is_chunk_need.Add(isChunkNeeded(x, z + 4));

        is_chunk_need.Add(isChunkNeeded(x, z));

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
                for (int i = is_chunk_need[l].centerX - 6; i < is_chunk_need[l].centerX + 6; i++)
                {
                    for (int j = is_chunk_need[l].centerZ - 4; j < is_chunk_need[l].centerZ + 4; j++)
                    {
                        float currentHeight = noiseMap[x, z];
                        for (int k = 0; k < environnementType.Length; k++)
                        {
                            if (currentHeight < environnementType[k].endingHeightOfTerrain && environnementType[k].spawningObject.Length > 0)
                            {
                                int indexOfSpawn = (int)UnityEngine.Random.Range(0.0f, environnementType[k].spawningObject.Length - 1);
                                //Debug.Log("x = " + x + " y = " + y + "rX=" + (x - mapWidth / 2.0f) * 5.0f + " rY=" + (y - mapHeight / 2.0f) * 5.0f+ "currentHeight= "+ currentHeight+" Type =" + environnement_type[i].name);
                                new_chunk.obj.Add(Instantiate(environnementType[k].spawningObject[indexOfSpawn], new Vector3((i - mapWidth / 2.0f) * 5, mapGenerator.evaluateHeight(environnementType[k].spawningHeight), (j - mapHeight / 2.0f) * 5), Quaternion.identity, environnementType[k].parent));

                            }

                        }
                    }
                }

                instantiedChunk.Add(new_chunk);
                Debug.Log("Instantiation d'un nouveau chunk a x=" + new_chunk.centerX + " z=" + new_chunk.centerZ);

                
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

                if (Mathf.Abs(distance[0]) > 2 * 6 || Mathf.Abs(distance[1]) > 2 * 4)
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
            if(x<=instantiedChunk[i].centerX+6 && x >= instantiedChunk[i].centerX - 6 && z <= instantiedChunk[i].centerZ + 4 && z >= instantiedChunk[i].centerZ - 4){
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
                //Debug.Log("Distance x=" + (x - instantiedChunk[i].centerX));
                //Debug.Log("Distance z=" + (z - instantiedChunk[i].centerZ));
                Vector2 d = new Vector2(x - instantiedChunk[i].centerX, z - instantiedChunk[i].centerZ);
                if (d.sqrMagnitude < distance.sqrMagnitude)
                {
                    distance = d;
                    iC = instantiedChunk[i];
                }
            }

            Debug.Log("Distance x=" + distance[0]);
            Debug.Log("Distance z=" + distance[1]);
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
                        c.centerX = iC.centerX - 12;
                        c.centerZ = iC.centerZ;
                    }
                    else
                    {
                        c.centerX = iC.centerX + 12;
                        c.centerZ = iC.centerZ;
                    }
                }
                else if (Mathf.Abs(distance[0]) < Mathf.Abs(distance[1]))
                {
                    if (distance[1] < 0)
                    {
                        c.centerX = iC.centerX;
                        c.centerZ = iC.centerZ - 8;
                    }
                    else
                    {
                        c.centerX = iC.centerX;
                        c.centerZ = iC.centerZ + 8;
                    }
                }
                else
                {
                    if (distance[0] < 0 && distance [1]>0)
                    {
                        c.centerX = iC.centerX - 12;
                        c.centerZ = iC.centerZ + 8;
                    }
                    else if (distance[0] < 0 && distance[1] < 0)
                    {
                        c.centerX = iC.centerX - 12;
                        c.centerZ = iC.centerZ - 8;
                    }
                    else if (distance[0] > 0 && distance[1] < 0)
                    {
                        c.centerX = iC.centerX + 12;
                        c.centerZ = iC.centerZ - 8;
                    }
                    else if (distance[0] > 0 && distance[1] > 0)
                    {
                        c.centerX = iC.centerX + 12;
                        c.centerZ = iC.centerZ + 8;
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
        return Mathf.CeilToInt(Mathf.Round(5 * 100 + value) / 5.0f);
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