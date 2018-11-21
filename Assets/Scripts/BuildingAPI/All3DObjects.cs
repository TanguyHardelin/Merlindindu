using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class All3DObjects : MonoBehaviour {
    [Header("Liste des objects 3D utilisés dans le jeu")]
    [SerializeField]
    protected StringBuilding all3DObjects;

    [SerializeField]
    protected Building currentObject =null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCurrentBuilding(string name)
    {
        if (all3DObjects[name])
        {
            currentObject = all3DObjects[name];
        }
    }

    public StringBuilding getAll3DObjects()
    {
        return all3DObjects;
    }

    public Building getCurrentObject()
    {
        return currentObject;
    }
}
