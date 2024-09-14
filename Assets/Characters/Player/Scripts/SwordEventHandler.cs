using UnityEngine;

public class SwordEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject SheatheWeapon;
    [SerializeField] private GameObject EquipWeapon;
    [SerializeField] private Collider weaponCollider;

    private void Start()
    {
        // Try to get the Collider if not assigned via Inspector
        if (weaponCollider == null)
        {
            weaponCollider = GetComponent<Collider>();
            if (weaponCollider == null)
            {
                Debug.LogError("No Collider component found on this GameObject.");
            }
        }
    }

    // Method to hide the equip weapon and show the sheathed weapon
    public void DisableEquipWeaponAndEnableSheatheWeapon()
    {
        if (SheatheWeapon != null && EquipWeapon != null)
        {
            SheatheWeapon.SetActive(true);
            EquipWeapon.SetActive(false);
        }
        else
        {
            Debug.LogWarning("SheatheWeapon or EquipWeapon GameObject is not assigned.");
        }
    }

    // Method to hide the sheathed weapon and show the equipped weapon
    public void DisableSheatheWeaponAndEnableEquipWeapon()
    {
        if (SheatheWeapon != null && EquipWeapon != null)
        {
            SheatheWeapon.SetActive(false);
            EquipWeapon.SetActive(true);
        }
        else
        {
            Debug.LogWarning("SheatheWeapon or EquipWeapon GameObject is not assigned.");
        }
    }

    // Method to enable the weapon collider 
    public void WeaponCollider_ON()
    {
    // This is an Animation Event
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
            
        }
        else
        {
            Debug.LogWarning("Weapon collider is not assigned.");
        }
    }

    // Method to disable the weapon collider
    public void WeaponCollider_OFF()
    {
        // This is an Animation Event
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
            
        }
        else
        {
            Debug.LogWarning("Weapon collider is not assigned.");
        }
    }
}
