using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingCameraSpawner : MonoBehaviour
{
    [SerializeField]
    protected string villageTag;
    protected Camera _camera;
    protected BuidingAPI _buildingAPI;

    public float speed = 0.2f;
    public float scrollSpeed = 2f;
    public float moveSpeed = 0.5f;

    protected Vector3 button_1_old_position = new Vector3(0, 0, 0);
    protected Vector3 button_2_old_position = new Vector3(0, 0, 0);
    protected float angleX = 360;

    protected int levelOfZoom = 0;

    // Use this for initialization
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _buildingAPI = GameObject.FindGameObjectWithTag(villageTag).GetComponentInChildren<BuidingAPI>();
    }
    // Update is called once per frame
    void Update()
    {
        //Spawn des batiments
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,Mathf.Infinity, 9))
                {
                    if (hit.transform != null)
                    {
                        Vector3 position = hit.point;
                        addBuildingToScene(position);
                    }
                }
            }
        }
        //Prévisualisation des batiments
        if(Input.GetAxis("Mouse X") != 0)
        {
            Vector3 mousePosition = Input.mousePosition;
            //Preview:
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
            {
                if (hit.transform != null)
                {
                    Vector3 position = hit.point;
                    addGhostBuildingToScene(position);
                }
            }
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            //Preview:
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
            {
                if (hit.transform != null)
                {
                    Vector3 position = hit.point;
                    addGhostBuildingToScene(position);
                }
            }
        }
        //Arret de la contruction du batiments:
        if (Input.GetMouseButtonDown(1))
        {
            stopBuilding();
        }

        //Gestion orientation:
        if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0) {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            this.transform.Translate(direction * speed, Space.World);
        }

        //Gestion zoom / dezoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            Vector3 direction = this.transform.forward;
            float sens = Input.GetAxis("Mouse ScrollWheel");
            this.transform.position += sens * direction * scrollSpeed;
        }

        //Gestion des touches:
        if (Input.GetKey(KeyCode.R))
        {
            applyRotation();
        }

    }
    protected void applyRotation()
    {
        _buildingAPI.apply90RotationDegree();
    }
    protected void stopBuilding()
    {
        _buildingAPI.stopBuilding();
    }
    protected void addGhostBuildingToScene(Vector3 position)
    {
        _buildingAPI.spawnGhostBuilding(position);
    }
    protected void addBuildingToScene(Vector3 position)
    {
        _buildingAPI.spawnBuilding(position);
    }
}
