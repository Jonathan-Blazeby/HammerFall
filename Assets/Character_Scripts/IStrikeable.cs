using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrikeable
{
    public void ApplyForce(Vector3 force);
    public float GetMass();
}
