using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Player_Health_System playerHealthSystem;
    private GameObject healthBar;
    Slider healthBarSlider;

    void Start()
    {
        playerHealthSystem = GetComponent<Player_Health_System>(); // Ensure this is the correct GameObject

        // Find the child GameObject named "HealthBar" among all children
        Transform healthBarTransform = null;

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "HealthBar")
            {
                healthBarTransform = child;
                break; // Exit the loop once found
            }
        }

        if (healthBarTransform != null)
        {
            // Get the Slider component from the HealthBar GameObject
            healthBarSlider = healthBarTransform.GetComponent<Slider>();

            if (healthBarSlider != null)
            {
                
                // Initialize health bar
                healthBarSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.maxHealth;
            }
            else
            {
                Debug.LogError("No Slider component found on HealthBar.");
            }
        }
        else
        {
            Debug.LogError("HealthBar not found in children of " + gameObject.name);
        }

        if (playerHealthSystem != null)
        {
            playerHealthSystem.OnPlayerDamage += UpdateHealthBar;
        }
        else
        {
            Debug.LogError("Player_Health_System component not found on this GameObject.");
        }
    }


    private void UpdateHealthBar(float currentHealth)
    {
        if (playerHealthSystem != null)
        {
            healthBarSlider.value = currentHealth / playerHealthSystem.maxHealth;
        }
    }
}
