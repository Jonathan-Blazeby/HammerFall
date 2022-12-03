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
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float attackForce = 1;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private int attackDamage = 5;
    #endregion

    private float attackTimer;

    public MovementManager GetMovement() { return moveManager; }

    private void Start()
    {
        
        moveManager.SetJumpAmount(jumpForce);
        moveManager.SetUseCalculatedRotation(false);

        attackApplicationComponent = GetComponentInChildren<IDamageDealer>();

        attackApplicationComponent.SetWeaponDamage(attackDamage);
        attackApplicationComponent.SetWeaponForce(attackForce);
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        MoveFunction();
        if (!aiInput.GetDazed())
        {
            
            AttackFunction();
            animator.UpdateAnimatorValues();
        }
        else
        {
            animator.Stop();
        }
    }

    private void LateUpdate()
    {
        attackManager.SetWeaponActive(animator.IsWeaponActive());
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

    private void AttackFunction()
    {
        int attackDirection = 0; //0 = No attack, 1 = left attack, 2 = right attack
        attackDirection = aiInput.GetWillAttack();
        if (attackDirection > 0 && attackTimer <= 0)
        {
            animator.Attack(attackDirection);
            attackTimer = attackDelay;
        }
    }

    public void Daze()
    {
        aiInput.SetDazed();
    }
}
