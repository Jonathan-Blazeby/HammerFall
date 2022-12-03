using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterController
{
    #region Serialized Fields
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private AttackManager attackManager;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerCharacterAnimator animator;
    private IDamageDealer attackApplicationComponent;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float attackForce = 1;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float attackDelay = 1.2f;
    #endregion

    private float rotationInput;
    private float attackTimer;

    public MovementManager GetMovement() { return moveManager; }

    private void Start()
    {
        moveManager.SetMoveSpeed(movementSpeed);
        moveManager.SetJumpAmount(jumpForce);
        moveManager.SetUseCalculatedRotation(true);

        attackApplicationComponent = GetComponentInChildren<IDamageDealer>();

        attackManager.SetAttackApplicationComponent(attackApplicationComponent);
        attackApplicationComponent.SetWeaponDamage(attackDamage);
        attackApplicationComponent.SetWeaponForce(attackForce);
    }

    private void LateUpdate()
    {
        attackTimer -= Time.deltaTime;
        CaptureInput();
        animator.UpdateAnimatorValues();
        attackManager.SetWeaponActive(animator.IsWeaponActive());
    }

    //Looks for horizontal & vertical input to be applied to x z axis, looks for jump input, looks for attack input
    private void CaptureInput()
    {
        Vector2 movementDirection = Vector2.zero;
        bool willJump = false;

        movementDirection = inputHandler.GetMoveInput();
        rotationInput = inputHandler.GetMouseInput();
        willJump = inputHandler.GetJumpInput();

        moveManager.Move(movementDirection);
        RotateCalc();
        moveManager.Jump(willJump);

        int attackDirection = 0; //0 = No attack, 1 = left attack, 2 = right attack
        attackDirection = inputHandler.GetAttackInput();
        if(attackDirection > 0 && attackTimer <= 0)
        {
            attackManager.SetAttackDirection(attackDirection);
            animator.Attack(attackDirection);
            attackTimer = attackDelay;
        }
    }

    private void RotateCalc()
    {
        rotationInput = inputHandler.GetMouseInput() * rotationSpeed;
        moveManager.Rotate(rotationInput);
    }

    public void Daze()
    {

    }

}
