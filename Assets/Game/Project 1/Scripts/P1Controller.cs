using UnityEngine;

public class P1Controller : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;     // Animator reference

    [Header("Movement")]
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float acceleration;    // Control rate of change of speed
    private float velocity;


    [Header("Input")]
    private PlayerControls controls;                // Input Action Asset C# class reference

    

    private void Awake()
    {
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

    private void Update()
    {
        // Get move speed
        //float move = controls.Player.Move.ReadValue().x;       // Read input
        //move *= playerSpeed;

        // Move
       // velocity = Mathf.MoveTowards(velocity, move, acceleration * Time.deltaTime);    // Smoothly change velocity
    }

}