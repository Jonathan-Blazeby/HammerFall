using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHealth : MonoBehaviour, IDamageable
{
    #region Private Fields
    [SerializeField] private UnityEngine.UI.Slider healthBar;
    private AudioSource objectiveAudioSource;
    [SerializeField] private List<AudioClip> objectiveHitAudioClips;
    [SerializeField] private AudioClip objectiveDestroyedAudioClip;
    [SerializeField] private ParticleSystem deathParticleBurst;
    [SerializeField] private ParticleSystem constantParticles;
    [SerializeField] private MeshRenderer objectiveCoreRenderer;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damagedDelay = 0.75f;
    private bool canBeDamaged;
    private bool dead;
    #endregion

    #region Monobehavior Callbacks
    private void Awake()
    {
        FindObjectOfType<GameManager>().GetCharactersManager().AddNewDamageable(this);
    }

    private void Start()
    {
        ResetHealth();
        objectiveAudioSource = GetComponent<AudioSource>();
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
        if (!canBeDamaged || dead) { return; }

        StartCoroutine(DamageTimer());
        currentHealth -= damage;
        healthBar.value = (float)currentHealth / (float)maxHealth;


        if (currentHealth <= 0)
        {
            dead = true;
            objectiveAudioSource.clip = objectiveDestroyedAudioClip;
            objectiveAudioSource.Play();
            healthBar.gameObject.SetActive(false);
            constantParticles.Stop();
            objectiveCoreRenderer.enabled = false;
            deathParticleBurst.Play();
            GameManager.Instance.GetCharactersManager().DeathSignal(this);
            return;
        }

        objectiveAudioSource.clip = objectiveHitAudioClips[Random.Range(0, objectiveHitAudioClips.Count)];
        objectiveAudioSource.Play();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.gameObject.SetActive(true);
        healthBar.value = 1;
        constantParticles.Play();
        objectiveCoreRenderer.enabled = true;
        canBeDamaged = true;
        dead = false;
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
}
