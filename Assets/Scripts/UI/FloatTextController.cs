using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatTextController : MonoBehaviour {

    private Text myText;

    [SerializeField]
    private float moveAmt;
    [SerializeField]
    private float moveSpeed;

    private Vector3 moveDir;

    private bool canMove = false;

    private void Start()
    {
        moveDir = transform.up;

    }

    private void Update()
    {
        if (canMove) transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, moveAmt * moveSpeed * Time.deltaTime);
    }

    public void SetTextandMove(string text, Color txtColor)
    {
        myText = GetComponentInChildren<Text>();
        myText.color = txtColor;
        myText.text = text;
        canMove = true;
    }


}
