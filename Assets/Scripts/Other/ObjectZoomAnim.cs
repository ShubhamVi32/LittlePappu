using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectZoomAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        iTween.ScaleTo(this.gameObject, iTween.Hash("scale", Vector3.one, "time", 3f, "easeType", iTween.EaseType.linear));
    }
}
