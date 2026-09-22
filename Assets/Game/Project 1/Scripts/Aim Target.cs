using TMPro;
using UnityEngine;

public class AimTarget : MonoBehaviour
{
    [Header("Target Movement")]
    [SerializeField] private Transform movementArea;
    [SerializeField] private Vector3 areaSize = new Vector3(10f, 5f, 10f);

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI counterText;

    private int hitCount = 0;

    private void Start()
    {
        UpdateCounter();
    }

    public void Hit()
    {
        hitCount++;
        UpdateCounter();
        MoveToRandomPosition();
    }

    private void MoveToRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
            Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
        );

        transform.position = movementArea.position + randomPosition;
    }

    private void UpdateCounter()
    {
        counterText.text = "Hits: " + hitCount;
    }

    private void OnDrawGizmosSelected()
    {
        if (movementArea == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(movementArea.position, areaSize);
    }
}
