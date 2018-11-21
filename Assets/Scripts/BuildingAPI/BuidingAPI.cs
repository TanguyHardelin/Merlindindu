using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BuidingAPI : MonoBehaviour {
    [SerializeField]
    protected Village villageReference;

    [SerializeField]
    protected Transform buildingParents;

    protected All3DObjects all3DObjectsScript;
    protected EnvironnementGenerator environnementGenerator;
    protected MapGenerator mapGenerator;


    public bool[,] caseIsOccuped ;

    // Use this for initialization
    void Start () {
        all3DObjectsScript = FindObjectOfType<All3DObjects>();
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


    public void spawnBatiments(float x, float y, float z)
    {
        Vector3 building_position = new Vector3(environnementGenerator.getIndexFromCoordinate(x), y, environnementGenerator.getIndexFromCoordinate(z));

        if(caseIsOccuped[(int)building_position.x, (int)building_position.z] == false)
        {
            caseIsOccuped[(int)building_position.x, (int)building_position.z] = true;
            building_position = caseToCoordonnate(building_position);


            Building new_building = all3DObjectsScript.getCurrentObject();
            new_building.setParent(buildingParents);



            Instantiate(new_building, building_position, Quaternion.identity, buildingParents);
        }
        else
        {
            Debug.Log("Could not instantiate this building");
        }

    }
}
