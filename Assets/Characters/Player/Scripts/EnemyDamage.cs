using UnityEngine;
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float Health = 100;
    [SerializeField] GameObject KatanaEquip;
    private KatanaCollisionHandle katanaCollisionHandle;
    private string katanaEquipName = "KatanaEquip";

    private void Start()
    {
        KatanaEquip = GameObject.Find(katanaEquipName);
        katanaCollisionHandle = KatanaEquip.GetComponent<KatanaCollisionHandle>();
        
        katanaCollisionHandle.OnDamageEnemy += Damage;
    }

    private void Damage()
    {
        Health -= 10;
      
    }
}
