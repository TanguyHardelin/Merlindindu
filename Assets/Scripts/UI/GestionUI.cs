﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionUI : MonoBehaviour {
    //Raffaichissement de l'UI:
    public int refresh_rate = 15;
    protected int _refresh_count = 0;

    //Choix des batiments:
    protected bool _panel_visibility=false;
    protected bool _panel_need_refresh = true;
    protected string _current_batiment_type = "";
    protected GameObject _panel;
    protected GameObject _panel_info_village;

    [SerializeField]
    protected string villageTag="PlayerSystem";
    [SerializeField]
    protected GameObject _building_UI_prefab;
    [SerializeField]
    protected Vector3 _starting_position;
    [SerializeField]
    protected int _width;
    [SerializeField]
    protected int _height;
    [SerializeField]
    protected Transform _parent;

    [SerializeField]
    protected Camera _main_camera;
    [SerializeField]
    protected Camera _manage_camera;

    protected List<GameObject> _instantied_prefab = new List<GameObject>();
    protected All3DObjects _all_objects;

    //Infos générales:

    //Ressources:
    protected Village _village;

    public Text _food_text;
    public Text _stone_text;
    public Text _gold_text;
    public Text _silver_text;
    public Text _cold_text;
    public Text _wood_text;

    // Use this for initialization
    void Start () {

        _panel = GameObject.Find("PanelChoixBatiments");
        _panel_info_village = GameObject.Find("VillageInfoPanel");

        //Ressource:
        _village = GameObject.FindGameObjectWithTag(villageTag).GetComponentInChildren<Village>();
        _all_objects = GameObject.FindGameObjectWithTag(villageTag).GetComponentInChildren<All3DObjects>();

        /*
        _food_text = GameObject.Find("BleText").GetComponent<Text>();
        _stone_text = GameObject.Find("PierreText").GetComponent<Text>();
        _gold_text = GameObject.Find("OrText").GetComponent<Text>();
        _silver_text = GameObject.Find("FerText").GetComponent<Text>();
        _cold_text = GameObject.Find("CharbonText").GetComponent<Text>();
        _wood_text = GameObject.Find("BoisText").GetComponent<Text>();
        */

        Canvas UIGestion = GameObject.Find("UIGestion").GetComponent<Canvas>();
        UIGestion.enabled = true ;
        Canvas UIExploration = GameObject.Find("UIExploration").GetComponent<Canvas>();
        UIExploration.enabled=false;
    }
	
	// Update is called once per frame
	void Update () {
        _refresh_count++;
        if (_refresh_count > refresh_rate - 1)
        {
            updateUI();
            _refresh_count = 0;
        }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.MillitaryNeedOpen, openMillitaryPanel);
        Messenger.AddListener(GameEvent.RessourcesNeedOpen, openRessourcesPanel);
        Messenger.AddListener(GameEvent.VillageNeedOpen, openVillagePanel);
        Messenger.AddListener(GameEvent.SwitchToExplorationMode, switchToExplorationMode);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.MillitaryNeedOpen, openMillitaryPanel);
        Messenger.RemoveListener(GameEvent.RessourcesNeedOpen, openRessourcesPanel);
        Messenger.RemoveListener(GameEvent.VillageNeedOpen, openVillagePanel);
        Messenger.RemoveListener(GameEvent.SwitchToExplorationMode, switchToExplorationMode);
    }

    void updateUI()
    {
        //On affiche ou non le panel de gestions de sélection des batiments:
        if (_panel_need_refresh)
        {
            _panel.gameObject.SetActive(_panel_visibility);
            _panel_info_village.SetActive(!_panel_visibility);

            _panel_need_refresh = false;
        }

        //On affiche les ressources:
        updateRessource();
    }

    void updateRessource()
    {
        RessourceType actual_ressource = _village.getRessources();
        RessourceType limit_ressource = _village.getRessourcesLimit();
        _food_text.text = actual_ressource.food.ToString()+"/"+ limit_ressource.food.ToString();
        _stone_text.text = actual_ressource.stone.ToString() + "/" + limit_ressource.stone.ToString();
        _gold_text.text = actual_ressource.gold.ToString() + "/" + limit_ressource.gold.ToString();
        _silver_text.text = actual_ressource.silver.ToString() + "/" + limit_ressource.silver.ToString();
        _cold_text.text = actual_ressource.cold.ToString() + "/" + limit_ressource.cold.ToString();
        _wood_text.text = actual_ressource.wood.ToString() + "/" + limit_ressource.wood.ToString();
    }

    //Reactions aux evennements des boutons:
    void openVillagePanel()
    {
        //Ouverture du panel
        if("village"== _current_batiment_type && _panel_visibility == true) _panel_visibility = !_panel_visibility;
        else if(_panel_visibility == false) _panel_visibility = !_panel_visibility;
        _panel_need_refresh = true;
        

        //Ajout des batiments à celui-ci:
        _current_batiment_type = "village";
        if (_panel_visibility == true)
        {
            List<Building> l = _all_objects.getAllBuildingWithTag(_current_batiment_type);
            instantiateBuildingUI(l);
        }
       
    }
    void openMillitaryPanel()
    {
        //Ouverture du panel
        if ("millitary" == _current_batiment_type && _panel_visibility == true) _panel_visibility = !_panel_visibility;
        else if (_panel_visibility == false) _panel_visibility = !_panel_visibility;
        _panel_need_refresh = true;

        //Ajout des batiments à celui-ci:
        _current_batiment_type = "millitary";
        if (_panel_visibility == true)
        {
            List<Building> l = _all_objects.getAllBuildingWithTag(_current_batiment_type);
            instantiateBuildingUI(l);
        }
    }
    void openRessourcesPanel()
    {
        //Ouverture du panel
        if ("ressources" == _current_batiment_type && _panel_visibility == true) _panel_visibility = !_panel_visibility;
        else if (_panel_visibility == false) _panel_visibility = !_panel_visibility;
        _panel_need_refresh = true;

        //Ajout des batiments à celui-ci:
        _current_batiment_type = "ressources";
        if (_panel_visibility == true)
        {
            List<Building> l = _all_objects.getAllBuildingWithTag(_current_batiment_type);
            instantiateBuildingUI(l);
        }
    }
    void instantiateBuildingUI(List<Building> l)
    {
        //On enelève les prefab déjà instantié
        for(int i=0;i< _instantied_prefab.Count; i++)
        {
            Destroy(_instantied_prefab[i]);
        }
        //On les supprime de la liste
        for (int i = 0; i < _instantied_prefab.Count; i++)
        {
            _instantied_prefab.RemoveAt(0);
        }
        //On en instantié de nouveau
        for (int i = 0; i < l.Count; i++)
        {
            Vector3 new_position = _starting_position;
            new_position[0] += i*( _width + 10.0f * _width / 100.0f );
            GameObject obj=Instantiate(_building_UI_prefab, new_position, Quaternion.identity,_parent) as GameObject;
            
            BuildingElementScript s= obj.GetComponent<BuildingElementScript>();
            s.objectName = l[i].getName();
            s.imageTexture = l[i].getIcon();
            s.initialize();

            popupInfoBuilding p= obj.GetComponent<popupInfoBuilding>();
            p.position = new_position;
            p.position.x += 130;
            p.position.y += 220;
            p.parent = GameObject.Find("UIGestion").transform;
            p.nom = l[i].getName();
            p.description = l[i].getDescription();
            p.ressourcesNeeded = l[i].getRessourcesNeeded();
            p.curentRessources = _village.getRessources();

            _instantied_prefab.Add(obj);
        }
    }
    void switchToExplorationMode()
    {
        _main_camera.enabled = true;
        _manage_camera.enabled = false;

        Canvas UIGestion = GameObject.Find("UIGestion").GetComponent<Canvas>();
        UIGestion.enabled = false;
        Canvas UIExploration = GameObject.Find("UIExploration").GetComponent<Canvas>();
        UIExploration.enabled = true;
    }
}