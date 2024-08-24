using System.Collections.Generic;
using UnityEngine;

namespace JustTanks.Decorations
{
    public class SpawnerDecorationsStart : MonoBehaviour
    {
        private const float MaxRotationY = 360f;

        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private List<GameObject> prefabs;

        private void Start()
        {
            foreach (var point in spawnPoints)
            {
                GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
                var obj = Instantiate(randomPrefab, point.position, point.rotation);
                obj.transform.Rotate(0f, Random.Range(0, MaxRotationY), 0f);
            }
        }
    }
}
