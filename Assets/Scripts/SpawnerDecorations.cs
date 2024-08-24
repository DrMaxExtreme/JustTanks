using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustTanks.Decorations
{
    public class SpawnerDecorations : MonoBehaviour
    {
        private const float MaxRotationY = 360;

        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private List<GameObject> prefabs;
        [SerializeField] private float _minSpawnInterval = 3f;
        [SerializeField] private float _maxSpawnInterval = 7f;

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

                var obj = Instantiate(randomPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                obj.transform.Rotate(0f, Random.Range(0, MaxRotationY), 0f);

                yield return new WaitForSeconds(Random.Range(_minSpawnInterval, _maxSpawnInterval));
            }
        }
    }
}
