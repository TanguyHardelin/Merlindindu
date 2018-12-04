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
    public RessourceType curentRessources;

    public Text textName;
    public Text textDescription;



    [SerializeField]
    protected GameObject _popup;

    protected GameObject _instantied_obj;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _instantied_obj = Instantiate(_popup, position, Quaternion.identity, parent) as GameObject;
        actualizeText();
    }

    public void actualizeText()
    {
        Text nameText = GameObject.Find("NameText").GetComponent<Text>();
        nameText.text = nom;

        Text desciptionName = GameObject.Find("DescriptionText").GetComponent<Text>();
        desciptionName.text = description;

        Text bleText = GameObject.Find("PopupBleText").GetComponent<Text>();

        if(curentRessources.food< ressourcesNeeded.food)
        {
            bleText.color = Color.red;
            bleText.fontStyle = FontStyle.Bold;
        }

        bleText.text = ressourcesNeeded.food.ToString();

        Text ferText = GameObject.Find("PopupFerText").GetComponent<Text>();

        if (curentRessources.silver < ressourcesNeeded.silver)
        {
            ferText.color = Color.red;
            ferText.fontStyle = FontStyle.Bold;
        }

        ferText.text = ressourcesNeeded.silver.ToString();

        Text pierreText = GameObject.Find("PopupPierreText").GetComponent<Text>();

        if (curentRessources.stone < ressourcesNeeded.stone)
        {
            pierreText.color = Color.red;
            pierreText.fontStyle = FontStyle.Bold;
        }

        pierreText.text = ressourcesNeeded.stone.ToString();

        Text boisText = GameObject.Find("PopupBoisText").GetComponent<Text>();

        if (curentRessources.wood < ressourcesNeeded.wood)
        {
            boisText.color = Color.red;
            boisText.fontStyle = FontStyle.Bold;
        }

        boisText.text = ressourcesNeeded.wood.ToString();

        Text orText = GameObject.Find("PopupOrText").GetComponent<Text>();

        if (curentRessources.stone < ressourcesNeeded.stone)
        {
            orText.color = Color.red;
            orText.fontStyle = FontStyle.Bold;
        }

        orText.text = ressourcesNeeded.stone.ToString();

        Text charbonText = GameObject.Find("PopupCharbonText").GetComponent<Text>();

        if (curentRessources.cold < ressourcesNeeded.cold)
        {
            charbonText.color = Color.red;
            charbonText.fontStyle = FontStyle.Bold;
        }

        charbonText.text = ressourcesNeeded.cold.ToString();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        actualizeText();
        Destroy(_instantied_obj);
        actualizeText();
    }
}
