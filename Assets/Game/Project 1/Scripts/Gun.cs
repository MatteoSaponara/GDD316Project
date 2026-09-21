using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Tooltip("Animation curve of the recoil animation")]
    [SerializeField] private AnimationCurve curve; // Animation curve
    [Tooltip("Speed of recoil animation")]
    [SerializeField] private float aniSpeed; // Speed of animation
    [Tooltip("Recoil multiplier.")]
    [SerializeField] private float recoilAmount = 0.025f;
    [Tooltip("Upward rotation of recoil.")]
    [SerializeField] private float recoilRotation = 10f;

    [Tooltip("Position of Right Hand Constraint target.")]
    [SerializeField] private Transform rightHandGrip;
    [Tooltip("Position of Left Hand Constraint target.")]
    [SerializeField] private Transform leftHandGrip;
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;

    private float aniTime = 1;

    private Vector3 startingPosition; // Starting position of gun parent
    private Quaternion startingRotation; // Starting rotation of gun parent

    // Gets starting positions
    private void Start()
    {
        startingPosition = transform.localPosition;
        startingRotation = transform.localRotation;

    }

    private void Update()
    {
        rightHandGrip.SetPositionAndRotation(
            rightHandTarget.position,
            rightHandTarget.rotation
            );

        leftHandGrip.SetPositionAndRotation(
            leftHandTarget.position,
            leftHandTarget.rotation
        );

        aniTime += Time.deltaTime * aniSpeed;

        // Amount the gun is offset from recoil
        Vector3 recoilOffset =
            new Vector3(curve.Evaluate(aniTime) * recoilAmount, 0, 0);
        Quaternion recoilRotationOffset =
            Quaternion.Euler(0, 0, curve.Evaluate(aniTime) * recoilRotation);

        transform.localPosition =
            Vector3.Lerp(
                transform.localPosition,
                startingPosition + recoilOffset,
                Time.deltaTime * 10f * aniSpeed);

        transform.localRotation = 
            Quaternion.Lerp(
                transform.localRotation,
                startingRotation * recoilRotationOffset,
                Time.deltaTime * 10f * aniSpeed);


        // Gets mouse input to intiate recoil
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            aniTime = 0f;
        }
    }
}