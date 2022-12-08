using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedMovement : MonoBehaviour, IStrikeable
{
    [SerializeField] private Rigidbody ownRigidbody;
    [SerializeField] private float forcedDelay = 0.75f;
    private Vector3 appliedForce;
    private bool canBeForced = true;

    private void Update()
    {
        ForceUpdate();
    }

    private void ForceUpdate()
    {
        ownRigidbody.AddForce(appliedForce * Time.deltaTime, ForceMode.Impulse);
        appliedForce /= 2;
    }

    private IEnumerator ForceTimer()
    {
        canBeForced = false;
        yield return new WaitForSeconds(forcedDelay);
        canBeForced = true;
    }

    public void ApplyForce(Vector3 force)
    {
        if (canBeForced && gameObject.activeInHierarchy)
        {
            StartCoroutine(ForceTimer());
            appliedForce = force;
        }
    }
}
