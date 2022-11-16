using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMovement
{
    [SerializeField] private Rigidbody moveRigidbody;
    [SerializeField] private Collider moveCollider;
    private Vector3 moveDirection;
    private Vector3 movementVelocity;
    private float verticalVelocity;
    private float speed;
    private float jumpAmount;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool jumpState;

    #region IMovement Get/Set Methods
    public Vector3 GetDirection() { return moveDirection; }
    public float GetVerticalVelocity() { return moveRigidbody.velocity.y; }
    public void SetVerticalVelocity(float velocity) { verticalVelocity = velocity; }
    public float GetJumpAmount() { return jumpAmount; }
    public bool GetJumpState() { return jumpState; }
    public void SetJumpState(bool jump) { jumpState = jump; }
    public bool GetIsGrounded() { return isGrounded; }
    public void SetIsGrounded(bool grounded) { isGrounded = grounded; }
    #endregion

    private void FixedUpdate()
    {
        movementVelocity = moveDirection * speed * Time.fixedDeltaTime;

        movementVelocity.y = verticalVelocity;
        verticalVelocity = 0;

        moveRigidbody.AddForce(movementVelocity, ForceMode.Impulse);

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
