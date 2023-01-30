using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterController
{
    #region Private Fields
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private AttackManager attackManager;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerCharacterAnimator animator;
    private IDamageDealer attackApplicationComponent;
    [SerializeField] private float movementSpeed = 5.0f;
    //[SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float attackForce = 1.0f;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float attackDelay = 1.2f;
    private Vector2 movementDirection = Vector2.zero;
    //private bool willJump = false;
    private float rotationInput;
    private bool canAttack;
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }

    private void LateUpdate()
    {
        CaptureInput();
        Move();
        Attack();
        animator.UpdateAnimatorValues();
        attackManager.SetWeaponActive(animator.IsWeaponActive());
    }
    #endregion

    #region ICharacterController Implementation
    public void Daze() { }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        moveManager.SetMoveSpeed(movementSpeed);
        //moveManager.SetJumpAmount(jumpForce);
        moveManager.SetUseCalculatedRotation(true);

        attackApplicationComponent = GetComponentInChildren<IDamageDealer>();

        attackManager.SetAttackApplicationComponent(attackApplicationComponent);
        attackApplicationComponent.SetWeaponDamage(attackDamage);
        attackApplicationComponent.SetWeaponForce(attackForce);

        canAttack = true;
    }

    //Looks for horizontal & vertical input to be applied to x z axis, looks for jump input, looks for attack input
    private void CaptureInput()
    {
        movementDirection = inputHandler.GetMoveInput();
        rotationInput = inputHandler.GetMouseInput();
        //willJump = inputHandler.GetJumpInput();
    }

    private void Move()
    {
        moveManager.Move(movementDirection);
        RotateCalc();
        //moveManager.Jump(willJump);
        movementDirection = Vector2.zero;
        //willJump = false;
    }

    private IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    private void Attack()
    {
        AttackType attackDirection; //0 = No attack, 1 = left attack, 2 = right attack
        attackDirection = inputHandler.GetAttackInput();
        if (attackDirection > 0 && canAttack)
        {
            StartCoroutine(AttackTimer());
            attackManager.SetAttackDirection(((int)attackDirection));
            animator.Attack(((int)attackDirection));
        }
    }

    private void RotateCalc()
    {
        rotationInput = inputHandler.GetMouseInput() * rotationSpeed;
        moveManager.Rotate(rotationInput);
    }
    #endregion

    #region Public Methods
    public MovementManager GetMovement() { return moveManager; }
    #endregion

}
