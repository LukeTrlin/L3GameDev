using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("References")]

    public MusicController musicController;

    public Transform player;
    public NavMeshAgent agent;

    [Header("Stats")]
    public float moveSpeed = 1.75f;
    public float attackRange = 2f;
    public float maxHealth;
    private float currentHealth;

    private Animator animator;
    private GlobalVariables globalVariables;

    private bool hasDied = false;
    void Start()
    {
        globalVariables = FindObjectOfType<GlobalVariables>();
        animator = GetComponent<Animator>();
        if (!agent) agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        maxHealth = 50 * globalVariables.difficultyLevel;
        currentHealth = maxHealth;
        moveSpeed = 3.5f * globalVariables.difficultyLevel;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange & distance < 100f )
        {
            // Move toward player
            agent.isStopped = false;
            agent.SetDestination(player.position);

            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
            // ensure NavMeshAgent uses current moveSpeed (in case moveSpeed was changed after Start)
            if (agent != null) agent.speed = moveSpeed;
        }
        else
        {
            // Stop moving
            agent.isStopped = true;

            // Loop attack animation
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            TakeDamage(20); // change damage amount as needed
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
             musicController.PlayHitEnemySound();
            // Play hit animation
            animator.SetTrigger("Hit");
           
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        if (!hasDied)
        {
             musicController.PlayMonsterDeathSound();

            globalVariables.enemyKills += 1;
       
        animator.SetTrigger("Die"); // optional death animation
        agent.isStopped = true;
        Destroy(gameObject, 3f); // destroy zombie after delay
        hasDied = true;
        }
        
    }
}