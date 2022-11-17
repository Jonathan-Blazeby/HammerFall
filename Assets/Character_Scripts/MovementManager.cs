using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMovement
{
    [SerializeField] private Rigidbody moveRigidbody;
    [SerializeField] private Collider moveCollider;
    private Vector3 moveDirection;
    private Vector3 movementVelocity;
    private Quaternion targetRot;
    private float verticalVelocity;
    private float rotationAmount;
    private float speed;
    private float jumpAmount;
    private float rotationSpeed;
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

        Quaternion newRotation = Quaternion.Euler(0, rotationAmount * rotationSpeed * Time.fixedDeltaTime, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, 1);
    }

    public void Move(Vector2 direction, float rotationInput, bool willJump, float characterSpeed, float jumpForce, float characterRotationSpeed)
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

        rotationAmount += rotationInput;
        speed = characterSpeed;
        jumpAmount = jumpForce;
        rotationSpeed = characterRotationSpeed;
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
}
