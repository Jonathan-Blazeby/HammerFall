using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMovement
{
    [SerializeField] private Rigidbody moveRigidbody;
    [SerializeField] private Vector3 moveDirection;
    private bool jumpState;

    public Vector3 GetDirection()
    {
        return moveDirection;
    }

    public bool GetJumpState()
    {
        return jumpState;
    }

    public void Move(Vector2 direction, bool willJump, float characterSpeed)
    {
        float delta = Time.deltaTime;

        moveDirection = transform.forward * direction.y;
        moveDirection += transform.right * direction.x;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = characterSpeed;
        Vector3 movementVel = moveDirection * speed;

        Vector3 projectedVel = Vector3.ProjectOnPlane(movementVel, Vector3.zero);
        moveRigidbody.velocity = projectedVel;

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


private void FixedUpdate()
    {
        
    }

}
