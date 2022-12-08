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
    #region Serialized Fields
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private bool weaponEnabled;
    #endregion

    #region Private Fields
    private IDamageDealer attackApplicationComponent;
    private int attackDirection; //0 = No direction, 1 = Left Swing, 2 = Right Swing
    #endregion
    
    private void Start()
    {
        Initialise();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(weaponEnabled) 
        {
            if (other.tag == "Enemy" && gameObject != other.gameObject)
            {
                attackApplicationComponent.AddDazed(other.GetComponent<ICharacterController>());
                attackApplicationComponent.AddDamage(other.GetComponent<IDamageable>());
                attackApplicationComponent.AddForce(other.GetComponent<IStrikeable>(), attackDirection);
            }
            else if (other.tag == "Player" && gameObject != other.gameObject)
            {
                attackApplicationComponent.AddDamage(other.GetComponent<IDamageable>());
            }
        }
    }

    private void Initialise()
    {
        weaponEnabled = false;
        weaponCollider.enabled = false;
    }

    public bool GetWeaponActive() { return weaponEnabled; }
    public void SetWeaponActive(bool active) 
    { 
        if(weaponEnabled != active)
        {
            weaponEnabled = active;
            weaponCollider.enabled = active;
        }
    }
    public void SetAttackDirection(int dir) { attackDirection = dir; }
    public void SetAttackApplicationComponent(IDamageDealer component) { attackApplicationComponent = component; }

}
