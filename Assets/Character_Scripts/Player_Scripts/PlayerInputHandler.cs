using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Vector2 moveInput;
    private bool willJump;

    public Vector2 GetMoveInput() { return moveInput; }
    public bool GetJumpInput() { return willJump; }

    private void FixedUpdate()
    {
        moveInput = Vector2.zero;
        willJump = false;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveInput.x += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveInput.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveInput.y -= 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            willJump = true;
        }
    }

}
