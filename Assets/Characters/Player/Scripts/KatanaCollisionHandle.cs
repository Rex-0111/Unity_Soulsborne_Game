using System;
using UnityEngine;


public class KatanaCollisionHandle : MonoBehaviour
{
    public event Action OnDamageEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Check if there are any subscribers before invoking the event

            if (OnDamageEnemy != null)
            {
                OnDamageEnemy.Invoke();
            }
        }
    }
}
