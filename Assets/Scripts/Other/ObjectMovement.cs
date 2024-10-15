using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Vector3 TargetPos;
    [SerializeField] private float Movetime;
    [SerializeField] private iTween.EaseType easeType;
    [SerializeField] private iTween.LoopType loop;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(MoveObject());
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        iTween.MoveFrom(this.gameObject, iTween.Hash("position", TargetPos, "easeType", easeType, "loopType", loop, "time", Movetime));
    }

    IEnumerator MoveObject()
    {
        iTween.MoveFrom(this.gameObject, iTween.Hash("position", TargetPos,"easeType",easeType,"loop",loop,"time",Movetime));
        yield return new WaitForSeconds(1f);
    }
}
