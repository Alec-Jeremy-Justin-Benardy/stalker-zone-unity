using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Bleed")]
    public float bleedRate = 0f;
    public float bleedDamagePerSecond = 2f;

    [Header("Radiation")]
    public float radiationLevel = 0f;
    public float radiationDamageThreshold = 0.3f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (bleedRate > 0)
        {
            ApplyDamage(bleedDamagePerSecond * bleedRate * Time.deltaTime);
            bleedRate = Mathf.Max(0, bleedRate - 0.01f * Time.deltaTime);
        }

        if (radiationLevel > radiationDamageThreshold)
            ApplyDamage(radiationLevel * 2f * Time.deltaTime);

        radiationLevel = Mathf.Max(0, radiationLevel - 0.0001f * Time.deltaTime);
    }

    public void ApplyDamage(float amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        if (currentHealth <= 0) Die();
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}