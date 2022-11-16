using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public Vector3 GetDirection();
    public float GetVerticalVelocity();
    public void SetVerticalVelocity(float velocity);
    public float GetJumpAmount();
    public bool GetJumpState();
    public void SetJumpState(bool jump);
    public bool GetIsGrounded();
    public void SetIsGrounded(bool grounded);
}
