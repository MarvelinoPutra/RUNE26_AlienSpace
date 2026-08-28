using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void Awake()
    {
        // Tetapkan isTrigger jika collider sudah ada
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ObstacleManager.Instance?.TriggerGameOver();
        }
    }
}