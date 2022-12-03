using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedMovement : MonoBehaviour, IStrikeable
{
    [SerializeField] private Rigidbody ownRigidbody;
    [SerializeField] private float forcedDelay = 0.75f;
    private Vector3 appliedForce;
    private float forceTimer;
    private float cutOff = 0.2f;

    private void Update()
    {
        forceTimer -= Time.deltaTime;
        ownRigidbody.AddForce(appliedForce * Time.deltaTime, ForceMode.Impulse);
        appliedForce /= 2;
        
    }

    public void ApplyForce(Vector3 force)
    {
        if (forceTimer <= 0)
        {
            forceTimer = forcedDelay;
            appliedForce = force;
        }
    }
}
