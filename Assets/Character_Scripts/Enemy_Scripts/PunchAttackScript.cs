using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttackScript : MonoBehaviour, IDamageDealer
{
    #region Private Fields
    private float forceMultiplier;
    private int attackDamage;
    #endregion

    #region IDamageDealer Implementation
    public void AddDazed(ICharacterController recipient)
    {
        recipient.Daze();
    }

    public void AddDamage(IDamageable recipient)
    {
        if (recipient.CompareTag("Enemy")) { return; }
        recipient.ApplyDamage(attackDamage);
    }

    public void AddForce(IStrikeable recipient, int directionRelativeToSelf) //0 = No direction, 1 = Left Swing, 2 = Right Swing
    {
        Vector3 forceVector = transform.root.forward;
        forceVector += Vector3.up * -Physics.gravity.y; //*recipient.GetMass();
        forceVector *= forceMultiplier;
        recipient.ApplyForce(forceVector);
    }

    public void SetWeaponDamage(int damage) => attackDamage = damage;
    public void SetWeaponForce(float multiplier) => forceMultiplier = multiplier;
    #endregion
}
