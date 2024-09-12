using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attacks : MonoBehaviour
{
    public bool isEquipped;
    IAA_Player inputActions;
    Player_Locomotion locomotionScript;
    string AnimationName;
    //Animator
    int EquipWeaponId;
    int UnEquipWeaponId;
    Animator animator;
    bool Can_Equip_UnEquip;

    int check = 0;
    private void Awake()
    {
        inputActions = new IAA_Player();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    void Start()
    {
        locomotionScript = GetComponent<Player_Locomotion>();
        isEquipped = false;
        animator = GetComponent<Animator>();
        EquipWeaponId = Animator.StringToHash("EquipWeapon");
        UnEquipWeaponId = Animator.StringToHash("UnEquipWeapon");
        inputActions.Player.EquipWeapon.performed += EquipWeapon;
    }

    private void EquipWeapon(InputAction.CallbackContext context)
    {
        if (Can_Equip_UnEquip)
        {
            check++;
            if (check % 2 == 1) { isEquipped = false; }
            if (check % 2 == 0) { isEquipped = true; }
            if (!isEquipped)
            {
                animator.SetBool(EquipWeaponId, true);
            }
            if (isEquipped)
            {
                animator.SetBool(UnEquipWeaponId, true);
            }
        }
        Invoke("boolOff", 0.2f);
    }
    void boolOff()
    {
        animator.SetBool(EquipWeaponId, false);
        animator.SetBool(UnEquipWeaponId, false);

    }
    private void LateUpdate()
    {
        AnimationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (AnimationName == "Equip" || AnimationName == "UnEquip")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                Can_Equip_UnEquip = false;
                locomotionScript.canMove = false;
            }
        }
        else
        {
            Can_Equip_UnEquip = true;
            locomotionScript.canMove = true;
        }
    }
}
