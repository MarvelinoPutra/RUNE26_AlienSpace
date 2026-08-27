using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObstacleCollision : MonoBehaviour
{
    private void Awake()
    {
        // Automatically set the collider to Trigger mode
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ObstacleManager.Instance?.TriggerGameOver();
        }
    }
}