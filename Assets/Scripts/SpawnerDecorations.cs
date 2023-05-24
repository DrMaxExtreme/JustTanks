using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDecorations : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private float _minSpawnInterval = 3f;
    [SerializeField] private float _maxSpawnInterval = 10f;

    private bool _isActive = true;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (_isActive)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];

            Instantiate(randomPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            yield return new WaitForSeconds(Random.Range(_minSpawnInterval, _maxSpawnInterval));
        }
    }
}
