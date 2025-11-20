using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Needed for scene loading

public class HealthManager : MonoBehaviour
{
    public GameObject player;
    private GlobalVariables globalVariables;
    public Image healthBar;
    public float healthAmount;

    // Optional: prevent taking damage every frame
    public float damageCooldown = 0.5f;
    private float lastDamageTime;
    

    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
        healthAmount = globalVariables.playerHealth; // Initialize healthAmount from GlobalVariables
        if (healthBar != null)
            healthBar.fillAmount = healthAmount / globalVariables.playerHealth;
    }

   public void TakeDamage(float damage)
{
    healthAmount -= damage;
    healthAmount = Mathf.Clamp(healthAmount, 0, globalVariables.playerHealth);

    if (healthBar != null)
        healthBar.fillAmount = healthAmount / globalVariables.playerHealth;

    

    if (healthAmount <= 0)
    {
        globalVariables.currentHealth = healthAmount;
        Die();
        
    }
}

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, globalVariables.playerHealth);

        if (healthBar != null)
            healthBar.fillAmount = healthAmount / globalVariables.playerHealth;
    }

    // Detect collision with enemy attacks
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack") && Time.time > lastDamageTime + damageCooldown)
        {
            TakeDamage(20f); // Adjust damage as needed
            lastDamageTime = Time.time;
        }
    }

   private void Die()
{
    

    if (player != null)
    {
        // Get the CharacterController component
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.enabled = false; // Disable the controller to move the transform
            player.transform.position = new Vector3(24.8f, 7.6f, -101.65f);
            cc.enabled = true;  // Re-enable controller
        }
        else
        {
            player.transform.position = new Vector3(24.8f, 7.6f, -101.65f);
        }
    }

    // Reset health and stats
    
    Heal(100f);

    globalVariables.playerMaxAmmo += 10;
    globalVariables.TimesDied += 1;
}


    private void Update()
    {
        globalVariables.currentHealth = healthAmount;
        if (healthAmount <= 0)
    {
        globalVariables.currentHealth = healthAmount;
        Die();
        
    }
    }
  
}
