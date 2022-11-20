using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 moveInput;
    private float mouseXInput;
    private int attackInput; //attackInput 0 = no Input, 1 = left Input, 2 = right Input
    private bool willJump;

    public Vector2 GetMoveInput() { return moveInput; }
    public float GetMouseInput() { return mouseXInput; }
    public int GetAttackInput() { return attackInput; }
    public bool GetJumpInput() { return willJump; }

    private void FixedUpdate()
    {
        moveInput = Vector2.zero;
        attackInput = 0;
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

        if(Input.GetMouseButton(0))
        {
            attackInput = 1;
        }
        else if(Input.GetMouseButton(1))
        {
            attackInput = 2;
        }

        mouseXInput = Input.GetAxis("Mouse X");
    }

}
