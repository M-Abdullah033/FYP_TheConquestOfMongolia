using UnityEngine;

public class Player2Combat : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage dealt to the other player
    public float attackRange = 2f; // Maximum range for attacking
    public KeyCode attackKey = KeyCode.LeftControl; // Key to trigger the attack
    public KeyCode spinKey = KeyCode.LeftAlt;
    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
        if (Input.GetKeyDown(spinKey))
        {
            Spin();
        }
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player") && collider != this.GetComponent<Collider>())
            {
                PlayerHealth enemyHealth = collider.GetComponent<PlayerHealth>(); // Assuming the other player has a PlayerHealth component
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount); // Reduce health of the other player by the specified damage amount
                }
            }
        }
    }
    void Spin()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player") && collider != this.GetComponent<Collider>())
            {
                PlayerHealth enemyHealth = collider.GetComponent<PlayerHealth>(); // Assuming the other player has a PlayerHealth component
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount); // Reduce health of the other player by the specified damage amount
                }
            }
        }
    }
}
