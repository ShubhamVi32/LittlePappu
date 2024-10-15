using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("player", System.StringComparison.OrdinalIgnoreCase))
        {
            UiController.Instance.GotKey();
            collision.collider.gameObject.GetComponent<PlayerController>().IsGotKey = true;
            this.gameObject.SetActive(false);
        }
    }
}
