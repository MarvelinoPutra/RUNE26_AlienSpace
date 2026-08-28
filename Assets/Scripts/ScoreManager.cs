using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;

    [Header("Score Settings")]
    [SerializeField] private float scoreMultiplier = 10f;
    private float currentScore = 0f;
    private float highscore = 0f;
    private bool isPlaying = true;

    void Start()
    {
        // Ambil data High Score yang tersimpan di memori perangkat
        highscore = PlayerPrefs.GetFloat("Highscore", 0f);
        UpdateUI();
    }

    void Update()
    {
        if (isPlaying)
        {
            // Skor bertambah seiring waktu game berjalan
            currentScore += scoreMultiplier * Time.deltaTime;
            UpdateUI();

            // Cek dan simpan jika ada High Score baru
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

    // Dipanggil saat game over agar skor berhenti bertambah
    public void StopScore()
    {
        isPlaying = false;
    }
}