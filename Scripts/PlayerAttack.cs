using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damagePerAttack = 10; // Amount of damage per attack

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Assuming your enemy has a tag "Enemy"
        {
            // Reduce enemy's health
            other.GetComponent<EnemyHealth>().TakeDamage1(damagePerAttack);
        }
    }
}
