using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }

    // Variabel GameOver dihapus.
    public GameObject ScoreSystem, Powerup, ObstacleSpawner, PowerUpSpawner;

    private float globalSpeed = 5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Time.timeScale = 1f;
    }

    public void TriggerGameOver()
    {
        if (PowerUpManager.Instance != null)
        {
            if (PowerUpManager.Instance.isDefenseActive || PowerUpManager.Instance.isBoosterActive)
            {
                return;
            }
            PowerUpManager.Instance.CancelPowerUpsOnDeath();
        }

        Time.timeScale = 0f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }

        if (ScoreSystem != null) ScoreSystem.SetActive(false);
        if (Powerup != null) Powerup.SetActive(false);
        if (ObstacleSpawner != null) ObstacleSpawner.SetActive(false);
        if (PowerUpSpawner != null) PowerUpSpawner.SetActive(false);
    }

    public float GetCurrentSpeed()
    {
        return globalSpeed;
    }
}