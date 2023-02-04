using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType 
{ 
    none, //0
    left, //1
    right //2
}

public class AttackManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private bool weaponEnabled;
    private IDamageDealer attackApplicationComponent;
    private List<Collider> collidersHitThisAttack;
    private int attackDirection; //0 = No direction, 1 = Left Swing, 2 = Right Swing
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!weaponEnabled) { return; }
        if(collidersHitThisAttack.Contains(other)) { return; }

        if (other.CompareTag("Enemy"))
        {
            attackApplicationComponent.AddDazed(other.GetComponent<ICharacterController>());
            attackApplicationComponent.AddDamage(other.GetComponent<IDamageable>());
            attackApplicationComponent.AddForce(other.GetComponent<IStrikeable>(), attackDirection);
        }
        else if (other.CompareTag("Player"))
        {
            attackApplicationComponent.AddDamage(other.GetComponent<IDamageable>());
        }
        else if (other.CompareTag("Objective"))
        {
            attackApplicationComponent.AddDamage(other.GetComponent<IDamageable>());
        }
        collidersHitThisAttack.Add(other);
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        weaponEnabled = false;
        weaponCollider.enabled = false;
    }
    #endregion

    #region Public Methods
    public bool GetWeaponActive() { return weaponEnabled; }
    public void SetWeaponActive(bool active)
    {
        if (weaponEnabled != active)
        {
            weaponEnabled = active;
            weaponCollider.enabled = active;
            collidersHitThisAttack = new List<Collider>();
        }
    }
    public void SetAttackDirection(int dir) => attackDirection = dir;
    public void SetAttackApplicationComponent(IDamageDealer component) => attackApplicationComponent = component;
    #endregion

}
