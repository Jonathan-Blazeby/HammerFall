using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(int damage);
    public void ResetHealth();
    public bool Living();
    public bool CompareTag(string tag);
    public int GetMaxHealth();
}
