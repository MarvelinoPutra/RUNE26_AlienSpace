using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton; // Tambahan untuk tombol Home

    [Header("Score Settings")]
    [SerializeField] private float scoreMultiplier = 10f;
    private float currentScore = 0f;
    private float highscore = 0f;

    private bool isPlaying = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        highscore = PlayerPrefs.GetFloat("Highscore", 0f);
        UpdateUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // KODE SAKTI ANTI-MISSING: Tombol Restart
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }

        // KODE SAKTI ANTI-MISSING: Tombol Home
        if (homeButton != null)
        {
            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(GoToHome);
        }
    }

    void Update()
    {
        if (isPlaying)
        {
            currentScore += scoreMultiplier * Time.deltaTime;

            if (currentScore > highscore)
            {
                highscore = currentScore;
            }
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(currentScore).ToString();

        if (highscoreText != null)
            highscoreText.text = "High Score: " + Mathf.FloorToInt(highscore).ToString();
    }

    public void GameOver()
    {
        isPlaying = false;
        SaveHighscoreData();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fungsi baru untuk tombol Home
    public void GoToHome()
    {
        Time.timeScale = 1f; // Sangat penting agar menu utama tidak ikutan freeze
        SceneManager.LoadSceneAsync(0); // Load scene Home (index 1)
    }

    private void OnDestroy()
    {
        SaveHighscoreData();
    }

    private void SaveHighscoreData()
    {
        if (currentScore > PlayerPrefs.GetFloat("Highscore", 0f))
        {
            PlayerPrefs.SetFloat("Highscore", currentScore);
            PlayerPrefs.Save();
        }
    }
}