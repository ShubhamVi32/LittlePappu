using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBoard : MonoBehaviour
{
    public static PlatformBoard Board;
    private static int Boardwidth = 100;
    private static int BoardHeight = 100;
    public Dictionary<GameObject, Vector3> board = new Dictionary<GameObject, Vector3>();
    public Transform NodesParent;

    private void Awake()
    {
        if (Board == null)
            Board = this;
    
        if (NodesParent != null)
        {
            foreach(Transform t in NodesParent)
            {
                board.Add(t.gameObject, t.position);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
