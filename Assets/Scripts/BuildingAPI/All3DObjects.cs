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

    private void Awake()
    {
        Messenger.AddListener(GameEvent.SwitchToExplorationMode, setCurrentObjToNull);
    }

    private void OnDestroy()
    {
        
        Messenger.RemoveListener(GameEvent.SwitchToExplorationMode, setCurrentObjToNull);
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

    public void setCurrentObjToNull()
    {
        currentObject = null;
    }

    public List<Building> getAllBuildingWithTag(string tag)
    {
        List<Building> all_building_with_tag = new List<Building>();

        foreach(string keys in all3DObjects.Keys)
        {
            if (all3DObjects[keys].getTag() == tag)
            {
                all_building_with_tag.Add(all3DObjects[keys]);
            }
        }
        return all_building_with_tag;
    }
}
