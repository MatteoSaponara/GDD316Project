using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Generate integers by hashing strings
    private static readonly int aniMoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int aniInjury = Animator.StringToHash("Injury");
    private static readonly int aniIsCarrying = Animator.StringToHash("IsCarrying");

    [Header("Animation")]
    [SerializeField] private Animator animator;     // Animator reference

    [Header("Movement")]
	[SerializeField] private float walkSpeed;       // Walking speed
    [SerializeField] private float runSpeed;        // Running speed (max speed)
    [SerializeField] private float acceleration;    // Control rate of change of speed
    private float velocity;

    [Header("Input")]
    private PlayerControls controls;                // Input Action Asset C# class reference

    [Header("Equipment")]
    private bool isCarrying;                        // Keep track of carry state on this component (separate from animator)

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
        float move = controls.Player.Move.ReadValue<Vector2>().x;       // Read input
        bool sprint = controls.Player.Sprint.IsPressed();               // Check if sprint button is held
        move *= (sprint ? runSpeed : walkSpeed);                        // Multiply input by the appropriate speed

        // Move
        velocity = Mathf.MoveTowards(velocity, move, acceleration * Time.deltaTime);    // Smoothly change velocity (linearly. use lerped for fast to slow)
        transform.position += new Vector3(0f, 0f, velocity * Time.deltaTime);  // Move

        // Move speed animation
        animator.SetFloat(aniMoveSpeed, velocity / runSpeed); //Send speed as percetage of max for blendtree

        // Injury animation
        if (controls.Player.Jump.IsPressed())
        {
            animator.SetFloat(aniInjury, 1.0f);
        }
        else
        {
            animator.SetFloat(aniInjury, 0f);
        }

        // Check collisions
        if (!isCarrying)
        {
            Collider[] col = Physics.OverlapSphere(transform.position, 0.5f); // Array of objects the player overlap sphere has collided with
            if (col.Length > 0) // Checks if the player has colllided with anyt
            {
                isCarrying = true; // Sets isCarrying to true
                col[0].gameObject.SetActive(false); // Sets picked up object as inactive
                animator.SetBool(aniIsCarrying, true); // Initiates masked carrying animation
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
