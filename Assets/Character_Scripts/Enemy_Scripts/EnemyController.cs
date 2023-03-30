using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, ICharacterController
{
    #region Private Fields
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private EnemyHealth healthManager;
    [SerializeField] private AttackManager attackManager;
    [SerializeField] private EnemyAIStandard aiInput;
    [SerializeField] private EnemyCharacterAnimator animator;
    [SerializeField] private ForcedMovement forcedMovement;
    [SerializeField] private float corpseRemainTime = 4.0f;
    [SerializeField] private float attackForce = 1;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private int attackDamage = 5;

    private IDamageDealer attackApplicationComponent;

    private bool isGrounded;
    private bool canAttack;
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }

    private void OnEnable()
    {
        aiInput.Initialise();
        animator.SmokeBurst();
    }

    private void Update()
    {
        ControllerUpdate();
    }

    private void LateUpdate()
    {
        attackManager.SetWeaponActive(animator.IsWeaponActive());
    }
    #endregion

    #region ICharacterController Implementation
    public void Daze()
    {
        if(aiInput.GetCurrentState() == aiInput.GetDeadState()) { return; }
        aiInput.SetCurrentState(aiInput.GetDazedState());
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        InitialiseOwnManagers();

        moveManager.SetUseCalculatedRotation(false);

        attackApplicationComponent = GetComponentInChildren<IDamageDealer>();

        attackManager.SetAttackApplicationComponent(attackApplicationComponent);
        attackApplicationComponent.SetWeaponDamage(attackDamage);
        attackApplicationComponent.SetWeaponForce(attackForce);

        canAttack = true;
    }

    private void InitialiseOwnManagers()
    {
        moveManager.Initialise();
        healthManager.Initialise();
        attackManager.Initialise();
        animator.Initialise(moveManager);
    }

    private void ControllerUpdate()
    {
        isGrounded = moveManager.GetIsGrounded();
        aiInput.SetIsGrounded(isGrounded);

        var state = aiInput.GetCurrentState();
        string stateString = state.GetType().ToString();
        switch(stateString)
        {
            case "IdleState":
                return;
            case "MoveState":
                break;
            case "AttackState":
                AttackFunction();
                break;
            case "DazedState":  
                animator.Stop();
                return;
            case "DeadState":
                animator.Die();
                return;
        }

        MoveFunction();
        animator.UpdateAnimatorValues();
    }

    //Looks for horizontal & vertical input to be applied to x z axis
    private void MoveFunction()
    {
        Vector2 movementDirection;

        movementDirection = aiInput.GetDirection();

        moveManager.Move(movementDirection);
        moveManager.Rotate(0);
        isGrounded = moveManager.GetIsGrounded();
    }

    private IEnumerator CorpseDisappearTimer()
    {
        yield return new WaitForSeconds(corpseRemainTime);
        animator.SmokeBurst();
        gameObject.SetActive(false);
    }

    private IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    private void AttackFunction()
    {
        if (!canAttack) { return; } 
        StartCoroutine(AttackTimer());
        animator.Attack();

    }
    #endregion

    #region Public Methods
    public MovementManager GetMovement() { return moveManager; }

    public void Death()
    {
        aiInput.SetCurrentState(aiInput.GetDeadState());
        StartCoroutine(CorpseDisappearTimer());
    }
    #endregion

}
