using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollect : MonoBehaviour
{
    public GameObject collectEffect;
   

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("player", System.StringComparison.OrdinalIgnoreCase))
        {
            var effect = Instantiate(collectEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
