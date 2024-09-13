//using unityengine;
//using unityengine.ui;

//public class enemyhealth : monobehaviour
//{
//    public int maxhealth = 100;
//    public int currenthealth;
//    public slider healthslider; // reference to the slider component

//    void start()
//    {
//        currenthealth = maxhealth;
//        updatehealthui();
//    }

//    void updatehealthui()
//    {
//        // update the slider's value to reflect current health
//        healthslider.value = currenthealth;
//    }

//    public void takedamage(int damageamount)
//    {
//        currenthealth -= damageamount;
//        updatehealthui(); // update ui after taking damage

//        if (currenthealth <= 0)
//        {
//            die();
//        }
//    }

//    void die()
//    {
//        // implement death behavior here
//        debug.log("enemy has died!");
//        destroy(gameobject); // destroy the enemy object when it dies
//    }
//}







using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float damageCooldown = 1f; // Cooldown period between taking damage
    private float lastDamageTime; // Time of the last damage taken
    public Slider healthSlider; // Reference to the slider component

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        // Update the slider's value to reflect current health
        healthSlider.value = currentHealth;
    }

    public void TakeDamage1(int damageAmount)
    {
        // Check if enough time has passed since the last damage
        if (Time.time - lastDamageTime > damageCooldown)
        {
            currentHealth -= damageAmount;
            UpdateHealthUI(); // Update UI after taking damage

            if (currentHealth <= 0)
            {
                Die();
            }

            lastDamageTime = Time.time; // Update the last damage time
        }
    }

    void Die()
    {
        // Implement death behavior here
        Debug.Log("Enemy has died!");
        Destroy(gameObject); // Destroy the enemy object when it dies
    }
}



