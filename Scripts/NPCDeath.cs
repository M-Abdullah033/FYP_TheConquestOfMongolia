using UnityEngine;
using UnityEngine.AI;

public class NPCDeath : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component (if used for NPC movement)

    void Start()
    {
        // Ensure the Animator component is assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Ensure the NavMeshAgent component is assigned (if used for NPC movement)
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }

    // Function to trigger the NPC's death animation and destroy the GameObject
    public void Die()
    {
        // Trigger the death animation
        animator.SetTrigger("DieTrigger");

        // Optionally, disable NPC control scripts or other actions during death animation
        // For example, if the NPC has a NavMeshAgent, you may want to stop its navigation
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        // Optionally, disable collider to prevent further interactions
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Destroy the GameObject after a delay (optional)
        Destroy(gameObject, 3f);
    }
}
