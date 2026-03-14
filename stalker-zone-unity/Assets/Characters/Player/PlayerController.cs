using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3.5f;
    public float sprintSpeed = 6.5f;
    public float crouchSpeed = 1.8f;
    public float gravity = -19.62f;

    [Header("Look")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 85f;

    private CharacterController _controller;
    private Camera _camera;
    private Vector3 _velocity;
    private float _xRotation = 0f;
    private bool _isSprinting;
    private bool _isCrouching;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
        _isCrouching = Input.GetKey(KeyCode.LeftControl);

        float speed = _isSprinting ? sprintSpeed :
                      _isCrouching ? crouchSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * speed * Time.deltaTime);

        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -maxLookAngle, maxLookAngle);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}