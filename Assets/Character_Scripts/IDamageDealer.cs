using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    public void SetWeaponDamage(int damage);
    public void SetWeaponForce(float multiplier);
    public void AddDazed(ICharacterController recipient);
    public void AddDamage(IDamageable recipient);
    public void AddForce(IStrikeable recipient, int directionRelativeToSelf); //0 = No direction, 1 = Leftward, 2 = Rightward
}
