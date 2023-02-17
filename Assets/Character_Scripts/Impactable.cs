using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impactable : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private AudioClip softImpactAudio;
    [SerializeField] private AudioClip hardImpactAudio;
    [SerializeField] private float softImpactForceThreshold = 6.0f;
    [SerializeField] private float hardImpactForceThreshold = 12.0f;
    [SerializeField] private int softImpactDamage = 5;
    [SerializeField] private int hardImpactDamage = 10;

    [SerializeField] private bool hurtsPlayer = false;
    #endregion

    #region MonoBehavior Callbacks
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable healthComponent = collision.collider.GetComponent<IDamageable>();
        if(healthComponent is null) { return; }

        if(healthComponent.CompareTag("Player") && !hurtsPlayer) { return; }
        
        if (collision.relativeVelocity.magnitude >= hardImpactForceThreshold)
        {
            //Debug.Log("Hard Impact");
            AudioSource.PlayClipAtPoint(hardImpactAudio, collision.collider.ClosestPointOnBounds(transform.position));
            healthComponent.ApplyDamage(hardImpactDamage);
        }
        else if (collision.relativeVelocity.magnitude >= softImpactForceThreshold)
        {
            //Debug.Log("Soft Impact");
            AudioSource.PlayClipAtPoint(softImpactAudio, collision.collider.ClosestPointOnBounds(transform.position));
            healthComponent.ApplyDamage(softImpactDamage);
        }
    }
    #endregion
}
