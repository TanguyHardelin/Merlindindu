﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public int damageAmount;
    public Animator anim;
    public float healthMax;
    public float health;
    public float moveRate;
    public bool isAggressive;
    public bool isACoward;
    public PlayerController player;
    public float level = 1;
    public string typeMonster;
    public int timeBtwAtk = 50;
    public int amtGold;

    private bool isAttacking = false;
    private bool isFleeing = false;
    private bool isAttacked = false;
    private bool isDead = false;
    private Vector3 direction;
    private float angle;
    private int timeNewDir = 0;
    private int timeAtk = 0;
    private Vector3 moveDir;
    public bool hasAttacked;
    private int deathCnt = 0;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        typeMonster = this.tag;

        moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        switch (typeMonster)
        {
            case "Rabbit1":
                damageAmount = 4;
                health = 20;
                healthMax = 20;
                isAggressive = false;
                isACoward = true;
                moveRate = 3f;
                amtGold = 10;
                break;
            case "Rabbit2":
                damageAmount = 8;
                health = 50;
                healthMax = 50;
                isAggressive = false;
                isACoward = true;
                moveRate = 3f;
                amtGold = 16;
                break;
            case "Rabbit3":
                damageAmount = 12;
                health = 110;
                healthMax = 110;
                isAggressive = false;
                isACoward = true;
                moveRate = 4f;
                amtGold = 24;
                break;
            case "Bat1":
                damageAmount = 6;
                health = 25;
                healthMax = 25;
                isAggressive = true;
                isACoward = false;
                moveRate = 4f;
                amtGold = 12;
                break;
            case "Bat2":
                damageAmount = 12;
                health = 60;
                healthMax = 60;
                isAggressive = true;
                isACoward = false;
                moveRate = 4f;
                amtGold = 18;
                break;
            case "Bat3":
                damageAmount = 16;
                health = 120;
                healthMax = 120;
                isAggressive = true;
                isACoward = false;
                moveRate = 5f;
                amtGold = 25;
                break;
            case "Blob1":
                damageAmount = 2;
                health = 40;
                healthMax = 40;
                isAggressive = false;
                isACoward = true;
                moveRate = 2f;
                amtGold = 11;
                break;
            case "Blob2":
                damageAmount = 4;
                health = 85;
                healthMax = 85;
                isAggressive = false;
                isACoward = false;
                moveRate = 2f;
                amtGold = 17;
                break;
            case "Blob3":
                damageAmount = 8;
                health = 180;
                healthMax = 180;
                isAggressive = true;
                isACoward = false;
                moveRate = 3f;
                amtGold = 26;
                break;
            case "Ghost1":
                damageAmount = 6;
                health = 35;
                healthMax = 35;
                isAggressive = false;
                isACoward = true;
                moveRate = 2f;
                amtGold = 12;
                break;
            case "Ghost2":
                damageAmount = 9;
                health = 80;
                healthMax = 80;
                isAggressive = true;
                isACoward = true;
                moveRate = 2f;
                amtGold = 20;
                break;
            case "Ghost3":
                damageAmount = 13;
                health = 170;
                healthMax = 170;
                isAggressive = true;
                isACoward = false;
                moveRate = 3f;
                amtGold = 28;
                break;
            case "Mushroom1":
                damageAmount = 8;
                health = 20;
                healthMax = 20;
                isAggressive = true;
                isACoward = true;
                moveRate = 1f;
                amtGold = 10;
                break;
            case "Mushroom2":
                damageAmount = 15;
                health = 45;
                healthMax = 45;
                isAggressive = true;
                isACoward = true;
                moveRate = 1f;
                amtGold = 16;
                break;
            case "Mushroom3":
                damageAmount = 26;
                health = 90;
                healthMax = 90;
                isAggressive = true;
                isACoward = false;
                amtGold = 24;
                break;
            case "Skeleton1":
                damageAmount = 5;
                health = 50;
                healthMax = 50;
                isAggressive = true;
                isACoward = false;
                moveRate = 4f;
                amtGold = 15;
                break;
            case "Skeleton2":
                damageAmount = 7;
                health = 90;
                healthMax = 90;
                isAggressive = true;
                isACoward = false;
                moveRate = 5f;
                amtGold = 122;
                break;
            case "Skeleton3":
                damageAmount = 10;
                health = 150;
                healthMax = 150;
                isAggressive = true;
                isACoward = false;
                moveRate = 5f;
                amtGold = 35;
                break;
        }
    }

    public void RandomMove()
    {
        timeNewDir++;
        //if the changeTime was reached, calculate a new movement vector
        if (timeNewDir > 100)
        {
            moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            timeNewDir = 0;
        }

        //move enemy: 
        anim.SetBool("isWalking", true);

        moveDir.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
        if (!isDead) this.transform.Translate(0, 0, 0.02f * moveRate);

    }

    public void Attack()
    {
        direction.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), moveRate);

        if (direction.magnitude > 1) // 1 => monstre à distance suffisante pour effectuer une attaque de mêlée
        {   
            anim.SetBool("isWalking", true);
            this.transform.Translate(0, 0, 0.02f * moveRate); 
        }
        else
        {
            anim.SetBool("isAttacking", true);
        }
    }

    public void Flee()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 15)
        {
            anim.SetBool("isWalking", true);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(-direction), 0.1f);
            if (!isDead) this.transform.Translate(0, 0, 0.02f * moveRate);
        }
    }

    public void Update()
    {
        direction = player.transform.position - this.transform.position;
        angle = Vector3.Angle(direction, this.transform.forward);

        updateStatus();
        
        ifDead();

        updateActions();

        // On le détruit si plus dans le champs de vision
        if (Vector3.Distance(player.transform.position, this.transform.position) > 40) Destroy(this.gameObject);
    }

    void updateStatus() {

        // Si le player est en range
        if (Vector3.Distance(player.transform.position, this.transform.position) < 11 && Mathf.Abs(angle) < 140) {
            
            // Si le monstre est agressif, il attaque
            if (isAggressive && !isDead) {
                isAttacking = true;
            }

            // S'il est coward et low life, il fuit
            if (health <= 0.2 * healthMax && isACoward && !isDead) {
                isAttacking = false;
                isFleeing = true;
            }
        }

        // Si le player n'est pas en range il n'attaque plus / n'est plus attaqué
        if (Vector3.Distance(player.transform.position, this.transform.position) > 15)
        {
            isAttacking = false;
            isFleeing = false;
        }
    }

    void ifDead() {
        if (isDead || health == 0)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            isAttacking = false;
            isFleeing = false;
            if (deathCnt == 1) {
                player.GetComponent<Player>().getRessources().gold += amtGold;
            }
            Destroy(this.gameObject,5);
            deathCnt++;
        }
    }

    void updateActions() {
        if (isAttacking) Attack();
        else if (isFleeing) Flee();
        else RandomMove();
    }

     /*************/
    /** Setters **/
   /*************/
    public void setAggressive(bool isaggressive)
    {
        isAggressive = isaggressive;
    }
    public void setCoward(bool coward)
    {
        isACoward = coward;
    }
    public void setDmgAmount(int dmg)
    {
        damageAmount = dmg;
    }
    public void setMoveRate(float moveRt)
    {
        moveRate = moveRt;
    }
    public void setHealth(float hlth)
    {
        if (hlth < 0) isDead = true;
        else if (hlth <= healthMax) health = hlth;
    }
    public void setLevel(float lvl)
    {
        level = lvl;
    }
    public void setIsAttacked(bool isatk)
    {
        isAttacked = isatk;
    }


      /*************/
     /** Getters **/
    /*************/
    public bool getAggressive()
    {
        return isAggressive;
    }
    public bool getCoward()
    {
        return isACoward;
    }
    public float getDmgAmount()
    {
        return damageAmount;
    }
    public float getMoveRate()
    {
        return moveRate;
    }
    public float getHealth()
    {
        return health;
    }
}