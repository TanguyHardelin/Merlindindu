using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCameraSpawner : MonoBehaviour
{
    protected Camera _camera;
    protected BuidingAPI _buildingAPI;

    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
        _buildingAPI = FindObjectOfType<BuidingAPI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    Vector3 position = hit.point;
                    addBuildingToScene(position);
                }
            }

        }
    }

    protected void addBuildingToScene(Vector3 position)
    {
        _buildingAPI.spawnBatiments(position.x,position.y,position.z);
    }
}
