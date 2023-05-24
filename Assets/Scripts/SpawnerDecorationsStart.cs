using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDecorationsStart : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> prefabs;

    private void Start()
    {
        foreach (var point in spawnPoints)
        {
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
            Instantiate(randomPrefab, point.position, point.rotation);
        }
    }
}
