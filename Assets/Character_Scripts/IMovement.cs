using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public Vector3 GetDirection();
    public bool GetJumpState();
}
