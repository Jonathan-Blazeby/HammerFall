using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;
    private int weaponDamage;
    private bool weaponEnabled;

    private void Start()
    {
        weaponEnabled = false;
    }

    public void ActivateWeapon(int damage)
    {
        weaponEnabled = true;
        weaponDamage = damage;
    }

    public void DeactivateWeapon()
    {
        weaponEnabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!weaponEnabled) { return; }

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().ApplyDamage(weaponDamage);
        }
    }

}
