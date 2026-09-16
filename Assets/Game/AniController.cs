using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))] // Use this when you use GetComponent in a script
public class AniController : MonoBehaviour
{
    // Using rename can change variable name across the entire project
    readonly private int AniSpeedKey = Animator.StringToHash("Speed"); // Use to create reference to animator variable
    readonly private int AniInjuryKey = Animator.StringToHash("Injury");

    private Animator animator;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls(); // create new instance of the Input Action Asset C# class
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Player.Enable(); // Enable input action
    }

    private void OnDisable()
    {
        controls.Player.Disable(); // Disable input action
    }

    private void Update()
    {
        float move = controls.Player.Move.ReadValue<Vector2>().x;
        transform.position += new Vector3(move, 0, 0) * Time.deltaTime;

        animator.SetFloat(AniSpeedKey, move);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            animator.SetFloat(AniInjuryKey, 1f);
        }
        if(animator.GetFloat(AniInjuryKey) > 0)
        {
            float newVal = Mathf.MoveTowards(animator.GetFloat(AniInjuryKey), 0, 0.5f * Time.deltaTime);
            animator.SetFloat(AniInjuryKey, newVal);
        }
            
    }
}
