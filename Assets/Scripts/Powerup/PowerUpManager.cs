using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    [Header("UI Text Notification")]
    public Text statusText;

    [Header("Visual Perisai (Shield)")]
    public GameObject shieldVisualObject;

    [Header("Durasi Sesuai Desain (Detik)")]
    public float defenseDuration = 5f;
    public float slowDuration = 10f;
    public float boosterDuration = 3f;
    public float fasterDuration = 5f;
    public float freezeDuration = 3f;

    private PlayerMovement playerMovement;
    private float baseMoveSpeed; // Menyimpan nilai default moveSpeed dari PlayerMovement

    // Status Penanda Power Up
    [HideInInspector] public bool isDefenseActive = false;
    [HideInInspector] public bool isSlowActive = false;
    [HideInInspector] public bool isBoosterActive = false;
    [HideInInspector] public bool isFasterActive = false;
    [HideInInspector] public bool isFreezeActive = false;

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
        if (statusText != null) statusText.text = "";

        // Ambil komponen PlayerMovement dan simpan nilai speed awalnya
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            baseMoveSpeed = playerMovement.moveSpeed;
        }
    }

    public void TriggerPowerUp(string effectName)
    {
        StopAllCoroutines();
        ResetAllPowerUpStates();

        switch (effectName)
        {
            case "Defense": StartCoroutine(DefenseRoutine()); break;
            case "Slow": StartCoroutine(SlowRoutine()); break;
            case "Booster": StartCoroutine(BoosterRoutine()); break;
            case "Faster": StartCoroutine(FasterRoutine()); break;
            case "Freeze": StartCoroutine(FreezeRoutine()); break;
        }
    }

    public void CancelPowerUpsOnDeath()
    {
        StopAllCoroutines();
        ResetAllPowerUpStates();

        if (shieldVisualObject != null) shieldVisualObject.SetActive(false);

        // Kembalikan speed ke normal jika mati saat sedang Freeze/Booster/Faster
        if (playerMovement != null)
        {
            playerMovement.moveSpeed = baseMoveSpeed;
        }
    }

    private void ResetAllPowerUpStates()
    {
        isDefenseActive = false;
        isSlowActive = false;
        isBoosterActive = false;
        isFasterActive = false;
        isFreezeActive = false;
    }

    IEnumerator DefenseRoutine()
    {
        isDefenseActive = true;
        if (statusText != null) statusText.text = "Invisible Shield Active!";
        if (shieldVisualObject != null) shieldVisualObject.SetActive(true);

        yield return new WaitForSecondsRealtime(defenseDuration);

        isDefenseActive = false;
        if (shieldVisualObject != null) shieldVisualObject.SetActive(false);
        if (statusText != null) statusText.text = "";
    }

    IEnumerator SlowRoutine()
    {
        isSlowActive = true;
        if (statusText != null) statusText.text = "Game Slowed Down (10s)!";
        Time.timeScale = 1.0f;

        yield return new WaitForSecondsRealtime(slowDuration);

        isSlowActive = false;
        Time.timeScale = 1.3f;
        if (statusText != null) statusText.text = "";
    }

    IEnumerator BoosterRoutine()
    {
        isBoosterActive = true;
        if (statusText != null) statusText.text = "SPEED BOOSTER!";
        if (playerMovement != null) playerMovement.moveSpeed = baseMoveSpeed * 2.5f;

        yield return new WaitForSecondsRealtime(boosterDuration);

        isBoosterActive = false;
        if (playerMovement != null) playerMovement.moveSpeed = baseMoveSpeed;
        if (statusText != null) statusText.text = "";
    }

    IEnumerator FasterRoutine()
    {
        isFasterActive = true;
        if (statusText != null) statusText.text = "FASTER x2 (Hard Mode)!";
        Time.timeScale = 1.9f;
        if (playerMovement != null) playerMovement.moveSpeed = baseMoveSpeed * 2f;

        yield return new WaitForSecondsRealtime(fasterDuration);

        isFasterActive = false;
        Time.timeScale = 1.0f;
        if (playerMovement != null) playerMovement.moveSpeed = baseMoveSpeed;
        if (statusText != null) statusText.text = "";
    }

    IEnumerator FreezeRoutine()
    {
        isFreezeActive = true;
        if (statusText != null) statusText.text = "FROZEN (Cant Move)!";

        // Buat speed jadi 0, PlayerMovement akan otomatis membaca ini dan velocity jadi 0
        if (playerMovement != null) playerMovement.moveSpeed = 0f;

        yield return new WaitForSecondsRealtime(freezeDuration);

        isFreezeActive = false;
        if (playerMovement != null) playerMovement.moveSpeed = baseMoveSpeed;
        if (statusText != null) statusText.text = "";
    }
}