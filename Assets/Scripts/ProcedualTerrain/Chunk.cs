using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk{

    public int x_center;
    public int y_center;
    protected EvironnementType[] environnement_type;

    public List<SubChunk> subChunks = new List<SubChunk>();

    public bool instantied = false;
    public bool initialized = false;

    public Chunk(int xCenter, int yCenter, EvironnementType[] environnementType, int mapWidth,int mapHeight,float [,] noiseMap)
    {
        x_center = xCenter;
        y_center = yCenter;
        environnement_type = environnementType;

      
        initialized = true;
        
    }

    public void GenerateChunk()
    {
        
        if (instantied == false && initialized==true)
        {
            for (int i = 0; i < subChunks.Count; i++)
            {
                if (subChunks[i].instantiate == false)
                {
                    subChunks[i].Instantiate();
                }
                
                
            }
            instantied = true;
        }
    }

    protected bool IsChunkInstantied()
    {
        return instantied;
    }
    
    public void DeleteObjectsInChunk()
    {
        int size = subChunks.Count;
        for (int i = 0; i < size; i++)
        {
            subChunks[0].Destroy();
            subChunks.RemoveAt(0);
        }
        instantied = false;
        
    }

    

}
