using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMovement
{
    [SerializeField] private Rigidbody moveRigidbody;
    [SerializeField] private Collider moveCollider;
    private Vector3 moveDirection;
    private Vector3 movementVel;
    private float speed;
    private float jumpAmount;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool jumpState;

    private void FixedUpdate()
    {
        movementVel = moveDirection * speed * Time.fixedDeltaTime;

        if (moveRigidbody.velocity.y < 0 && isGrounded)
        {
            movementVel.y = Physics.gravity.y * Time.fixedDeltaTime;
        }
        else
        {
            movementVel.y += Physics.gravity.y * Time.fixedDeltaTime;
        }

        if (moveRigidbody.velocity.y < 0 && jumpState)
        {
            jumpState = false;
        }

        if (jumpState && isGrounded)
        {
            isGrounded = false;
            movementVel.y += Mathf.Sqrt(jumpAmount * -Physics.gravity.y);
        }

        moveRigidbody.AddForce(movementVel, ForceMode.Impulse);

    }

    public Vector3 GetDirection()
    {
        return moveDirection;
    }

    public bool GetJumpState()
    {
        return jumpState;
    }

    public void Move(Vector2 direction, bool willJump, float characterSpeed, float jumpForce)
    {
        moveDirection = transform.forward * direction.y;
        moveDirection += transform.right * direction.x;
        moveDirection.Normalize();

        GroundCheck();
        if (isGrounded && willJump)
        {
            jumpState = true;
        }
        else if (isGrounded)
        {
            jumpState = false;
        }
        
        speed = characterSpeed;
        jumpAmount = jumpForce;
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        Vector3 checkPoint = moveCollider.bounds.center;
        checkPoint.y -= (moveCollider.bounds.extents.y * 0.95f);
        if (Physics.Raycast(checkPoint, Vector3.down, out hit, (moveCollider.bounds.extents.y * 0.1f)))
        {
            if (hit.transform.gameObject.layer == 8)
            {
                isGrounded = true;
                jumpState = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    //#region Movement
    //Vector3 normalVec;
    //Vector3 targetPos;

    //private void HandleRotation(float delta)
    //{
    //    Vector3 targetDir = Vector3.zero;
    //    float moveOverride = inputHandler.moveAmount;

    //    targetDir = transform.forward * moveDirection.y;
    //    targetDir += transform.right * moveDirection.x;

    //    targetDir.Normalize();
    //    targetDir.y = 0;

    //    if (targetDir == Vector3.zero)
    //    {
    //        targetDir = myTransform.forward;
    //    }

    //    float rs = rotationSpeed;
    //    Quaternion tr = Quaternion.LookRotation(targetDir);
    //    Quaternion targetRot = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

    //    myTransform.rotation = targetRot;
    //}

    //#endregion


}
