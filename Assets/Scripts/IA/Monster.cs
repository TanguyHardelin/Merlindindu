using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int amtGold;
    public float attDistance;
    public float correcSpeed = 0.6f;
    public bool hasAttacked;

    private bool isAttacking = false;
    private bool isFleeing = false;
    private bool isAttacked = false;
    private bool isDead = false;
    private Vector3 direction;
    private float angle;
    private int timeNewDir = 0;
    private Vector3 moveDir;
    private int deathCnt = 0;
    private bool isKnight = false;
    private Canvas UIMonster;


    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        typeMonster = this.tag;
        UIMonster = gameObject.GetComponentInChildren<Canvas>();
        UpdateLifeBar();

        moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        switch (typeMonster)
        {
            case "Rabbit1":
                damageAmount = 4;
                health = 20;
                healthMax = 20;
                isAggressive = false;
                isACoward = true;
                moveRate = 3f * correcSpeed;
                amtGold = 10;
                attDistance = 1f;
                break;
            case "Rabbit2":
                damageAmount = 8;
                health = 50;
                healthMax = 50;
                isAggressive = false;
                isACoward = true;
                moveRate = 3f * correcSpeed;
                amtGold = 16;
                attDistance = 0.9f;
                break;
            case "Rabbit3":
                damageAmount = 12;
                health = 110;
                healthMax = 110;
                isAggressive = false;
                isACoward = true;
                moveRate = 4f * correcSpeed;
                amtGold = 24;
                attDistance = 1f;
                break;
            case "Bat1":
                damageAmount = 6;
                health = 25;
                healthMax = 25;
                isAggressive = true;
                isACoward = false;
                moveRate = 4f * correcSpeed;
                amtGold = 12;
                attDistance = 1f;
                break;
            case "Bat2":
                damageAmount = 12;
                health = 60;
                healthMax = 60;
                isAggressive = true;
                isACoward = false;
                moveRate = 4f * correcSpeed;
                amtGold = 18;
                attDistance = 1f;
                break;
            case "Bat3":
                damageAmount = 16;
                health = 120;
                healthMax = 120;
                isAggressive = true;
                isACoward = false;
                moveRate = 5f * correcSpeed;
                amtGold = 25;
                attDistance = 1f;
                break;
            case "Blob1":
                damageAmount = 2;
                health = 40;
                healthMax = 40;
                isAggressive = false;
                isACoward = true;
                moveRate = 2f * correcSpeed;
                amtGold = 11;
                attDistance = 0.9f;
                break;
            case "Blob2":
                damageAmount = 4;
                health = 85;
                healthMax = 85;
                isAggressive = false;
                isACoward = false;
                moveRate = 2f * correcSpeed;
                amtGold = 17;
                attDistance = 1f;
                break;
            case "Blob3":
                damageAmount = 8;
                health = 180;
                healthMax = 180;
                isAggressive = true;
                isACoward = false;
                moveRate = 3f * correcSpeed;
                amtGold = 26;
                attDistance = 1.1f;
                break;
            case "Ghost1":
                damageAmount = 6;
                health = 35;
                healthMax = 35;
                isAggressive = false;
                isACoward = true;
                moveRate = 2f * correcSpeed;
                amtGold = 12;
                attDistance = 1f;
                break;
            case "Ghost2":
                damageAmount = 9;
                health = 80;
                healthMax = 80;
                isAggressive = true;
                isACoward = true;
                moveRate = 2f * correcSpeed;
                amtGold = 20;
                attDistance = 1f;
                break;
            case "Ghost3":
                damageAmount = 13;
                health = 170;
                healthMax = 170;
                isAggressive = true;
                isACoward = false;
                moveRate = 3f * correcSpeed;
                amtGold = 28;
                attDistance = 1f;
                break;
            case "Mushroom1":
                damageAmount = 8;
                health = 20;
                healthMax = 20;
                isAggressive = true;
                isACoward = true;
                moveRate = 1f * correcSpeed;
                amtGold = 10;
                attDistance = 1f;
                break;
            case "Mushroom2":
                damageAmount = 15;
                health = 45;
                healthMax = 45;
                isAggressive = true;
                isACoward = true;
                moveRate = 1f * correcSpeed;
                amtGold = 16;
                attDistance = 1f;
                break;
            case "Mushroom3":
                damageAmount = 26;
                health = 90;
                healthMax = 90;
                isAggressive = true;
                isACoward = false;
                moveRate = 2f * correcSpeed;
                amtGold = 24;
                attDistance = 1f;
                break;
            case "Skeleton1":
                damageAmount = 5;
                health = 50;
                healthMax = 50;
                isAggressive = true;
                isACoward = false;
                moveRate = 4f * correcSpeed;
                amtGold = 15;
                attDistance = 0.8f;
                break;
            case "Skeleton2":
                damageAmount = 7;
                health = 90;
                healthMax = 90;
                isAggressive = true;
                isACoward = false;
                moveRate = 5f * correcSpeed; ;
                amtGold = 122;
                attDistance = 1f;
                break;
            case "Skeleton3":
                damageAmount = 10;
                health = 150;
                healthMax = 150;
                isAggressive = true;
                isACoward = false;
                moveRate = 5f * correcSpeed;
                amtGold = 35;
                attDistance = 1f;
                break;
            case "Knight":
                damageAmount = 15;
                health = 150;
                healthMax = 150;
                isAggressive = false;
                isACoward = false;
                moveRate = 5f * correcSpeed;
                amtGold = 50;
                isKnight = true;
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

        if (direction.magnitude > attDistance)
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
        UpdateLifeBar();

        updateStatus();
        
        ifDead();

        if (GameObject.Find("UIExploration").GetComponent<UIManager>().getFreeze()) {
            anim.SetBool("isIdle", true);
        }
        else {
            updateActions();
        }
        

        // On le détruit si plus dans le champs de vision
        if (Vector3.Distance(player.transform.position, this.transform.position) > 40) Destroy(this.gameObject);
    }

    void updateStatus() {

        // Si le player est en range
        if (Vector3.Distance(player.transform.position, this.transform.position) < 11 && Mathf.Abs(angle) < 140) {
            

            // Si le monstre est agressif, il attaque
            if ((isAggressive && !isDead) || (isAttacked && !isDead)) {
                isAttacking = true;
                player.setCountRegen(-250);
            }

            // S'il est coward ou low life, il fuit
            if (health <= 0.2 * healthMax && isACoward && !isDead) {
                isAttacking = false;
                isFleeing = true;
            }
        }

        // Si le player n'est pas en range il n'attaque plus / n'est plus attaqué
        if (Vector3.Distance(player.transform.position, this.transform.position) > 15)
        {
            isAttacking = false;
            isAttacked = false;
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
        else if (!isKnight) RandomMove();
    }
    void UpdateLifeBar()
    {

        float ratio2 = health / healthMax;
        UIMonster.transform.GetChild(0).GetChild(0).GetComponent<Image>().rectTransform.localScale = new Vector3(ratio2, 1, 1);
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
        if (hlth <= 0) {
            isDead = true;
            health = 0;
        }
        else if (hlth >= healthMax) health = healthMax;
        else health = hlth;
    }
    public void setLevel(float lvl)
    {
        level = lvl;
    }
    public void setIsAttacked(bool b) { isAttacked = b; }


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
