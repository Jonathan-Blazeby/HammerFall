using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] UnityEngine.UI.Scrollbar healthBar;
    [SerializeField] private int startHealth;
    [SerializeField] private int currentHealth;
    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.size = (float)currentHealth / (float)startHealth;
        Debug.Log("Player Health: " + currentHealth);
    }

    private void Start()
    {
        currentHealth = startHealth;
        healthBar.size = 1;
    }

    private void Update()
    {
        healthBar.transform.forward = Camera.main.transform.forward;
    }
}
