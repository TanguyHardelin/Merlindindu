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
            noiseMap = mapGenerator.noiseMap;
            isInitialized = true;
        }
    }

    public void GenerateAroundPlayer(int x, int y)
    {

        if (isInitialized == true)
        {
            if(Mathf.Abs(oldX-x)>8 || Mathf.Abs(oldY - y) > 8)
            {
                List<Chunk> all_chunks = new List<Chunk>();

                all_chunks.Add(InstantiateNewChunk(x, y));

                //all_chunks.Add(InstantiateNewChunk(x+9, y+9));
                //all_chunks.Add(InstantiateNewChunk(x+9, y-9));
                //all_chunks.Add(InstantiateNewChunk(x-9, y+9));
                //all_chunks.Add(InstantiateNewChunk(x-9, y-9));

                all_chunks.Add(InstantiateNewChunk(x, y-9));
                all_chunks.Add(InstantiateNewChunk(x, y+9));
                all_chunks.Add(InstantiateNewChunk(x-9, y));
                all_chunks.Add(InstantiateNewChunk(x+9, y));


                for(int i = 0; i < all_chunks.Count; i++)
                {
                    if (all_chunks[i].instantied == false)
                    {

                        all_chunks[i].GenerateChunk();
                        _instantied_chunks.Add(all_chunks[i]);
                    }
                }

                UnloadAllOtherChunk(all_chunks);
            }
            
        }
       
    }
    Chunk InstantiateNewChunk(int x,int y)
    {
        Chunk c = findChunk(x, y);
        if (c == null)
        {
            _chunks.Add(new Chunk(x, y, environnementType));

        }
        return c;
    }
    void UnloadAllOtherChunk(List<Chunk> all_chunks)
    {
        //Delete all other chunks:
        for (int i = 0; i < _instantied_chunks.Count; i++)
        {
            bool unload = true;
            for(int j = 0; j < all_chunks.Count; j++)
            {
                if (_instantied_chunks[i] == all_chunks[j])
                {
                    unload = false;
                }
            }
            if (unload)
            {
                _instantied_chunks[i].DeleteObjectsInChunk();
            }
        }
    }
    bool isChunkCreated(int x, int y)
    {
        for(int i=0;i< _chunks.Count; i++)
        {
            if(x==_chunks[i].x_center && y == _chunks[i].y_center)
            {
                return true;
            }
        }
        return false;
    }

    bool isChunkInstantied(int x, int y)
    {
        for (int i = 0; i < _chunks.Count; i++)
        {
            if (x == _chunks[i].x_center && y == _chunks[i].y_center)
            {
                return _chunks[i].instantied;
            }
        }
        return false;
    }

    Chunk findChunk(int x, int y)
    {
        for (int i = 0; i < _chunks.Count; i++)
        {
            if (x == _chunks[i].x_center && y == _chunks[i].y_center)
            {
                return _chunks[i];
            }
        }
        return null;
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