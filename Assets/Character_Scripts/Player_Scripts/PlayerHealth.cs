using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    #region Private Fields
    [SerializeField] private UnityEngine.UI.Slider healthBar;
    [SerializeField] private UnityEngine.UI.Image redHitScreen;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damagedDelay = 0.75f;
    private bool canBeDamaged;
    private bool dead;
    #endregion

    #region MonoBehaviour Callbacks
    private void Update()
    {
        DecreaseHitColour();
    }
    #endregion

    #region Private Methods
    private IEnumerator DamageTimer()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(damagedDelay);
        canBeDamaged = true;
    }

    private void DecreaseHitColour()
    {
        if (redHitScreen.color.a > 0)
        {
            var hitColour = redHitScreen.color;
            hitColour.a -= 0.01f;
            redHitScreen.color = hitColour;
        }
    }
    #endregion

    #region IDamageable Implementation
    public void ApplyDamage(int damage)
    {
        if (!canBeDamaged || dead) { return; }

        StartCoroutine(DamageTimer());
        currentHealth -= damage;
        healthBar.value = (float)currentHealth / (float)maxHealth;

        playerAudioSource.Play();
        var hitColour = redHitScreen.color;
        hitColour.a = 0.8f;
        redHitScreen.color = hitColour;

        if (currentHealth <= 0)
        {
            dead = true;
            GetComponent<PlayerController>().Death();
            GameManager.Instance.GetCharactersManager().DeathSignal(this);
            
        }
        
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.value = 1;
        canBeDamaged = true;
    }

    public bool Living()
    {
        if (currentHealth > 0) { return true; }
        else { return false; }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    #endregion

    #region Public Methods
    public void Initialise()
    {
        FindObjectOfType<GameManager>().GetCharactersManager().AddPlayerDamageable(this);
        ResetHealth();
    }
    #endregion
}
