using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;
    public PlayerStamina playerStamina;

    [Header("Health UI")]
    public Slider healthBar;
    public Image healthFill;
    public Color healthyColor = new Color(0.2f, 0.8f, 0.2f);
    public Color woundedColor = new Color(0.8f, 0.4f, 0.1f);
    public Color criticalColor = new Color(0.8f, 0.1f, 0.1f);

    [Header("Stamina UI")]
    public Slider staminaBar;
    public Image staminaFill;

    [Header("Radiation UI")]
    public Slider radiationBar;
    public GameObject radiationPanel;

    void Update()
    {
        if (playerHealth != null)
        {
            float healthPercent = playerHealth.currentHealth / playerHealth.maxHealth;
            if (healthBar != null) healthBar.value = healthPercent;

            if (healthFill != null)
            {
                if (healthPercent > 0.6f)
                    healthFill.color = healthyColor;
                else if (healthPercent > 0.3f)
                    healthFill.color = woundedColor;
                else
                    healthFill.color = criticalColor;
            }

            if (radiationBar != null)
                radiationBar.value = playerHealth.radiationLevel;

            if (radiationPanel != null)
                radiationPanel.SetActive(playerHealth.radiationLevel > 0.05f);
        }

        if (playerStamina != null)
        {
            if (staminaBar != null)
                staminaBar.value = playerStamina.GetStaminaPercent();
        }
    }
}