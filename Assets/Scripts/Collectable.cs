using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public string collectableType;
    public int durationRepop = 0;
    public float ressourcePts = 0;
    public float maxRessourcePts = 0;
    Player player;
    private bool isEmpty;
    public int timer;


    protected Vector3 initialPosition;
    protected Vector3 notAvailablePosition;
    protected Quaternion initialRotation;
    protected Quaternion targetRotation;
    private GameObject floatTextPrefab;
    //protected Ressources playerRessources;


    // Use this for initialization
    void Start() {
        player = FindObjectOfType<Player>();
        
        collectableType = this.gameObject.name;
        initialPosition = this.transform.position;
        initialRotation = transform.rotation;
        floatTextPrefab = GameObject.FindGameObjectWithTag("floatTxt");

        switch (collectableType) {
            case "BigGoldRock":
                ressourcePts = 200;
                maxRessourcePts = 200;
                durationRepop = 3000;
                notAvailablePosition = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, 2f, 0), 10f);
                break;
            case "BigRock":
                ressourcePts = 100;
                maxRessourcePts = 100;
                durationRepop = 2000;
                notAvailablePosition = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, 3.5f, 0), 10f);
                break;
            case "SmallRock":
                ressourcePts = 20;
                maxRessourcePts = 20;
                durationRepop = 1500;
                notAvailablePosition = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, 0.5f, 0), 10f);

                break;
            case "BigTree":
                ressourcePts = 120;
                maxRessourcePts = 120;
                durationRepop = 2000;
                notAvailablePosition = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, 4f, 0), 10f);
                break;
            case "SmallTree":
                ressourcePts = 40;
                maxRessourcePts = 40;
                durationRepop = 1500;
                notAvailablePosition = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, 4.5f, 0), 10f);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	public void PickRessources() {

        if (ressourcePts > 0)
        {
            switch (collectableType)
            {
                case "BigGoldRock":
                    ressourcePts -= 50;
                    player.getRessources().gold += 50;
                    player.GetComponent<AudioSource>().volume = 1;
                    var audioClip = Resources.Load<AudioClip>("Sounds/Player/collectStone");
                    player.GetComponent<AudioSource>().clip = audioClip;
                    player.GetComponent<AudioSource>().Play();
                    showFloatTxtRessources("+50 G", new Color32(255, 254, 103, 255));
                    break;
                case "BigRock":
                    ressourcePts -= 25;
                    player.getRessources().stone += 25;
                    player.GetComponent<AudioSource>().volume = 1;
                    var audioClip2 = Resources.Load<AudioClip>("Sounds/Player/collectStone");
                    player.GetComponent<AudioSource>().clip = audioClip2;
                    player.GetComponent<AudioSource>().Play();
                    showFloatTxtRessources("+25 S", new Color32(125, 126, 128, 255));
                    break;
                case "SmallRock":
                    ressourcePts -= 5;
                    player.getRessources().stone += 5;
                    player.GetComponent<AudioSource>().volume = 1;
                    var audioClip3 = Resources.Load<AudioClip>("Sounds/Player/collectStone");
                    player.GetComponent<AudioSource>().clip = audioClip3;
                    player.GetComponent<AudioSource>().Play();
                    showFloatTxtRessources("+5 S", new Color32(125, 126, 128, 255));
                    break;
                case "BigTree":
                    ressourcePts -= 30;
                    player.getRessources().wood += 30;
                    player.GetComponent<AudioSource>().volume = 1;
                    var audioClip4 = Resources.Load<AudioClip>("Sounds/Player/collectWoodMP3");
                    player.GetComponent<AudioSource>().clip = audioClip4;
                    player.GetComponent<AudioSource>().Play();
                    showFloatTxtRessources("+30 W", new Color32(232, 185, 151, 255));
                    break;
                case "SmallTree":
                    ressourcePts -= 10;
                    player.getRessources().wood += 10;
                    player.GetComponent<AudioSource>().volume = 1;
                    var audioClip5 = Resources.Load<AudioClip>("Sounds/Player/collectWoodMP3");
                    player.GetComponent<AudioSource>().clip = audioClip5;
                    player.GetComponent<AudioSource>().Play();
                    showFloatTxtRessources("+10 W", new Color32(232, 185, 151, 255));
                    break;
            }
            if (ressourcePts <= 0) {
                this.transform.position = notAvailablePosition;
                isEmpty = true;
                timer = 0;
            }
        }
	}

    void Update()
    {
        timer++;
        if (isEmpty && timer > durationRepop) {
            this.transform.position = initialPosition;
            isEmpty = false;
        }
    }

    private void showFloatTxtRessources(string txt, Color color)
    {
        GameObject newTxtFloat = Instantiate(floatTextPrefab, floatTextPrefab.transform.position + new Vector3(0, 150, 0), Quaternion.identity);
        newTxtFloat.SetActive(true);
        newTxtFloat.GetComponent<FloatTextController>().SetTextandMove(txt, color);
        Destroy(newTxtFloat.gameObject, (float)0.6);
    }

    public bool getIsEmpty() {
        return isEmpty;
    }
}
