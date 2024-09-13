using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public Slider HealthBar; // Reference to the health bar component

    void Start()
    {
        // Ensure the Animator component is assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Check if player's health is less than or equal to 0
        // Replace this condition with your actual health checking logic
        if (HealthBar.value <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Trigger the death animation
        animator.SetTrigger("DieTrigger");

        // Optionally, disable player control or other actions during death animation
        // Example: Disable movement script
        GetComponent<Movement>().enabled = false;

        // Add any additional functionality here, such as game over logic
    }
}
