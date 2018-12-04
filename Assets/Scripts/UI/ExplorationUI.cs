using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationUI : MonoBehaviour {
    [SerializeField]
    protected Camera _main_camera;
    [SerializeField]
    protected Camera _manage_camera;

    //Player:
    protected Player _player;

    //Partie ressources:
    public Text _food_text;
    public Text _stone_text;
    public Text _gold_text;
    public Text _silver_text;
    public Text _cold_text;
    public Text _wood_text;

    public Text _text_info;
    protected string _info_text;

    public GameObject _panelTextInfo;

    //Partie gestion des panels ect...
    protected int _refresh_count = 0;
    public int refresh_rate = 15;
    protected bool _panel_visibility = false;
    protected bool _panel_need_refresh = true;

    public void setInfoText(string text)
    {
        _info_text = text;
        _text_info.text = _info_text;
    }

    public void setInfoPanelVisibility(bool visibility)
    {
        _panel_need_refresh=true;
        _panel_visibility = visibility;
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.SwitchToGestionMode, switchToGestionMode);
    }

    private void OnDestroy()
    {
        
        Messenger.RemoveListener(GameEvent.SwitchToGestionMode, switchToGestionMode);
    }

    // Use this for initialization
    void Start () {
        _player = GameObject.FindObjectOfType<Player>();
        _panelTextInfo = GameObject.Find("PanelTextInfo");
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

    void updateUI()
    {
        //On affiche ou non le panel de gestions de sélection des batiments:
        if (_panel_need_refresh)
        {
            _panelTextInfo.gameObject.SetActive(_panel_visibility);
            _panel_need_refresh = false;
        }

        //On affiche les ressources:
        updateRessource();
    }

    void updateRessource()
    {
        RessourceType actual_ressource = _player.getRessources();
        _food_text.text = actual_ressource.food.ToString();
        _stone_text.text = actual_ressource.stone.ToString();
        _gold_text.text = actual_ressource.gold.ToString();
        _silver_text.text = actual_ressource.silver.ToString();
        _cold_text.text = actual_ressource.cold.ToString();
        _wood_text.text = actual_ressource.wood.ToString();
    }

    void switchToGestionMode()
    {
        _main_camera.enabled = false;
        _manage_camera.enabled = true;

        Canvas UIGestion = GameObject.Find("UIGestion").GetComponent<Canvas>();
        UIGestion.enabled = true;
        Canvas UIExploration = GameObject.Find("UIExploration").GetComponent<Canvas>();
        UIExploration.enabled = false;
    }
    
}
