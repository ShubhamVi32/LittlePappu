using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody body;
    private Vector3 moveDirection;
    private Camera cam;
    public float moveSpeed = 5f;
    public Joystick movementStick;

    [SerializeField] private Material SolidObj, ForceField;
    public bool IsSolid = true;
    [HideInInspector] public bool AllowMovement = true, isCaught = false;
    [SerializeField] private GameObject DeathEffect;

    [HideInInspector] public bool IsGotKey;
    private InvisiblePower power;

    private Material mat;

    public Shader shader;
    [SerializeField] private DissolveObject dissolveObj;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
 
        body = this.GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        dissolveObj = GetComponent<DissolveObject>();

        cam = Camera.main;
        power = new InvisiblePower(20);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(mat != null)
        {
            Debug.Log(mat.name);
        }
       
    }

    // Update is called once per frame
    void Update()
    {

        if (AllowMovement)
        {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), -0.5f, Input.GetAxisRaw("Vertical")).normalized;
            //moveDirection = new Vector3(movementStick.Direction.x, -0.5f, movementStick.Direction.y);
#elif UNITY_ANDROID
            moveDirection = new Vector3(movementStick.Direction.x, 0f, movementStick.Direction.y);
#endif

            body.velocity = (moveDirection * moveSpeed);

            ChangeAvatar();

            if (!IsSolid)
            {
                var timeLeft = power.MakeInivisble();
                if(timeLeft <= 10.0f)
                {
                    MakeVisiblePlayer();
                }
            }
        }


    }


    void ChangeAvatar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<MeshRenderer>().material = IsSolid ? ForceField : SolidObj;
            IsSolid = !IsSolid;
        }
    }

    void MakeVisiblePlayer()
    {
        this.GetComponent<MeshRenderer>().material = IsSolid ? ForceField : SolidObj;
        IsSolid = !IsSolid;
    }

    public void CaughtByShip(Transform Ship)
    {
        StartCoroutine(DestoryPlayer(Ship));
     
    }

    IEnumerator DestoryPlayer(Transform ship)
    {
        this.transform.position = new Vector3(ship.position.x, this.transform.position.y, ship.position.z);
        this.GetComponent<Rigidbody>().isKinematic = true;
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        iTween.MoveTo(this.gameObject, iTween.Hash("position", targetPos, "easetype", iTween.EaseType.linear, "time", 2f));
        yield return new WaitForSeconds(2f);
        Instantiate(DeathEffect, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public IEnumerator DissolvePlayer()
    {
        yield return new WaitForSeconds(1f);
        dissolveObj.isDissolve = !dissolveObj.isDissolve;
    }
}
