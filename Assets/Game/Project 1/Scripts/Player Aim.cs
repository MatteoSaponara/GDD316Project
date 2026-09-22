using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float aimRange = 100f;

    private void Update()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Debug.DrawRay(ray.origin, ray.direction * aimRange, Color.red);

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
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            AimTarget target = hit.collider.GetComponent<AimTarget>();

            if (target != null)
            {
                Debug.Log("Target hit should teleport");
                target.Hit();
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
    }
}