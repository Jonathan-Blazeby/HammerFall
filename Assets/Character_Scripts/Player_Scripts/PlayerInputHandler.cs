using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    #region Private Fields
    private Vector2 moveInput;
    private float mouseXInput;
    private AttackType attackInput; //attackInput 0 = no Input, 1 = left Input, 2 = right Input
    private bool willJump;
    #endregion

    #region MonoBehaviour Callbacks
    private void Update()
    {
        InputCapture();
    }
    #endregion

    #region Private Methods
    private void InputCapture()
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

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        willJump = Input.GetButton("Jump");



        if (Input.GetButton("LeftAttack"))
        {
            attackInput = AttackType.left;
        }
        else if (Input.GetButton("RightAttack"))
        {
            attackInput = AttackType.right;
        }
        else
        {
            attackInput = AttackType.none;
        }

        if (Input.GetKey(KeyCode.R))
        {
            GameManager.Instance.ResetPlayer();
        }

        mouseXInput = Input.GetAxis("Mouse X");
    }
    #endregion

    #region Public Methods
    public Vector2 GetMoveInput() => moveInput;
    public float GetMouseInput() => mouseXInput;
    public AttackType GetAttackInput() => attackInput;
    public bool GetJumpInput() => willJump;
    #endregion 

}
