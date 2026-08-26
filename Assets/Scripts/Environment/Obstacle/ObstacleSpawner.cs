using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    //[SerializeField] private float _height1 = 0.10f;
    //[SerializeField] private float _height2 = 3.50f;
    [SerializeField] private GameObject _obstacle;

    [Header("Lane Settings")]
    // Pokoknya buat ngatur 3 lane ini wok
    [SerializeField]
    private float[] _laneYPositions = new float[3]
    {
        1.0f, 0.8f, 0.5f
    };

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
        int randomLaneIndex = Random.Range(0, _laneYPositions.Length);
        float yPos = _laneYPositions[randomLaneIndex];

            //Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(_height1, _height2));
        Vector3 spawnPos = transform.position + new Vector3(0, yPos, 0);
        GameObject obs = Instantiate(_obstacle, spawnPos, Quaternion.identity);
        Destroy(obs, 10f);
    }
}
