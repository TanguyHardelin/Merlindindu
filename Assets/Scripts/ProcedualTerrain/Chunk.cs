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

    public Chunk(int xCenter, int yCenter, EvironnementType[] environnementType)
    {
        x_center = xCenter;
        y_center = yCenter;
        environnement_type = environnementType;

        subChunks.Add(new SubChunk(x_center, y_center, environnement_type));

        
        subChunks.Add(new SubChunk(x_center + 3, y_center + 3, environnement_type));
        subChunks.Add(new SubChunk(x_center - 3, y_center + 3, environnement_type));
        subChunks.Add(new SubChunk(x_center + 3, y_center - 3, environnement_type));
        subChunks.Add(new SubChunk(x_center - 3, y_center - 3, environnement_type));

        subChunks.Add(new SubChunk(x_center, y_center + 3, environnement_type));
        subChunks.Add(new SubChunk(x_center, y_center - 3, environnement_type));
        subChunks.Add(new SubChunk(x_center + 3, y_center, environnement_type));
        subChunks.Add(new SubChunk(x_center - 3, y_center, environnement_type));
        

        initialized = true;
        
    }

    public void GenerateChunk()
    {
        
        if (instantied == false && initialized==true)
        {
            for (int i = 0; i < subChunks.Count; i++)
            {
                subChunks[i].Instantiate();
                
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
        if (instantied == true)
        {
            for (int i = 0; i < subChunks.Count; i++)
            {
                subChunks[i].Destroy();
            }
            instantied = false;
        }
    }

    

}
