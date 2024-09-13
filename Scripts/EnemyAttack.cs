using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10; // Amount of damage per attack

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has a tag "Player"
        {
            // Reduce player's health
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
