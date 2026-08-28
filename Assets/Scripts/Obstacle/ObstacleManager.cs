using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void TriggerGameOver()
    {
        Time.timeScale = 0f;
        Debug.Log("[ObstacleManager] Player hit an obstacle! Game Over.");
    }

    [Header("Global Speed Settings")]
    public float globalSpeed = 5f;

    public float GetCurrentSpeed()
    {
        return globalSpeed;
    }
}