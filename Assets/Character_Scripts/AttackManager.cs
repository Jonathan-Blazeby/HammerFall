using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [SerializeField] private Collider weaponCollider;
    private Vector3 leftUpVector = new Vector3(-2, 1, 0);
    private Vector3 RightUpVector = new Vector3(2, 1, 0);
    private float weaponForceMultiplier;
    private int weaponDamage;
    private int attackDirection; //0 = No direction, 1 = Left Swing, 2 = Right Swing
    [SerializeField] private bool weaponEnabled;

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
                other.GetComponent<EnemyHealth>().ApplyDamage(weaponDamage);
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
    public void SetWeaponDamage(int damage) { weaponDamage = damage; }
    public void SetWeaponForce(float forceMultiplier) { weaponForceMultiplier = forceMultiplier; }

}
