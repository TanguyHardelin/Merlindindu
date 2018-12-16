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
    public float distanceMin = 10f;
    public float distanceMax = 100f;

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
            Vector3 newPosition = this.transform.position + sens * direction * scrollSpeed;
            Vector3 CameraPositionToTheGround = Vector3.zero;
            CameraPositionToTheGround.x = newPosition.x;
            CameraPositionToTheGround.z = newPosition.z;
            float DistanceToTheGround = Vector3.Distance(newPosition, CameraPositionToTheGround);
            if (DistanceToTheGround > distanceMin && DistanceToTheGround < distanceMax) {
                this.transform.position = newPosition;
            }
        }
    }
        
    protected void stopBuilding()
    {
        _buildingAPI.stopBuilding();
    }
    protected void addGhostBuildingToScene(Vector3 position)
    {
        Vector3 old = position;
        position.x = - old.x -5;
        position.z =  -1*(old.z+5);
        _buildingAPI.spawnGhostBuilding(position);
    }
    protected void addBuildingToScene(Vector3 position)
    {
        Vector3 old = position;
        position.x = - old.x -5;
        position.z = -1 * (old.z +5 );
        _buildingAPI.spawnBuilding(position);
    }
}
