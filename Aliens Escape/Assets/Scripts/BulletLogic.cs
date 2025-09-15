using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
           
            Destroy(gameObject); // Destroy the bullet
           
        }
    }
}
