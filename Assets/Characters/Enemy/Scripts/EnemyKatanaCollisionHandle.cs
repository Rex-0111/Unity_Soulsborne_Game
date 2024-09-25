using System;
using UnityEngine;

public class EnemyKatanaCollisionHandle : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Health_System player_Health_System = other.GetComponent<Player_Health_System>();
            if (player_Health_System != null)
            {
                player_Health_System.Damage(4);
            }
            else
            {
                Debug.LogWarning("EnemyDamage component not found on the enemy object.");
            }
        }
    }
}
