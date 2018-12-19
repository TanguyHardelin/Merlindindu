using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class popupInfoBuilding : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 position;
    
    public Transform parent;

    public string nom;
    public string description;
    public RessourceType ressourcesNeeded;
    public RessourceType currentRessources;

    public Text textName;
    public Text textDescription;

    protected float current_time = 2;

    private void Update()
    {
        /*
        current_time += Time.deltaTime;
        if (current_time > 1)
        {
            actualizeText();
            current_time = 0;
        }
        */
    }

    [SerializeField]
    protected GameObject _popup;

    protected GameObject _instantied_obj;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _instantied_obj = Instantiate(_popup, position, Quaternion.identity, parent) as GameObject;
        actualizeText();
    }

    
    private void Awake()
    {
        Messenger.AddListener(GameEvent.BuildingSpawn, actualizeRessources);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BuildingSpawn, actualizeRessources);
    }


    public void actualizeRessources()
    {
        currentRessources = GameObject.Find("Village").GetComponent<Village>().getRessources();
    }

    public void actualizeText()
    {
        Text nameText = GameObject.Find("NameText").GetComponent<Text>();
        nameText.text = nom;

        Text desciptionName = GameObject.Find("DescriptionText").GetComponent<Text>();
        desciptionName.text = description;
        

        Text pierreText = GameObject.Find("PopupPierreText").GetComponent<Text>();

        if (currentRessources.stone < ressourcesNeeded.stone)
        {
            pierreText.color = Color.red;
            pierreText.fontStyle = FontStyle.Bold;
        }

        pierreText.text = ressourcesNeeded.stone.ToString();

        Text boisText = GameObject.Find("PopupBoisText").GetComponent<Text>();

        if (currentRessources.wood < ressourcesNeeded.wood)
        {
            boisText.color = Color.red;
            boisText.fontStyle = FontStyle.Bold;
        }

        boisText.text = ressourcesNeeded.wood.ToString();

        Text orText = GameObject.Find("PopupOrText").GetComponent<Text>();

        if (currentRessources.gold < ressourcesNeeded.gold)
        {
            orText.color = Color.red;
            orText.fontStyle = FontStyle.Bold;
        }

        orText.text = ressourcesNeeded.gold.ToString();

        Text citizenText = GameObject.Find("PopupCitizenText").GetComponent<Text>();
        if (currentRessources.citizen < ressourcesNeeded.citizen)
        {
            citizenText.color = Color.red;
            citizenText.fontStyle = FontStyle.Bold;
        }

        citizenText.text = ressourcesNeeded.citizen.ToString();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        actualizeText();
        Destroy(_instantied_obj);
        actualizeText();
    }
}
