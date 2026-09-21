using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float aimRange = 100f;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Aim();
        }
    }

    private void Aim()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, aimRange))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
}
