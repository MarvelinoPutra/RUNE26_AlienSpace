using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }

    [Header("Global Speed Settings")]
    public float globalSpeed = 5f;

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
        if (PowerUpManager.Instance != null)
        {
            // Jika punya perisai / booster, abaikan tabrakan SEPENUHNYA.
            // Tidak ada Game Over, dan TIDAK ADA LOG yang muncul.
            if (PowerUpManager.Instance.isDefenseActive || PowerUpManager.Instance.isBoosterActive)
            {
                return; // Langsung keluar dari fungsi
            }

            // FIX BUG: Hentikan semua timer PowerUp agar Time.timeScale 
            // tidak ke-reset kembali ke 1.0 beberapa detik setelah mati.
            PowerUpManager.Instance.CancelPowerUpsOnDeath();
        }

        // Eksekusi mutlak Game Over
        Time.timeScale = 0f;
        Debug.Log("[ObstacleManager] Player hit an obstacle! Game Over.");
    }

    public float GetCurrentSpeed()
    {
        return globalSpeed;
    }
}