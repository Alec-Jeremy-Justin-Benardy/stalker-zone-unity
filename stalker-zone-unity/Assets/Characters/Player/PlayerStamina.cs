using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 15f;
    public float staminaRegenRate = 10f;
    public float staminaRegenDelay = 2f;
    public float sprintRequiredStamina = 20f;

    private PlayerController _controller;
    private float _regenTimer;
    private bool _isExhausted;
    private float _originalWalkSpeed;
    private float _originalSprintSpeed;

    void Start()
    {
        currentStamina = maxStamina;
        _controller = GetComponent<PlayerController>();
        _originalWalkSpeed = _controller.walkSpeed;
        _originalSprintSpeed = _controller.sprintSpeed;
    }

    void Update()
    {
        // Can only START sprinting if stamina above required threshold
        // Can only CONTINUE sprinting if not exhausted
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift);
        bool canSprint = !_isExhausted && currentStamina > 0;
        bool isSprinting = wantsToSprint && canSprint;

        if (isSprinting)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            _regenTimer = 0f;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                _isExhausted = true;
                // Force stop sprinting immediately
                _controller.sprintSpeed = _controller.walkSpeed;
            }
        }
        else
        {
            _regenTimer += Time.deltaTime;

            if (_regenTimer >= staminaRegenDelay)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Min(currentStamina, maxStamina);
            }

            // Only recover from exhaustion once stamina hits required threshold
            if (_isExhausted && currentStamina >= sprintRequiredStamina)
            {
                _isExhausted = false;
                _controller.sprintSpeed = _originalSprintSpeed;
            }
        }

        // Slow walk when exhausted
        if (_controller != null)
        {
            _controller.walkSpeed = _isExhausted
                ? _originalWalkSpeed * 0.6f
                : _originalWalkSpeed;
        }
    }

    public float GetStaminaPercent()
    {
        return currentStamina / maxStamina;
    }
}