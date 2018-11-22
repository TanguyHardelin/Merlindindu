using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

   public string collectableType;
   public float durationRepop = 0f;
   public float ressourcePts = 0;
   public float maxRessourcePts = 0;


   private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private PlayerController playerScript;

    private GameObject floatTextPrefab;


    [SerializeField] private bool isEmpty = false;

    // Use this for initialization
    void Start() {
        collectableType = this.gameObject.name;
        initialPosition = this.transform.position;
        initialRotation = transform.rotation;

        floatTextPrefab = GameObject.FindGameObjectWithTag("floatTxt");


        switch (collectableType) {
            case "BigGoldRock":
                ressourcePts = 200;
                maxRessourcePts = 200;
                durationRepop = 100f;
                break;
            case "BigRock":
                ressourcePts = 100;
                maxRessourcePts = 100;
                durationRepop = 120f;
                break;
            case "SmallRock":
                ressourcePts = 20;
                maxRessourcePts = 20;
                durationRepop = 80f;
                break;
            case "BigTree":
                ressourcePts = 120;
                maxRessourcePts = 150;
                durationRepop = 90f;
                break;
            case "SmallTree":
                ressourcePts = 40;
                maxRessourcePts = 40;
                durationRepop = 75f;
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	public void PickRessources() {

        if (ressourcePts > 1 && !isEmpty)
        {
            switch (collectableType)
            {
                case "BigGoldRock":
                    ressourcePts -= 50;
                    this.transform.position += new Vector3(0, (float)-0.5, 0);
                    playerScript.setGold(playerScript.getGold() + 50);
                    showFloatTxtRessources("+50 G", new Color32(255, 254, 103,255));
                    break;
                case "BigRock":
                    ressourcePts -= 25;
                    this.transform.position += new Vector3(0, (float)-0.5, 0);
                    playerScript.setStone(playerScript.getStone() + 25);
                    showFloatTxtRessources("+25 S", new Color32(125, 126, 128, 255));
                    break;
                case "SmallRock":
                    ressourcePts -= 5;
                    this.transform.position += new Vector3(0, (float)-0.5, 0);
                    playerScript.setStone(playerScript.getStone() + 5);
                    showFloatTxtRessources("+5 S", new Color32(125, 126, 128, 255));
                    break;
                case "BigTree":
                    ressourcePts -= 30;
                    targetRotation = Quaternion.FromToRotation(initialRotation.eulerAngles, new Vector3(90,0,0));
                    //if (ressourcePts <= maxRessourcePts - 30) transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10);
                    playerScript.setWood(playerScript.getWood() + 30);
                    showFloatTxtRessources("+30 W",  new Color32(232, 185, 151,255));
                    break;
                case "SmallTree":
                    ressourcePts -= 10;
                    targetRotation = Quaternion.FromToRotation(initialRotation.eulerAngles, new Vector3(90, 0, 0));
                    //if (ressourcePts <= maxRessourcePts - 10) transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10);
                    playerScript.setWood(playerScript.getWood() + 10);
                    showFloatTxtRessources("+10 W", new Color32(232, 185, 151,255));
                    break;
            }
        }
        else isEmpty = true;
	}

    void Update()
    {
        float ratio = 0;
        float multiplier = 1 / durationRepop;
        playerScript = (PlayerController)GameObject.FindGameObjectWithTag("Player").GetComponent((typeof(PlayerController)));

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
                if (isEmpty)
                {
                    if (ressourcePts <= maxRessourcePts) ressourcePts += (float)0.025;
                }

                else isEmpty = false;
                break;

        }

    }

    private void showFloatTxtRessources(string txt, Color color)
    {
        GameObject newTxtFloat = Instantiate(floatTextPrefab, floatTextPrefab.transform.position + new Vector3(0, 150,0), Quaternion.identity);
        newTxtFloat.SetActive(true);
        newTxtFloat.GetComponent<FloatTextController>().SetTextandMove(txt, color);
        Destroy(newTxtFloat.gameObject, (float)0.6);
    }


    public bool getIsEmpty()
    {
        return isEmpty;
    }
}
