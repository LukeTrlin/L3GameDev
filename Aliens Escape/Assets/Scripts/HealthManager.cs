using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private GlobalVariables globalVariables;
    public Image healthBar;
    public float healthAmount;
    // Start is called before the first frame update
    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
        healthAmount = globalVariables.playerHealth; // Initialize healthAmount from GlobalVariables
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / globalVariables.playerHealth;

    }
    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, globalVariables.playerHealth);

        healthBar.fillAmount = healthAmount / globalVariables.playerHealth;
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10f);
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10f);
        }


    }
}
