using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public int damageTaken;
    private GlobalVariables globalVariables;

    private bool canBeDamaged = true;

    void Awake()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();


    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            health -= damageTaken;
            Destroy(other.gameObject); // Destroy the bullet
          
        }
    }


    void Update()
    {
        if (health <= 0)
        {

            globalVariables.enemyKills += 1;
          
            Destroy(gameObject);


            }
    }
}