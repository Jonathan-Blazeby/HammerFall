using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    #region Private Fields
    [SerializeField] private UnityEngine.UI.Scrollbar healthBar;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damagedDelay = 0.75f;
    private bool canBeDamaged;
    private bool dead;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        FindObjectOfType<GameManager>().GetCharactersManager().AddNewDamageable(this);
    }

    private void Start()
    {
        ResetHealth();
    }

    private void Update()
    {
        healthBar.transform.forward = Camera.main.transform.forward;
    }
    #endregion

    #region Private Methods
    private IEnumerator DamageTimer()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(damagedDelay);
        canBeDamaged = true;
    }
    #endregion

    #region IDamageable Implementation
    public void ApplyDamage(int damage)
    {
        if(!canBeDamaged || dead) { return; }

        StartCoroutine(DamageTimer());
        currentHealth -= damage;
        healthBar.size = (float)currentHealth / (float)maxHealth;

        //Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            dead = true;
            healthBar.gameObject.SetActive(false);
            GameManager.Instance.GetCharactersManager().DeathSignal(this);
            GetComponent<EnemyController>().Death();
        }
        
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.gameObject.SetActive(true);
        healthBar.size = 1;
        canBeDamaged = true;
        dead = false;
    }
    #endregion

}
