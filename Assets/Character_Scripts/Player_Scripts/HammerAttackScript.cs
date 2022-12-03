using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttackScript : MonoBehaviour, IDamageDealer
{
    private float forceMultiplier;
    private int attackDamage;

    public void AddDazed(ICharacterController recipient)
    {
        recipient.Daze();
    }

    public void AddDamage(IDamageable recipient)
    {
        recipient.ApplyDamage(attackDamage);
    }

    public void AddForce(IStrikeable recipient, int directionRelativeToSelf) //0 = No direction, 1 = Left Swing, 2 = Right Swing
    {
        Vector3 forceVector = Vector3.zero;
        switch (directionRelativeToSelf)
        {
            case 0:
                forceVector = transform.root.forward;
                break;
            case 1:
                forceVector = -transform.root.right;
                break;
            case 2:
                forceVector = transform.root.right;
                break;
        }
        forceVector *= forceMultiplier;
        Debug.Log("Direction: " + directionRelativeToSelf + " Force to Enemy: " + forceVector.ToString());
        recipient.ApplyForce(forceVector);
    }

    public void SetWeaponDamage(int damage)
    {
        attackDamage = damage;
    }

    public void SetWeaponForce(float multiplier)
    {
        forceMultiplier = multiplier;
    }
}
