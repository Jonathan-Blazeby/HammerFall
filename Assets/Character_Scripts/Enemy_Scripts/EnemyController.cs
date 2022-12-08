using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, ICharacterController
{
    #region Serialized Fields
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private AttackManager attackManager;
    [SerializeField] private EnemyAIBasic aiInput;
    [SerializeField] private EnemyCharacterAnimator animator;
    private IDamageDealer attackApplicationComponent;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float attackForce = 1;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private int attackDamage = 5;
    #endregion

    private bool canAttack;

    public MovementManager GetMovement() { return moveManager; }

    private void Start()
    {
        Initialise();
    }

    private void Update()
    {
        ControllerUpdate();
    }

    private void LateUpdate()
    {
        attackManager.SetWeaponActive(animator.IsWeaponActive());
    }

    private void Initialise()
    {

        moveManager.SetJumpAmount(jumpForce);
        moveManager.SetUseCalculatedRotation(false);

        attackApplicationComponent = GetComponentInChildren<IDamageDealer>();

        attackManager.SetAttackApplicationComponent(attackApplicationComponent);
        attackApplicationComponent.SetWeaponDamage(attackDamage);
        attackApplicationComponent.SetWeaponForce(attackForce);

        canAttack = true;
    }

    private void ControllerUpdate()
    {
        MoveFunction();

        if (aiInput.GetState() == AIStates.Attacking)
        {
            AttackFunction();
        }

        if (aiInput.GetState() != AIStates.Dazed)
        {
            animator.UpdateAnimatorValues();
        }
        else
        {
            animator.Stop();
        }
    }

    //Looks for horizontal & vertical input to be applied to x z axis, and looks for jump
    private void MoveFunction()
    {
        Vector2 movementDirection = Vector2.zero;
        bool willJump = false;

        movementDirection = aiInput.GetDirection();

        moveManager.Move(movementDirection);
        moveManager.Rotate(0);
        moveManager.Jump(willJump);
    }

    private IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    private void AttackFunction()
    {
        if (canAttack)
        {
            StartCoroutine(AttackTimer());
            animator.Attack();
        }
    }

    public void Daze()
    {
        aiInput.SetDazed();
    }
}
