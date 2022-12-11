using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private UnityEngine.UI.Scrollbar healthBar;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damagedDelay = 0.75f;
    private bool canBeDamaged;

    private void Awake()
    {
        FindObjectOfType<GameManager>().GetCharactersManager().AddPlayerDamageable(this);
    }

    private void Start()
    {
        ResetHealth();
    }

    private IEnumerator DamageTimer()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(damagedDelay);
        canBeDamaged = true;
    }

    public void ApplyDamage(int damage)
    {
        if (canBeDamaged)
        {
            StartCoroutine(DamageTimer());
            currentHealth -= damage;
            healthBar.size = (float)currentHealth / (float)maxHealth;
            Debug.Log("Player Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                GameManager.Instance.GetCharactersManager().DeathSignal(this);
            }
        }

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.size = 1;
        canBeDamaged = true;
    }

}
