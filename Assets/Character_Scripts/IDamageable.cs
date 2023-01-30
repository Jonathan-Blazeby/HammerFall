using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(int damage);
    public void ResetHealth();

    public GameObject GetGameObject();
}
