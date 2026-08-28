using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Score Settings")]
    [SerializeField] private float scoreMultiplier = 10f;
    private float currentScore = 0f;
    private float highscore = 0f;
    
    private bool isPlaying = true;

    void Start()
    {
        // Ambil data high score yang tersimpan sebelumnya
        highscore = PlayerPrefs.GetFloat("Highscore", 0f);
        UpdateUI();

        // Sembunyikan panel game over saat awal game
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlaying)
        {
            // Tambah skor seiring waktu berjalan
            currentScore += scoreMultiplier * Time.deltaTime;
            UpdateUI();

            // Simpan high score jika skor saat ini lebih tinggi
            if (currentScore > highscore)
            {
                highscore = currentScore;
                PlayerPrefs.SetFloat("Highscore", highscore);
                PlayerPrefs.Save();
            }
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

        // Munculkan panel kalah
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Hentikan waktu permainan
        Time.timeScale = 0f;
    }

    // Fungsi ini dihubungkan ke tombol "Restart" di UI
    public void RestartGame()
    {
        // Kembalikan waktu normal sebelum merestart scene
        Time.timeScale = 1f;
        
        // Memuat ulang scene yang sedang aktif
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}