using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impactable : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private float softImpactForceThreshold = 6.0f;
    [SerializeField] private float hardImpactForceThreshold = 12.0f;
    [SerializeField] private int softImpactDamage = 5;
    [SerializeField] private int hardImpactDamage = 10;
    #endregion

    #region MonoBehavior Callbacks
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<IDamageable>() == null) { return; }

        Debug.Log("Impact Force: " + collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude >= hardImpactForceThreshold)
        {
            collision.collider.GetComponent<IDamageable>().ApplyDamage(hardImpactDamage);
        }
        else if (collision.relativeVelocity.magnitude >= softImpactForceThreshold)
        {
            collision.collider.GetComponent<IDamageable>().ApplyDamage(softImpactDamage);
        }
    }
    #endregion
}
