using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] UnityEngine.UI.Scrollbar healthBar;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damagedDelay = 0.75f;
    private float damageTimer;

    public void ApplyDamage(int damage)
    {
        if(damageTimer <= 0)
        {
            currentHealth -= damage;
            healthBar.size = (float)currentHealth / (float)maxHealth;
            damageTimer = damagedDelay;
            Debug.Log("Player Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                GameManager.Instance.DeathSignal(this);
            }
        }
        
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.size = 1;
    }

    private void Update()
    {
        damageTimer -= Time.deltaTime;
    }

    public void ResetHealth()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        healthBar.size = 1;
        damageTimer = damagedDelay;
    }
}
