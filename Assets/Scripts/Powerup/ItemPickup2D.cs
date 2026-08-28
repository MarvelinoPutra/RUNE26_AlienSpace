using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup2D : MonoBehaviour
{
    public enum EffectType { Defense, Slow, Booster, Faster, Freeze }
    public EffectType selectedEffect;

    [Header("Pengaturan Gerak & Efek")]
    public float moveSpeed = 3f; // Variabel pengatur kecepatan ke kiri
    public GameObject bubbleBreakEffectPrefab; 

    void Update()
    {
        // Menggeser item ke arah kiri secara terus-menerus
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUpManager playerPowerUp = other.GetComponent<PowerUpManager>();

            if (playerPowerUp != null)
            {
                playerPowerUp.TriggerPowerUp(selectedEffect.ToString());
            }

            if (bubbleBreakEffectPrefab != null)
            {
                Instantiate(bubbleBreakEffectPrefab, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySFX("Bubble");
            }

            Destroy(gameObject);
        }
    }
}
