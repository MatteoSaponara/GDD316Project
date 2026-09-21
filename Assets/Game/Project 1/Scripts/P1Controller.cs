using UnityEngine;
using UnityEngine.Animations.Rigging;

public class P1Controller : MonoBehaviour
{
    private static readonly int aniMoveX = Animator.StringToHash("MoveX");
    private static readonly int aniMoveY = Animator.StringToHash("MoveY");

    [Header("Animation")]
    [SerializeField] private Animator animator;     // Animator reference

    [Header("Movement Settings")]
    [Tooltip("Walking speed of the player.")]
    [SerializeField] private float moveSpeed = 5f;
    [Tooltip("Force of gravity on the player.")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Look Settings")]
    [Tooltip("Camera pivot for the player camera.")]
    [SerializeField] private Transform cameraPivot;
    [Tooltip("Target that the player's arms will aim toward.")]
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float lookSensitivity = 15f;
    [Tooltip("How far the player can move the camera up in degrees.")]
    [SerializeField] private float topLookLimit = -80f;
    [Tooltip("How far the player can move the camera down in degrees.")]
    [SerializeField] private float bottomLookLimit = 80f;
    [SerializeField] private Transform rightHandAimPivot;
    [SerializeField] private RigBuilder rigBuilder;


    [Header("Input")]
    private CharacterController characterController;
    private PlayerControls controls;                // Input Action Asset C# class reference

    // Input values
    private Vector2 movementInput;
    private Vector2 lookInput;

    // Calculation state
    private float verticalRotation = 0f;
    private Vector3 currentMovement;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        controls = new PlayerControls();            // create new instance of the Input Action Asset C# class
    }

    private void OnEnable()
    {
        controls.Player.Enable();                   // Enable input action
    }

    private void OnDisable()
    {
        controls.Player.Disable();                  // Disable input action
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks cursor to center of the screen
        Cursor.visible = false; // Turns of cursor visability
    }

    private void Update()
    {
        movementInput = controls.Player.Move.ReadValue<Vector2>();
        lookInput = controls.Player.Look.ReadValue<Vector2>();

        HandleRotation();
        HandleMovement();

        rigBuilder.SyncLayers();
    }

    private void HandleMovement()
    {
        // Ground movement direction calculations
        Vector3 forwardMovement = transform.forward * movementInput.y;
        Vector3 rightMovement = transform.right * movementInput.x;

        // Combine directions and apply speed
        currentMovement = (forwardMovement + rightMovement) * moveSpeed;

        // Gravity application
        if (characterController.isGrounded && currentMovement.y < 0)
        {
            currentMovement.y = -2f; // Force to keep player grounded
        }
        else
        {
            currentMovement.y += gravity * Time.deltaTime;
        }

        // Apply the calculation to character controller
        characterController.Move(currentMovement * Time.deltaTime);

        // Move speed animation
        animator.SetFloat(aniMoveX, movementInput.x);
        animator.SetFloat(aniMoveY, movementInput.y);
    }

    private void HandleRotation()
    {
        // Vertical Rotation (looking up and down)
        verticalRotation -= lookInput.y * lookSensitivity * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, topLookLimit, bottomLookLimit);

        cameraPivot.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        rightHandAimPivot.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);

        // Horizontal Rotation (looking left and right)
        float horizontalRotation = lookInput.x * lookSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * horizontalRotation);
    }

}