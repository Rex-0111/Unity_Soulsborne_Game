using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private IAA_Player inputActions;
    private bool animationState01;
    private bool animationState02;
    private bool animationState03;
    private float animationsNormalizedTime;
    public bool CanAttack { get; private set; }
    private int attackIncrement = 0;
    private Animator animator;
    private int attackPhaseId;
    [Range(0, 1)] [SerializeField] private float attackWindowOpen;
    [Range(0, 1)] [SerializeField] private float attackWindowClose;
    private bool canAttackNext;

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

    private void Start()
    {
        CanAttack = true;
        canAttackNext = true;
        animator = GetComponent<Animator>();
        attackPhaseId = Animator.StringToHash("AttackPhases");
        inputActions.Player.Attack.performed += AttackAction;
    }

    private void Update()
    {
        // Update animator state and normalized time each frame
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animationState01 = stateInfo.IsName("Attack01_1");
        animationState02 = stateInfo.IsName("Attack01_2");
        animationState03 = stateInfo.IsName("Attack01_3");
        animationsNormalizedTime = stateInfo.normalizedTime;

        // Determine whether the next attack is allowed based on the current animation state
        if ((animationState01 || animationState02 || animationState03) && 
            (animationsNormalizedTime >= 0f && animationsNormalizedTime <= attackWindowOpen))
        {
            canAttackNext = false;
        }
        else if ((animationState01 || animationState02 || animationState03) && 
                 (animationsNormalizedTime > attackWindowOpen && animationsNormalizedTime <= attackWindowClose))
        {
            canAttackNext = true;
        }
        else if ((animationState01 || animationState02 || animationState03) && 
                 (animationsNormalizedTime > attackWindowClose && animationsNormalizedTime < 1f))
        {
            canAttackNext = false;
        }
        else if ((animationState01 || animationState02 || animationState03) && 
                 animationsNormalizedTime >= 1f)
        {
            AttackReset();
            canAttackNext = true;
        }
    }

    private void AttackAction(InputAction.CallbackContext context)
    {
        if (CanAttack && canAttackNext)
        {
            ++attackIncrement;
            if (attackIncrement > 4) { attackIncrement = 0; }
            animator.SetInteger(attackPhaseId, attackIncrement);
        }
    }

    private void AttackReset()
    {
        attackIncrement = 0;
        animator.SetInteger(attackPhaseId, 0);
    }
}
