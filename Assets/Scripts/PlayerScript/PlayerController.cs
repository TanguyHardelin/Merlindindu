using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    public float moveSpeed = 8f;
    public float walkSpeed = 4f;
    public float speed = 8f;
    public float rotateSpeed = 30f;
    public int ATK = 10;
    public int DEF = 10;
    public int health = 100;
    public int maxHealth = 100;
    public bool hasAttacked = false;
    
    
    protected EnvironnementGenerator _environnmentGenerator;
    protected bool _environnementGeneratoInitialised = false;


    private Animator animator;
    private Vector3 villageCenter;
    private int countRegen = 0;

    void Start()
    {
        //villageCenter = GameObject.FindGameObjectWithTag("village").transform.position;
        
        animator = gameObject.GetComponent<Animator>();

        //Nécéssaire au raffraichissement des chunks:
        _environnmentGenerator = FindObjectOfType<EnvironnementGenerator>();
        gameObject.GetComponent<AudioSource>().volume = 0;
       
    }

    void Update()
    {   
        if (!GameObject.Find("UIExploration").GetComponent<UIManager>().getFreeze()) {
            countRegen++;
            if (countRegen >= 200)
            {
                setHealth(health + 1);
                countRegen = 0;
            }
            if (_environnementGeneratoInitialised==false)
            {
                _environnementGeneratoInitialised = true;
            }
            else
            {
                //Nécéssaire au raffraichissement des chunks:
                int x = _environnmentGenerator.getIndexFromCoordinate(this.transform.position.x);
                int z = _environnmentGenerator.getIndexFromCoordinate(this.transform.position.z);

                _environnmentGenerator.GenerateAroundPlayer(x, z);
            }

            if (mainCamera.enabled == true) {
                if (Input.GetKeyDown("space")) {
                    animator.SetTrigger("isJumping");
                }

                if (Input.GetMouseButtonDown(0)) {
                    animator.SetBool("isAttacking", true);
                }

                if (health <= 0) { 
                    animator.SetBool("isDead", true);

                    countRegen = -300;
                    StartCoroutine(Death());
                }
            }
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        GameObject respawnPoint = GameObject.FindGameObjectWithTag("respawn");
        gameObject.transform.position = respawnPoint.transform.position;
        animator.SetBool("isDead", false);
        setHealth(maxHealth / 2);
        GameObject.FindObjectOfType<Player>().getRessources().gold = GameObject.FindObjectOfType<Player>().getMaxGold()/4;

    }

    void FixedUpdate()
    {   
        if (!GameObject.Find("UIExploration").GetComponent<UIManager>().getFreeze()) {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            if (mainCamera.enabled == true) {
                if (moveHorizontal != 0 || moveVertical != 0) {
                    animator.SetBool("isMoving", true);
                    movePlayer(moveHorizontal, moveVertical);
                }
                else {
                    animator.SetBool("isMoving", false);
                }
            }
        }
    }

    void movePlayer(float moveHorizontal, float moveVertical) {
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward.normalized, Vector3.up);
        Vector3 forwardOrthoPLanXZ = Vector3.Cross(Vector3.up, forward);
        Vector3 direction = Vector3.zero;

        
        direction = forward.normalized * moveVertical + forwardOrthoPLanXZ.normalized * moveHorizontal;
        
        Vector3 targetPosition = transform.position + direction;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void setATK(int atk)
    {
        if (atk >= 0) ATK = atk;
    }
    public void setDEF(int def)
    {
        if (def >= 0) DEF = def;
    }
    public void setHealth(int hlt)
    {
        if (hlt >= maxHealth) health = maxHealth;
        else if (hlt <= 0) health = 0;
        else health = hlt;
    }
    public void setMaxHealth(int hlt)
    {
        maxHealth = hlt;
    }
    public void setCountRegen(int count)
    {
        countRegen = count;
    }

    public int getATK()
    {
        return ATK;
    }
    public int getDEF()
    {
        return DEF;
    }
    public int getHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
}

