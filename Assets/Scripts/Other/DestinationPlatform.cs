using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPlatform : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(other.GetComponent<PlayerController>().DissolvePlayer());
        }
    }
}
