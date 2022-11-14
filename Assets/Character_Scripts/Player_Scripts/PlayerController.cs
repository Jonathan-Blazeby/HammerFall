using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerCharacterAnimator animator;

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float rotationSpeed = 10;

    public MovementManager GetMovement() { return moveManager; }

    private void Update()
    {

        CaptureInput();
        animator.UpdateAnimatorValues();
    }

    //Looks for horizontal & vertical input to be applied to x z axis, and looks for jump
    private void CaptureInput()
    {
        Vector2 movementDirection = Vector2.zero;
        bool willJump = false;

        movementDirection = inputHandler.GetMoveInput();
        willJump = inputHandler.GetJumpInput();
        moveManager.Move(movementDirection, willJump, movementSpeed, jumpForce);
    }
    
}
