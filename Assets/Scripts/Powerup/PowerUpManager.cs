using System.Collections;
using UnityEngine;
using TMPro; 

public class PowerUpManager : MonoBehaviour
{[Header("UI Text Notification")]
    public TextMeshProUGUI statusText; 

    [Header("Visual Perisai (Shield)")]
    public GameObject shieldVisualObject; 

    [Header("Durasi Sesuai Desain (Detik)")]
    public float defenseDuration = 5f;
    public float slowDuration = 10f;
    public float boosterDuration = 3f;
    public float fasterDuration = 5f;
    public float freezeDuration = 3f;

    [Header("Referensi Kontrol Player & Kecepatan")]
    public float baseMoveSpeed = 5f; // Kecepatan normal player
    private DummyPlayerController playerController;

    void Start()
    {
        if (statusText != null) statusText.text = "";
        
        if (shieldVisualObject != null)
        {
            shieldVisualObject.SetActive(false);
        }

        // Ambil komponen penggerak player secara otomatis
        playerController = GetComponent<DummyPlayerController>();
        if (playerController != null)
        {
            baseMoveSpeed = playerController.moveSpeed;
        }
    }

    public void TriggerPowerUp(string effectName)
    {
        // Hentikan efek sebelumnya agar tidak tumpang tindih
        StopAllCoroutines(); 

        switch (effectName)
        {
            case "Defense":
                StartCoroutine(DefenseRoutine());
                break;
            case "Slow":
                StartCoroutine(SlowRoutine());
                break;
            case "Booster":
                StartCoroutine(BoosterRoutine());
                break;
            case "Faster":
                StartCoroutine(FasterRoutine());
                break;
            case "Freeze":
                StartCoroutine(FreezeRoutine());
                break;
        }
    }

    // 1. DEFENSE: Kebal / ada perisai
    IEnumerator DefenseRoutine()
    {
        if (statusText != null) statusText.text = "Defense Shield Active!";
        if (shieldVisualObject != null) shieldVisualObject.SetActive(true);
        
        Debug.Log("Defense Aktif: Player kebal & ada perisai.");
        yield return new WaitForSecondsRealtime(defenseDuration);

        if (shieldVisualObject != null) shieldVisualObject.SetActive(false);
        if (statusText != null) statusText.text = "";
    }

    // 2. SLOW: Memperlambat tempo game (menguntungkan saat game makin cepat)
    IEnumerator SlowRoutine()
    {
        if (statusText != null) statusText.text = "Game Slowed Down (10s)!";
        
        Time.timeScale = 0.5f; // Memperlambat jalannya waktu game jadi setengahnya
        Debug.Log("Slow Aktif: Permainan melambat.");
        
        yield return new WaitForSecondsRealtime(slowDuration);

        Time.timeScale = 1.0f; // Kembalikan normal
        if (statusText != null) statusText.text = "";
    }

    // 3. BOOSTER: Menerjang cepat & skor melesat (simulasi kecepatan tinggi)
    IEnumerator BoosterRoutine()
    {
        if (statusText != null) statusText.text = "SPEED BOOSTER!";
        
        if (playerController != null) playerController.moveSpeed = baseMoveSpeed * 2.5f;
        Debug.Log("Booster Aktif: Menerjang cepat!");

        yield return new WaitForSecondsRealtime(boosterDuration);

        if (playerController != null) playerController.moveSpeed = baseMoveSpeed;
        if (statusText != null) statusText.text = "";
    }

    // 4. FASTER (Debuff): Karakter & permainan makin cepat, susah dikontrol (durasi 5s)
    IEnumerator FasterRoutine()
    {
        if (statusText != null) statusText.text = "FASTER x2 (Hard Mode)!";
        
        Time.timeScale = 1.5f; // Waktu game dipercepat
        if (playerController != null) playerController.moveSpeed = baseMoveSpeed * 2f;
        
        Debug.Log("Faster Aktif: Game & Player ngebut x2!");
        yield return new WaitForSecondsRealtime(fasterDuration);

        Time.timeScale = 1.0f;
        if (playerController != null) playerController.moveSpeed = baseMoveSpeed;
        if (statusText != null) statusText.text = "";
    }

    // 5. FREEZE (Debuff): Karakter membeku total gak bisa pindah wilayah (durasi 3s)
    IEnumerator FreezeRoutine()
    {
        if (statusText != null) statusText.text = "FROZEN (Cant Move)!";
        
        if (playerController != null) playerController.moveSpeed = 0f; // Kunci total pergerakan player
        
        Debug.Log("Freeze Aktif: Player membeku!");
        yield return new WaitForSecondsRealtime(freezeDuration);

        if (playerController != null) playerController.moveSpeed = baseMoveSpeed; // Kembalikan kecepatan normal
        if (statusText != null) statusText.text = "";
    }
}