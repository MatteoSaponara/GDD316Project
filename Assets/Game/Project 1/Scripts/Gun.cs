using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Tooltip("Animation curve of the recoil animation")]
    [SerializeField] private AnimationCurve curve; // Animation curve
    [Tooltip("Speed of recoil animation")]
    [SerializeField] private float aniSpeed; // Speed of animation
    [Tooltip("Recoil multiplier.")]
    [SerializeField] private float recoilAmount = 0.1f;

    [Tooltip("Position of Right Hand Constraint target.")]
    [SerializeField] private Transform rightHandGrip;
    [Tooltip("Position of Left Hand Constraint target.")]
    [SerializeField] private Transform leftHandGrip;

    private float aniTime = 1;

    private Vector3 startingPosition; // Starting position of gun parent
    private Vector3 rightGripStartingPosition; // Position of Right Grip target
    private Vector3 leftGripStartingPosition; // Position of Left Grip target

    // Gets starting positions
    private void Start()
    {
        startingPosition = transform.localPosition;

        rightGripStartingPosition = rightHandGrip.localPosition;
        leftGripStartingPosition = leftHandGrip.localPosition;
    }

    private void Update()
    {
        aniTime += Time.deltaTime * aniSpeed;

        // Amount the gun is offset from recoil
        Vector3 recoilOffset =
            new Vector3(curve.Evaluate(aniTime) * recoilAmount, 0, 0);

        transform.localPosition =
            Vector3.Lerp(
                transform.localPosition,
                startingPosition + recoilOffset,
                Time.deltaTime * 10f * aniSpeed);

        rightHandGrip.localPosition =
            rightGripStartingPosition + recoilOffset;

        leftHandGrip.localPosition =
            leftGripStartingPosition + recoilOffset;

        // Gets mouse input to intiate recoil
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            aniTime = 0f;
        }
    }
}