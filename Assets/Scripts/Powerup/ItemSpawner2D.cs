using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner2D : MonoBehaviour
{
    [Header("Pengaturan Item")]
    public GameObject[] itemPrefabs;
    public float spawnInterval = 3f;

    [Header("Posisi Muncul (Spawn)")]
    public float spawnXPosition = 10f;

    [Header("Pengaturan 3 Jalur (Lane Y)")]
    public float[] laneYPositions = new float[] { 3f, 0f, -3f };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (itemPrefabs.Length > 0 && laneYPositions.Length > 0)
            {
                int randomItemIndex = Random.Range(0, itemPrefabs.Length);

                int randomLaneIndex = Random.Range(0, laneYPositions.Length);
                float selectedY = laneYPositions[randomLaneIndex];


                Vector2 spawnPosition = new Vector2(spawnXPosition, selectedY);

                Instantiate(itemPrefabs[randomItemIndex], spawnPosition, Quaternion.identity);
            }
        }
    }
}
