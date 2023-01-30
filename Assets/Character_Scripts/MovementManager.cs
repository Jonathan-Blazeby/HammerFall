using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMovement
{
    #region Private Fields
    [SerializeField] private Rigidbody moveRigidbody;
    [SerializeField] private Collider moveCollider;
    private Vector3 moveDirection;
    private Vector3 movementVelocity;
    private float verticalVelocity;
    private float rotationAmount;
    private float moveSpeed;
    //private float jumpAmount;

    private float groundCheckPointAdjust = 0.95f;
    private float groundCheckSphereDistAdjust = 0.25f;
    private int traversableLayer = 8;

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool jumpState;
    [SerializeField] private bool useCalculatedRotation;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        FindObjectOfType<GravityManager>().AddNewMoving(this);
    }

    private void Start()
    {
        GroundCheck();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }
    #endregion

    #region IMovement Implementation
    public Vector3 GetDirection() { return moveDirection; }
    public float GetVerticalVelocity() { return moveRigidbody.velocity.y; }
    public void SetVerticalVelocity(float velocity) { verticalVelocity = velocity; }
    //public float GetJumpAmount() { return jumpAmount; }
    //public bool GetJumpState() { return jumpState; }
    //public void SetJumpState(bool jump) { jumpState = jump; }
    public bool GetIsGrounded() { return isGrounded; }
    public void SetIsGrounded(bool grounded) { isGrounded = grounded; }
    public void SetUseCalculatedRotation(bool useCalcRotated) { useCalculatedRotation = useCalcRotated; }
    #endregion

    #region Private Methods
    private void MovementUpdate()
    {
        if (isGrounded)// || jumpState)
        {
            movementVelocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
        }

        movementVelocity.y = verticalVelocity;
        verticalVelocity = 0;

        moveRigidbody.AddForce(movementVelocity, ForceMode.Impulse);

        if (useCalculatedRotation)
        {
            Quaternion newRotation = Quaternion.Euler(0, rotationAmount * Time.fixedDeltaTime, 0);
            moveRigidbody.MoveRotation(newRotation);
        }
    }

    private void GroundCheck()
    {
        Vector3 checkPoint = moveCollider.bounds.center;
        checkPoint.y -= (moveCollider.bounds.extents.y - moveCollider.bounds.extents.x) * groundCheckPointAdjust;
        if (Physics.SphereCast(checkPoint, moveCollider.bounds.extents.x, Vector3.down, out RaycastHit hit, moveCollider.bounds.extents.y * groundCheckSphereDistAdjust))
        {
            if (hit.transform.gameObject.layer == traversableLayer)
            {
                isGrounded = true;
                //jumpState = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
    #endregion

    #region Public Methods
    public void SetMoveSpeed(float speed) => moveSpeed = speed;
    //public void SetJumpAmount(float jump) => jumpAmount = jump;

    public void Move(Vector2 direction)
    {
        moveDirection = transform.forward * direction.y;
        moveDirection += transform.right * direction.x;
        moveDirection.Normalize();
    }

    public void Rotate(float rotationInput)
    {
        rotationAmount += rotationInput;
    }

    //public void Jump(bool willJump)
    //{
    //    GroundCheck();
    //    if (isGrounded && willJump)
    //    {
    //        jumpState = true;
    //    }
    //    else if (isGrounded)
    //    {
    //        jumpState = false;
    //    }
    //}
    #endregion

}
