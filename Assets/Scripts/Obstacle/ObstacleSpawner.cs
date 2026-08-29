using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;

    [SerializeField] private GameObject[] obstacleList;

    [Header("Lane Settings")]
    [SerializeField]
    private float[] _laneYPositions = new float[5] { 3.8f, 3.5f, 2.8f, 1.3f, -0.3f };

    private float _timer;

    private void Start()
    {
        SpawnObs();
    }

    private void Update()
    {
        if (_timer > _maxTime)
        {
            SpawnObs();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void SpawnObs()
    {
        if (obstacleList == null || _laneYPositions.Length == 0) return;

        // Pilih 1 indeks jalur acak (0, 1, atau 2)
        int randomLaneIndex = Random.Range(0, _laneYPositions.Length);
        int randomPrefabIndex = Random.Range(0, 2);
        float yPos = _laneYPositions[randomLaneIndex];

        // Buat posisi spawn 1 obstacle saja
        Vector3 spawnPos = transform.position + new Vector3(0, yPos, 0);
        GameObject obs = Instantiate(obstacleList[randomPrefabIndex], spawnPos, Quaternion.identity);

        Destroy(obs, 10f);
    }

}