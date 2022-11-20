using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private AttackManager attackManager;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerCharacterAnimator animator;
    private float rotationInput;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private int attackDamage = 5;
    private bool hasAttacked;

    public MovementManager GetMovement() { return moveManager; }

    private void Update()
    {

        CaptureInput();
        animator.UpdateAnimatorValues();
    }

    //Looks for horizontal & vertical input to be applied to x z axis, looks for jump input, looks for attack input
    private void CaptureInput()
    {
        Vector2 movementDirection = Vector2.zero;
        bool willJump = false;

        movementDirection = inputHandler.GetMoveInput();
        rotationInput = inputHandler.GetMouseInput();
        willJump = inputHandler.GetJumpInput();
        moveManager.Move(movementDirection, rotationInput, willJump, movementSpeed, jumpForce, rotationSpeed);

        int attackDirection = 0; //0 = No attack, 1 = left attack, 2 = right attack
        attackDirection = inputHandler.GetAttackInput();
        if(attackDirection > 0)
        {
            hasAttacked = true;
            animator.Attack(attackDirection);
            attackManager.ActivateWeapon(attackDamage);
        }

        if(hasAttacked)
        {
            hasAttacked = false;
            if (!animator.HammerSwingFinishedCheck())
            {
                attackManager.DeactivateWeapon();
            }
        }

    }
    
}
