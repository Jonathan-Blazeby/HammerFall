using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    #region Monobehavior Callbacks
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable is null) { return; }
        damageable.ApplyDamage(damageable.GetMaxHealth());
    }
    #endregion

}
