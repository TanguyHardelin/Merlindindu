using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField] protected RessourceType _ressources;
	public float spdTimer = 240;
    public int maxGold = 1000;
	float spdCount = 0;
    public GameObject prefab;

    public RessourceType getRessources()
    {
        return _ressources;
    }

    public void RAZRessources()
    {
        _ressources.wood = 0;
        _ressources.stone = 0;
    }

    public void updateGold(int gold) {
        _ressources.gold = gold;
    }

	// Use this for initialization
	void Start () {
		_ressources.gold = 500;

        //Instantiate(prefab, new, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        //if (!GameObject.Find("UIExploration").GetComponent<UIManager>().getFreeze()) {
            spdCount += Time.deltaTime;

            if (spdCount >= spdTimer)
            {
                _ressources.gold --;
                spdCount = 0;
            
                if(_ressources.gold <= 0)
                {
                    SceneManager.LoadScene("Loose");
                }
            }
            if (_ressources.gold >= maxGold-10)
            {
            _ressources.gold = (int)(maxGold / 4.0f*3.0f);
            spdTimer /= 2;
            }
        //}
	}

    public int getMaxGold() { return maxGold; }
    public void setMaxGold(int gold) { maxGold = gold; }
}
