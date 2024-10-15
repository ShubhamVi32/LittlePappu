using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("player", System.StringComparison.OrdinalIgnoreCase))
        {
            if (collision.gameObject.GetComponent<PlayerController>().IsSolid)
            {
                this.gameObject.GetComponentInParent<EnemyController>().StartMoving = false;
                collision.gameObject.GetComponent<PlayerController>().AllowMovement = false;
                collision.gameObject.GetComponent<PlayerController>().CaughtByShip(this.transform);
            }
        }
    }

  
}
