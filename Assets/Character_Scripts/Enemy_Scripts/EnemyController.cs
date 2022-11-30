using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private AttackManager attackManager;
    [SerializeField] private EnemyAIBasic aiInput;
    [SerializeField] private EnemyCharacterAnimator animator;
    //private float rotationInput;
    private float attackTimer;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float attackForce = 1;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private int attackDamage = 5;    

    public MovementManager GetMovement() { return moveManager; }

    private void Start()
    {
        
        moveManager.SetJumpAmount(jumpForce);
        moveManager.SetUseCalculatedRotation(false);
        attackManager.SetWeaponDamage(attackDamage);
        attackManager.SetWeaponForce(attackForce);
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        AIInput();
        animator.UpdateAnimatorValues();
    }

    private void LateUpdate()
    {
        attackManager.SetWeaponActive(animator.IsWeaponActive());
    }

    //Looks for horizontal & vertical input to be applied to x z axis, and looks for jump
    private void AIInput()
    {
        Vector2 movementDirection = Vector2.zero;
        bool willJump = false;

        movementDirection = aiInput.GetDirection();

        moveManager.Move(movementDirection);
        moveManager.Rotate(0);
        moveManager.Jump(willJump);

        int attackDirection = 0; //0 = No attack, 1 = left attack, 2 = right attack
        attackDirection = aiInput.GetWillAttack();
        if (attackDirection > 0 && attackTimer <= 0)
        {
            animator.Attack(attackDirection);
            attackTimer = attackDelay;
        }

    }

}
