using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingElementScript : MonoBehaviour {

    [SerializeField]
    protected string villageTag = "PlayerSystem";


    //Paramétrage du batiment
    public string objectName;
    public Texture imageTexture;
    public string description;
    public RessourceType ressourceNeeded;
    protected All3DObjects _all_objects;

    protected EventTrigger eventTrigger;

    protected Button _button;

    void Start () {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => changeCurrentObject());

        _all_objects = GameObject.FindGameObjectWithTag(villageTag).GetComponentInChildren<All3DObjects>();
    }

	void Update () {
		
	}

    public void initialize()
    {
        Text textName = GetComponentInChildren<Text>();
        textName.text = objectName;
        RawImage image= GetComponentInChildren<RawImage>();
        image.texture = imageTexture;

        //On ajoute la callback pour le boutton_button.onClick.AddListener(() => changeCurrentObject());
        
    }

    void changeCurrentObject()
    {
        //On change l'object courant dans All3DObject:
        _all_objects.setCurrentBuilding(objectName);
    }
}
