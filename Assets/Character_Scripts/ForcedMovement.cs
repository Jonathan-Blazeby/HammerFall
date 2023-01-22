using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedMovement : MonoBehaviour, IStrikeable
{
    #region Private Fields
    [SerializeField] private Rigidbody ownRigidbody;
    [SerializeField] private float forcedDelay = 0.75f;
    private Vector3 appliedForce;
    private bool canBeForced = true;
    #endregion

    #region MonoBehaviour Callbacks
    private void FixedUpdate()
    {
        ForceUpdate();
    }
    #endregion

    #region Private Methods
    private void ForceUpdate()
    {
        ownRigidbody.AddForce(appliedForce * Time.fixedDeltaTime, ForceMode.Impulse);
        appliedForce /= 2;
    }
    
    private IEnumerator ForceTimer()
    {
        canBeForced = false;
        yield return new WaitForSeconds(forcedDelay);
        canBeForced = true;
    }
    #endregion

    #region IStrikeable Implementation
    /// <summary>
    /// Takes in the Force parameter as a direction and initial magnitude to apply force to the character
    /// </summary>
    /// <param name="force">The direction Magnitude of the impact</param>
    public void ApplyForce(Vector3 force)
    {
        if (!canBeForced) { return; }

        StartCoroutine(ForceTimer());
        appliedForce = force;
    }
    #endregion

}
