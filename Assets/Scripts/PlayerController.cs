using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lane Settings")]
    [SerializeField] private float[] lanePositions = { 3.0f, 0.0f, -3.0f }; // 0 = Atas, 1 = Tengah, 2 = Bawah
    private int targetLaneIndex = 1; // Mulai di lane tengah (index 1)
    [SerializeField] private float laneChangeSpeed = 15.0f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Pindah lane secara mulus ke posisi Y target
        float targetY = lanePositions[targetLaneIndex];
        float newY = Mathf.MoveTowards(transform.position.y, targetY, laneChangeSpeed * Time.deltaTime);

        // Posisi X dikunci tetap di 0
        transform.position = new Vector3(0f, newY, transform.position.z);

        // 2. Input Keyboard
        HandleInput();
    }

    void HandleInput()
    {
        // Tekan Atas / W -> Indeks berkurang (menuju 0 / Atas)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (targetLaneIndex > 0) 
            {
                targetLaneIndex++; 
            }
        }
        // Tekan Bawah / S -> Indeks bertambah (menuju 2 / Bawah)
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (targetLaneIndex < lanePositions.Length - 1) 
            {
                targetLaneIndex--; 
            }
        }
    }
}