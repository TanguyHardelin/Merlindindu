using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public string collectableType;
    public float durationRepop = 0f;
    public float ressourcePts = 0;
    public float maxRessourcePts = 0;
    Player player;


    protected Vector3 initialPosition;
    protected Quaternion initialRotation;
    protected Quaternion targetRotation;
    //protected Ressources playerRessources;


    [SerializeField] private bool isEmpty = false;

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<Player>();
        
        collectableType = this.gameObject.name;
        initialPosition = this.transform.position;
        initialRotation = transform.rotation;
        //playerRessources = (Ressources)GameObject.FindGameObjectWithTag("Player").GetComponent((typeof(Ressources)));


        switch (collectableType) {
            case "BigGoldRock":
                ressourcePts = 250;
                maxRessourcePts = 250;
                durationRepop = 160f;
                break;
            case "BigRock":
                ressourcePts = 100;
                maxRessourcePts = 100;
                durationRepop = 180f;
                break;
            case "SmallRock":
                ressourcePts = 20;
                maxRessourcePts = 20;
                durationRepop = 120f;
                break;
            case "BigTree":
                ressourcePts = 120;
                maxRessourcePts = 150;
                durationRepop = 130f;
                break;
            case "SmallTree":
                ressourcePts = 40;
                maxRessourcePts = 40;
                durationRepop = 125f;
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	public void PickRessources() {

        if (ressourcePts > 0 && !isEmpty)
        {
            switch (collectableType)
            {
                case "BigGoldRock":
                    ressourcePts -= 50;
                    player.getRessources().gold += 50;
                    this.transform.position += new Vector3(0, (float)-0.5, 0);
                    break;
                case "BigRock":
                    ressourcePts -= 25;
                    player.getRessources().stone += 25;
                    this.transform.position += new Vector3(0, (float)-0.25, 0);
                    break;
                case "SmallRock":
                    ressourcePts -= 5;
                    player.getRessources().stone += 5;
                    this.transform.position += new Vector3(0, (float)-0.5, 0);
                    break;
                case "BigTree":
                    ressourcePts -= 30;
                    player.getRessources().wood += 30;
                    targetRotation = Quaternion.FromToRotation(initialRotation.eulerAngles, new Vector3(90,0,0));
                    break;
                case "SmallTree":
                    ressourcePts -= 10;
                    player.getRessources().wood += 10;
                    targetRotation = Quaternion.FromToRotation(initialRotation.eulerAngles, new Vector3(90, 0, 0));
                    break;
            }
        }
        else isEmpty = true;
	}

    void Update()
    {
        float ratio = 0;
        float multiplier = 1 / durationRepop;
        

        switch (collectableType)
        {
            case "BigGoldRock":
                if (isEmpty && this.transform.position.y <= initialPosition.y - 0.05)
                {
                    ratio += Time.deltaTime * multiplier;
                    if (ressourcePts <= maxRessourcePts) ressourcePts += (float)10;
                    transform.position = Vector3.Lerp(this.transform.position, initialPosition, ratio);
                }

                else isEmpty = false;
                break;
            case "BigRock":
            case "SmallRock":
                if (isEmpty && this.transform.position.y <= initialPosition.y - 0.05)
                {
                    ratio += Time.deltaTime * multiplier;
                    if (ressourcePts <= maxRessourcePts) ressourcePts += (float)10;
                    transform.position = Vector3.Lerp(this.transform.position, initialPosition, ratio);
                }

                else isEmpty = false;
                break;
            case "BigTree":
            case "SmallTree":
                if (isEmpty && this.transform.rotation != initialRotation)
                {
                    if (ressourcePts <= maxRessourcePts) ressourcePts += (float)0.025;
                    if (ressourcePts >= maxRessourcePts) transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, (float)0.1);
                    Debug.Log(this.transform.rotation == initialRotation);
                }

                else isEmpty = false;
                break;

        }

    }

    public bool getIsEmpty()
    {
        return isEmpty;
    }
}
