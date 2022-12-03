using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private bool weaponEnabled;
    #endregion

    #region Private Fields
    private IDamageDealer attackApplicationComponent;
    private Vector3 leftUpVector = new Vector3(-2, 1, 0);
    private Vector3 RightUpVector = new Vector3(2, 1, 0);
    private float weaponForceMultiplier;
    private int weaponDamage;
    private int attackDirection; //0 = No direction, 1 = Left Swing, 2 = Right Swing
    #endregion
    
    private void Start()
    {
        weaponEnabled = false;
        weaponCollider.enabled = false;
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
                //Rigidbody enemyRigid = other.GetComponent<Rigidbody>();
                //Vector3 direction;
                //switch (attackDirection)
                //{
                //    case 0:
                //        break;
                //    case 1:
                //        direction = -transform.right;
                //        direction.x *= weaponForceMultiplier;
                //        direction.y *= weaponForceMultiplier / 2;
                //        enemyRigid.AddForce(direction, ForceMode.Impulse);
                //        break;
                //    case 2:
                //        direction = transform.right;
                //        direction.x *= weaponForceMultiplier;
                //        direction.y *= weaponForceMultiplier / 2;
                //        enemyRigid.AddForce(direction, ForceMode.Impulse);
                //        break;
                //}
                //enemyRigid.AddForce(Vector3.up * weaponForceMultiplier, ForceMode.Impulse);



            }
            else if (other.tag == "Player" && gameObject != other.gameObject)
            {
                other.GetComponent<PlayerHealth>().ApplyDamage(weaponDamage);
            }

        }
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
