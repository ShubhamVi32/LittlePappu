using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorAnalysis : MonoBehaviour
{
    public Vector3 Pos;
    public float speed;
    public Vector3 initialPos;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        targetPos = Pos;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.Equals(targetPos))
        {
            targetPos = initialPos;
            initialPos = transform.position;
        }

        transform.RotateAround(Vector3.zero,targetPos,speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
