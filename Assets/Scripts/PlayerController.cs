using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 8f;
    public float walkSpeed = 4f;
    public float speed = 8f;
    public float rotateSpeed = 30f;
    public Camera mainCamera;
    public Camera manageCamera;
    public UIManager uiManager;
    public VillageManager villageManager;
    public DisplaySystem dispSys;
    protected EnvironnementGenerator _environnmentGenerator;
    protected bool _environnementGeneratoInitialised = false;


    private bool canCollect = false;
    private bool manageMode = false;
    private Animator animator;
    private Vector3 villageCenter;

    private int maxGold = 1000;
    private int maxWood = 800;
    private int maxStone = 500;
    public int gold = 150;
    public int wood = 50;
    public int stone = 20;
    public int ATK = 10;
    public int DEF = 10;


    void Start()
    {
        //villageCenter = GameObject.FindGameObjectWithTag("village").transform.position;
        mainCamera.enabled = true;
        animator = gameObject.GetComponent<Animator>();

        //Nécéssaire au raffraichissement des chunks:
        _environnmentGenerator = FindObjectOfType<EnvironnementGenerator>();
        
        dispSys.UIManage(false);
        Debug.Log(walkSpeed);
    }

    void Update()
    {
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
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0) {
            animator.SetBool("isMoving", true);
            movePlayer(moveHorizontal, moveVertical);
        }
        else {
            animator.SetBool("isMoving", false);
        }
    }

    void movePlayer(float moveHorizontal, float moveVertical) {
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward.normalized, Vector3.up);
        Vector3 forwardOrthoPLanXZ = Vector3.Cross(Vector3.up, forward);
        Vector3 direction = Vector3.zero;

        if (manageMode) {
            direction = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        }
        else {
            direction = forward.normalized * moveVertical + forwardOrthoPLanXZ.normalized * moveHorizontal;
        }
        Vector3 targetPosition = transform.position + direction;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone")
        {
            Collectable script = (Collectable)other.GetComponent(typeof(Collectable));
            if (script.getIsEmpty()) dispSys.OpenMessagePanel("-No more ressources-");
            else dispSys.OpenMessagePanel("-Press P to gather-");
            canCollect = true;
        }
        else if (other.tag == "village" && !manageMode)
        {
            dispSys.OpenMessagePanel("-Press M to manage-");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.P) && (other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone"))
        {
            Collectable script = (Collectable)other.GetComponent(typeof(Collectable));
            script.PickRessources();
            if (script.getIsEmpty()) dispSys.OpenMessagePanel("-No more ressources-");
            else
            {
                dispSys.CloseMessagePanel();
                dispSys.OpenMessagePanel("-Press P to gather-");
            }
        }
        else if (Input.GetKeyDown(KeyCode.M) && (other.tag == "village"))
        {
            dispSys.CloseMessagePanel();
            OnManageMode(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collect_gold" || other.tag == "Collect_wood" || other.tag == "Collect_stone")
        {
            dispSys.CloseMessagePanel();
            canCollect = false;
        }
        else if (other.tag == "village")
        {
            dispSys.CloseMessagePanel();
        }
    }

    public void OnManageMode(bool mngMode)
    {
        if (mngMode)
        {
            Debug.Log("ManageMode");
            dispSys.CloseMessagePanel();

            mainCamera.enabled = false;
            manageCamera.enabled = true;

            dispSys.UIManage(true);
            manageMode = true;
            uiManager.setFreeze(true);

            villageManager.setStone(villageManager.getStone() + stone);
            villageManager.setWood(villageManager.getWood() + wood);
            setStone(0);
            setWood(0);
        }
        else
        {
            Debug.Log("ManageModeExit");
            mainCamera.enabled = true;
            manageCamera.enabled = false;

            dispSys.UIManage(false);
            manageMode = false;
            uiManager.setFreeze(false);
        }
    }

    //---------------//
    //    SETTERS    //
    //---------------//
    public void setMaxGold(int gld)
    {
        if (gld >= 1000) maxGold = gld;
    }
    public void setMaxWood(int wd)
    {
        if (wd >= 800) maxWood = wd;
    }
    public void setMaxStone(int st)
    {
        if (st >= 500) maxStone = st;
    }
    public void setGold(int gld)
    {
        if (gld >= 0 && gld <= maxGold) gold = gld;
    }
    public void setWood(int wd)
    {
        if (wd >= 0 && wd <= maxWood) wood = wd;
    }
    public void setStone(int st)
    {
        if (st >= 0 && st <= maxStone) stone = st;
    }
    public void setATK(int atk)
    {
        if (atk >= 0) ATK = atk;
    }
    public void setDEF(int def)
    {
        if (def >= 0) DEF = def;
    }


    //---------------//
    //    GETTERS    //
    //---------------//
    public int getMaxGold()
    {
        return maxGold;
    }
    public int getMaxWood()
    {
        return maxWood;
    }
    public int getMaxStone()
    {
        return maxStone;
    }
    public int getGold()
    {
        return gold;
    }
    public int getWood()
    {
        return wood;
    }
    public int getStone()
    {
        return stone;
    }
    public bool getManageMode()
    {
        return manageMode;
    }
    public int getATK()
    {
        return ATK;
    }
    public int getDEF()
    {
        return DEF;
    }

}
