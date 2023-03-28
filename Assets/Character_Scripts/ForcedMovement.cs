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
    /// <summary>
    /// Applies force to the character's rigidbody every fixed framerate frame if over minimum.
    /// Then divides the applied force by 2 to slow it down to 
    /// </summary>
    private void ForceUpdate()
    {
        if (appliedForce.magnitude <= 0.01f) { return; }
        ownRigidbody.AddForce(appliedForce * Time.fixedDeltaTime, ForceMode.Impulse);
        appliedForce /= 2;

    }
    
    /// <summary>
    /// Prevents a character being force-moved for a period of time following the last
    /// </summary>
    /// <returns></returns>
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

    public float GetMass()
    {
        return ownRigidbody.mass;
    }

    public void setCanForce(bool canForce)
    {
        canBeForced = false;
    }
    #endregion

}
