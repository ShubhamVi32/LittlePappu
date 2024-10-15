using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3.9f;
    public Nodes StartingPosition;
    public int ScatterModeTimer1 = 7;
    public int ChaseModeTimer1 = 20;
    public int ScatterModeTimer2 = 7;
    public int ChaseModeTimer2 = 20;
    public int ScatterModeTimer3 = 5;
    public int ChaseModeTimer3 = 20;
    public int ScatterModeTimer4 = 5;
    public int Ghost2relaseTime = 5;
    private float GhostReleaseTimer;
    public bool isGhost2Inside = false;
    private int modechangeinteration = 1;
    private float ModeChangetimer = 0;
    public bool StartMoving;
    public enum Mode
    {
        Chase,
        Scatter,
        Frightened
    }

    public enum GhostType
    {
        Red,
        Pink,
        Blue,
        Orange
    }

    public GhostType ghostType = GhostType.Red;

    Mode currentmode = Mode.Scatter;
    Mode previousmode;
    private GameObject truck;

    [HideInInspector] public Nodes currentnode, targetNode, previoudNode;
    private Vector3 direction, NextDirection;
    [HideInInspector]
    public Vector2 MonsterInitialPos;
    private bool GameStarted = false;
    private AudioSource Soundeffect;
    public AudioClip Monstercollision;

    [SerializeField] private Nodes moveToNode;
    public GameObject ShipRays;
    public Light pointLight;

    void Start()
    {
       

        if (!GameStarted)
        {
            GameStarted = true;
            //MosterSound = this.gameObject.GetComponent<AudioSource>();
            MonsterInitialPos = this.gameObject.transform.position;
            InitialSetup();
            Debug.Log(" started ");
        }

    }


    public void InitialSetup()
    {
        truck = GameObject.FindGameObjectWithTag("Player");
        //GameObject[] GameTrucks = GameObject.FindGameObjectsWithTag("Truck");
        //for (int a = 0; a < GameTrucks.Length; a++)
        //{
        //    if (GameTrucks[a].activeInHierarchy)
        //    {
        //        truck = GameTrucks[a];
        //    }
        //}
        Nodes node = GetNodePosition(transform.localPosition);
        if (node != null)
        {
            currentnode = node;
        }

        if (isGhost2Inside)
        {
            direction = Vector2.down;
            targetNode = currentnode.neighbours[0];
        }
        else
        {
            direction = Vector2.left;
            targetNode = ChooseNextNode();
        }
        previoudNode = currentnode;
        StartMoving = true;
        //Soundeffect = this.gameObject.GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        //if (GameStarted)
        //{
        //    MonsterInitialPos = this.gameObject.transform.position;
        //    InitialSetup();
        //    Debug.Log(" started onenable");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (StartMoving)
        {
            ReleaseGhost();
            //ModeUpdate();
            Move();
        }

        Vector3 scale = ShipRays.transform.localScale;
        if (scale.y < 1f)
        {
            scale.y = scale.y + 0.5f * Time.deltaTime;
            ShipRays.transform.localScale = scale;
        }

    }

    void Move()
    {
        if (targetNode != currentnode && targetNode != null && !isGhost2Inside)
        {
           // Debug.Log(" status " + OvershootTarget());
            if (OvershootTarget())
            {
                currentnode = targetNode;
                transform.localPosition = currentnode.transform.position;
                targetNode = ChooseNextNode();
                previoudNode = currentnode;
                currentnode = null;
            }
            else
            {
                //Debug.Log(" Direction " + direction);
                transform.localPosition += direction * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            Debug.Log(" No new Node");
        }

    }

    void ModeUpdate()
    {
        if (currentmode != Mode.Frightened)
        {
            ModeChangetimer += Time.deltaTime;
            if (modechangeinteration == 1)
            {
                if (currentmode == Mode.Scatter && ModeChangetimer > ScatterModeTimer1)
                {
                    ChangeMode(Mode.Chase);
                    ModeChangetimer = 0;
                }
                if (currentmode == Mode.Chase && ModeChangetimer > ChaseModeTimer1)
                {
                    modechangeinteration = 2;
                    ChangeMode(Mode.Scatter);
                    ModeChangetimer = 0;
                }
            }
            else if (modechangeinteration == 2)
            {
                if (currentmode == Mode.Scatter && ModeChangetimer > ScatterModeTimer2)
                {
                    ChangeMode(Mode.Chase);
                    ModeChangetimer = 0;
                }
                if (currentmode == Mode.Chase && ModeChangetimer > ChaseModeTimer2)
                {
                    modechangeinteration = 3;
                    ChangeMode(Mode.Scatter);
                    ModeChangetimer = 0;
                }
            }
            else if (modechangeinteration == 3)
            {
                if (currentmode == Mode.Scatter && ModeChangetimer > ScatterModeTimer3)
                {
                    ChangeMode(Mode.Chase);
                    ModeChangetimer = 0;
                }
                if (currentmode == Mode.Chase && ModeChangetimer > ChaseModeTimer3)
                {
                    modechangeinteration = 4;
                    ChangeMode(Mode.Scatter);
                    ModeChangetimer = 0;
                }
            }
            else if (modechangeinteration == 4)
            {
                if (currentmode == Mode.Scatter && ModeChangetimer > ScatterModeTimer4)
                {
                    ChangeMode(Mode.Chase);
                    ModeChangetimer = 0;
                }
            }

        }
        else if (currentmode == Mode.Frightened)
        {

        }
    }

    void ChangeMode(Mode m)
    {
        currentmode = m;
    }

    void ReleasePinkGhost()
    {
        if (ghostType == GhostType.Pink && isGhost2Inside)
        {
            isGhost2Inside = false;
        }
    }

    void ReleaseGhost()
    {
        GhostReleaseTimer += Time.deltaTime;
        if (GhostReleaseTimer > Ghost2relaseTime)
        {
            ReleasePinkGhost();
        }
    }

    Vector3 GetRedGhostTargetTile()
    {
        Vector3 Truckpos = truck.transform.localPosition;
        Vector3 targettile = new Vector3(Truckpos.x, Truckpos.y, Truckpos.z);
        return targettile;

    }
    Vector2 GetPinkGhostTargetTile()
    {
        Vector3 Truckpos = truck.transform.localPosition;
        //Vector2 TruckOrientation = truck.GetComponent<TruckPlayer>().orientation;
        //int TruckPosX = Mathf.RoundToInt(TruckOrientation.x);
        ////int TruckPosY = Mathf.RoundToInt(TruckOrientation.y);
       // Vector2 TruckTile = new Vector2(TruckPosX, TruckPosY);
       // Vector2 targetTile = TruckTile + (4 * TruckOrientation);
        return Truckpos;
    }

    Vector3 getTargetTile()
    {
        Vector3 targetTile = Vector3.zero;
        if (ghostType == GhostType.Red)
        {
            targetTile = GetRedGhostTargetTile();
        }
        else if (ghostType == GhostType.Pink)
        {
            targetTile = GetPinkGhostTargetTile();
        }
        return targetTile;
    }



    //GET NODE METHOD
    Nodes ChooseNextNode()
    {
        Vector3 targetTile = Vector3.zero;
        targetTile = getTargetTile();
        moveToNode = null;
        List<Nodes> FoundNodes = new List<Nodes>();
        List<Vector3> FoundNodedirection = new List<Vector3>();
        if (currentnode != null)
        {

        }
        else
        {
            Debug.Log("current node is ");
        }

        for (int a = 0; a < currentnode.neighbours.Length; a++)
        {
            if (currentnode.validDirections[a] != null)
            {
                FoundNodes.Add(currentnode.neighbours[a]);
                FoundNodedirection.Add(currentnode.validDirections[a]);
            }
        }

        if (FoundNodes.Count == 1)
        {
            moveToNode = FoundNodes[0];
            direction = FoundNodedirection[0];
        }

        if (FoundNodes.Count > 1)
        {
            float LeastDistance = 1000f;
            int index = Random.Range(0, FoundNodes.Count);
            moveToNode = FoundNodes[index];
            direction = FoundNodedirection[index];

            //for (int a = 0; a < FoundNodes.Length; a++)
            //{
            //    if (FoundNodedirection[a] != Vector3.zero)
            //    {
            //        //int index = Random.Range(0, FoundNodes.Length);
            //        //moveToNode = FoundNodes[index];
            //        ////direction = FoundNodedirection[index];
            //        //break;
            //        float distance = GetDistance(FoundNodes[a].transform.position, targetTile);
            //        Debug.Log("distance " + distance + " Name " + FoundNodes[a].gameObject.name);
            //        if (distance < LeastDistance)
            //        {
            //            Debug.Log(" Name " + FoundNodes[a].gameObject.name);
            //            //LeastDistance = distance;
            //            moveToNode = FoundNodes[a];
            //            direction = FoundNodedirection[a];
            //        }
            //    }
            //}
        }


        return moveToNode;
    }

    // GET NODE POSITION 
    Nodes GetNodePosition(Vector3 pos)
    {
        GameObject tile = PlatformBoard.Board.board.Where(x => x.Value.Equals(pos)).Select(y => y).FirstOrDefault().Key;
        if (tile != null)
        {
            if (tile.GetComponent<Nodes>() != null)
            {
                return tile.GetComponent<Nodes>();
            }
        }
        return null;
    }


    bool OvershootTarget()
    {
        float nodetoTarget = lengthFromnode(targetNode.transform.position);
        float nodeToself = lengthFromnode(transform.localPosition);
        //Debug.Log(" target1 " + nodetoTarget + " target2 " + nodeToself);
        return nodeToself > nodetoTarget;
    }

    float lengthFromnode(Vector3 targetpos)
    {
        Vector3 vec = targetpos - previoudNode.transform.position;
        return vec.sqrMagnitude;
    }

    float GetDistance(Vector3 PosA, Vector3 PosB)
    {
        float dx = PosA.x - PosB.x;
        float dy = PosA.y - PosB.y;
        float dz = PosA.z - PosB.z;
        float distnace = Mathf.Sqrt(dx * dx + dy * dy  + dz * dz);
       // Debug.Log(" distance " + distnace);
        return distnace;

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Truck")
        {
            
        }

    }

}
