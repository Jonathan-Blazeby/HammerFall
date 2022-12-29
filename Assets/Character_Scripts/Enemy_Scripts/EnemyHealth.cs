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
        if (canBeDamaged)
        {
            StartCoroutine(DamageTimer());
            currentHealth -= damage;
            healthBar.size = (float)currentHealth / (float)maxHealth;

            Debug.Log("Enemy Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                GameManager.Instance.GetCharactersManager().DeathSignal(this);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.size = 1;
        canBeDamaged = true;
    }
    #endregion

}
