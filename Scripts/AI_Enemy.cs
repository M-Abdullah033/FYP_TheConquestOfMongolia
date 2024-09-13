//using UnityEngine;
//using UnityEngine.AI;

//public class Enemy_NPC : MonoBehaviour
//{
//    public Transform playerTransform; // Reference to the player's transform
//    public float followDistance = 2f; // Distance at which the NPC will start following the player
//    public float runDistance = 5f; // Distance at which the NPC will start running towards the player
//    public float stopDistance = 1.5f; // Distance at which the NPC will stop moving towards the player
//    public float runSpeed = 5f; // Speed at which the NPC runs
//    public float attackDistance = 1f; // Distance at which the NPC will start attacking the player
//    public float counterInterval = 5f; // Interval between counter attacks

//    private NavMeshAgent agent; // Reference to the NPC's NavMeshAgent
//    private Animator animator; // Reference to the NPC's Animator component
//    private bool isFollowing = false; // Is the NPC currently following the player?
//    private bool isRun = false; // Is the NPC running?
//    private float counterTimer = 0f; // Timer for counter attacks
//    private int hitCount = 0; // Counter for tracking hits received

//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>(); // Getting the NavMeshAgent component attached to the NPC
//        animator = GetComponent<Animator>(); // Getting the Animator component attached to the NPC

//        if (playerTransform == null)
//        {
//            Debug.LogError("Player Transform is not assigned. Please assign the player transform in the Inspector.");
//        }
//    }

//    void Update()
//    {
//        if (playerTransform == null || animator == null)
//            return;

//        // Calculate the distance between the NPC and the player
//        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

//        // Check if NPC should follow the player
//        if (distanceToPlayer <= followDistance)
//        {
//            // Set destination to the player's position
//            agent.SetDestination(playerTransform.position);

//            // Determine whether NPC should run based on distance to player
//            isRun = distanceToPlayer > stopDistance && distanceToPlayer <= runDistance;

//            // Update animator parameter based on NPC's movement state
//            animator.SetBool("isRun", isRun);

//            // Set NPC's speed based on whether it's running
//            agent.speed = isRun ? runSpeed : agent.speed;

//            // Check if NPC should attack the player
//            if (distanceToPlayer <= attackDistance)
//            {
//                // Trigger attack animation
//                animator.SetTrigger("Attack");

//                // Stop moving when attacking
//                agent.velocity = Vector3.zero;
//            }
//            else
//            {
//                // NPC is actively following the player
//                isFollowing = true;
//            }

//            // Update the counter attack timer
//            counterTimer -= Time.deltaTime;
//            if (counterTimer <= 0)
//            {
//                // Trigger counter attack animation
//                animator.SetTrigger("Counter");
//                // Reset counter timer
//                counterTimer = counterInterval;
//            }
//        }
//        else
//        {
//            if (isFollowing)
//            {
//                // Stop moving if player is out of follow distance
//                agent.ResetPath();

//                // Update animator parameter to idle state
//                animator.SetBool("isRun", false);

//                // NPC is no longer following the player
//                isFollowing = false;
//            }
//        }

//        // If NPC is idle and not too close to the player, stop NPC's animation
//        if (!isRun && distanceToPlayer > followDistance)
//        {
//            agent.velocity = Vector3.zero;
//        }
//    }

//    // Function to handle when the NPC gets hit
//    public void TakeHit()
//    {
//        hitCount++; // Increment the hit count

//        if (hitCount >= 3) // Change this condition to 4 if you want the NPC to die after 4 hits
//        {
//            Die(); // Call the Die function if hit count reaches the desired value
//        }
//    }

//    // Function to handle enemy death
//    void Die()
//    {
//        // Perform death-related actions
//        Destroy(gameObject); // Destroy the enemy NPC GameObject
//    }
//}



using UnityEngine;
using UnityEngine.AI;

public class Enemy_NPC : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float followDistance = 2f; // Distance at which the NPC will start following the player
    public float runDistance = 5f; // Distance at which the NPC will start running towards the player
    public float stopDistance = 1.5f; // Distance at which the NPC will stop moving towards the player
    public float runSpeed = 5f; // Speed at which the NPC runs
    public float attackDistance = 1f; // Distance at which the NPC will start attacking the player
    public float counterInterval = 5f; // Interval between counter attacks

    private NavMeshAgent agent; // Reference to the NPC's NavMeshAgent
    private Animator animator; // Reference to the NPC's Animator component
    private bool isFollowing = false; // Is the NPC currently following the player?
    private bool isRun = false; // Is the NPC running?

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Getting the NavMeshAgent component attached to the NPC
        animator = GetComponent<Animator>(); // Getting the Animator component attached to the NPC

        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned. Please assign the player transform in the Inspector.");
        }
    }

    void Update()
    {
        if (playerTransform == null || animator == null || agent == null || !agent.isActiveAndEnabled)
            return;

        // Calculate the distance between the NPC and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Check if NPC should follow the player
        if (distanceToPlayer <= followDistance)
        {
            // Set destination to the player's position
            if (agent.isOnNavMesh)
                agent.SetDestination(playerTransform.position);

            // Determine whether NPC should run based on distance to player
            isRun = distanceToPlayer > stopDistance && distanceToPlayer <= runDistance;

            // Update animator parameter based on NPC's movement state
            animator.SetBool("isRun", isRun);

            // Set NPC's speed based on whether it's running
            agent.speed = isRun ? runSpeed : agent.speed;

            // Check if NPC should attack the player
            if (distanceToPlayer <= attackDistance)
            {
                // Trigger attack animation
                animator.SetTrigger("Attack");

                // Stop moving when attacking
                agent.velocity = Vector3.zero;
            }
            else
            {
                // NPC is actively following the player
                isFollowing = true;
            }

            // Update the counter attack timer
            counterInterval -= Time.deltaTime;
            if (counterInterval <= 0)
            {
                // Trigger counter attack animation
                animator.SetTrigger("Counter");
                // Reset counter timer
                counterInterval = 5f;
            }
        }
        else
        {
            if (isFollowing)
            {
                // Stop moving if player is out of follow distance
                agent.ResetPath();

                // Update animator parameter to idle state
                animator.SetBool("isRun", false);

                // NPC is no longer following the player
                isFollowing = false;
            }
        }

        // If NPC is idle and not too close to the player, stop NPC's animation
        if (!isRun && distanceToPlayer > followDistance)
        {
            agent.velocity = Vector3.zero;
        }
    }

    // Function to handle enemy death
    public void Die()
    {
        // Perform death-related actions
        Destroy(gameObject); // Destroy the enemy NPC GameObject
    }
}
