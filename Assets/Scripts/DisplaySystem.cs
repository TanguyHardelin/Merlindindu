using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySystem : MonoBehaviour {

	public GameObject messagePanel;
	public Canvas UIExploration;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void OpenMessagePanel(string txt)
    {
        messagePanel.SetActive(true);
        if (txt != null) messagePanel.transform.GetChild(0).GetComponent<Text>().text = txt;
    }

    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
    }

	//Enable or not UIElement that corresponds to manageMode
    public void UIManage(bool on)
    {
        if (on)
        {
            UIExploration.transform.GetChild(0).GetComponent<Image>().enabled = false;
            UIExploration.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            UIExploration.transform.GetChild(3).GetComponent<Image>().enabled = false;
            UIExploration.transform.GetChild(3).GetChild(0).GetComponent<RawImage>().enabled = false;

            UIExploration.transform.GetChild(6).GetComponent<Image>().enabled = true; //Placebuildings pannel
            UIExploration.transform.GetChild(6).GetChild(0).GetComponent<Image>().enabled = true; //ScrollView
            for (int i = 0; i < 9; i++)
            {
                UIExploration.transform.GetChild(6).GetChild(0).GetChild(i).GetComponent<Image>().enabled = true; //Basic House background etc..
                UIExploration.transform.GetChild(6).GetChild(0).GetChild(i).GetChild(0).GetComponent<RawImage>().enabled = true; //Basic House etc
                UIExploration.transform.GetChild(6).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<RawImage>().enabled = true; //Basic house button etc
            }
            UIExploration.transform.GetChild(7).GetComponent<Image>().enabled = true;//ScrollBar
            UIExploration.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;//ScrollBar
            UIExploration.transform.GetChild(8).GetComponent<Image>().enabled = true;//2nd building Panel (Quit button)
            UIExploration.transform.GetChild(8).GetChild(0).GetComponent<Image>().enabled = true;//2nd building Panel (Quit button)
            UIExploration.transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;//2nd building Panel (Quit button)
        }
        else
        {   
            UIExploration.transform.GetChild(0).GetComponent<Image>().enabled = true;
            UIExploration.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            UIExploration.transform.GetChild(3).GetComponent<Image>().enabled = true;
            UIExploration.transform.GetChild(3).GetChild(0).GetComponent<RawImage>().enabled = true;

            UIExploration.transform.GetChild(6).GetComponent<Image>().enabled = false; //Placebuildings pannel
            UIExploration.transform.GetChild(6).GetChild(0).GetComponent<Image>().enabled = false; //ScrollView
            for (int i = 0; i<9; i++)
            {
                UIExploration.transform.GetChild(6).GetChild(0).GetChild(i).GetComponent<Image>().enabled = false; //Basic House background etc..
                UIExploration.transform.GetChild(6).GetChild(0).GetChild(i).GetChild(0).GetComponent<RawImage>().enabled = false; //Basic House etc
                UIExploration.transform.GetChild(6).GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<RawImage>().enabled = false; //Basic house button etc
            }
            UIExploration.transform.GetChild(7).GetComponent<Image>().enabled = false;//ScrollBar
            UIExploration.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;//ScrollBar
            UIExploration.transform.GetChild(8).GetComponent<Image>().enabled = false;//2nd building Panel (Quit button)
            UIExploration.transform.GetChild(8).GetChild(0).GetComponent<Image>().enabled = false;//2nd building Panel (Quit button)
            UIExploration.transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;//2nd building Panel (Quit button)
        }
	}
}
