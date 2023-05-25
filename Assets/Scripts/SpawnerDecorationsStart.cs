using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDecorationsStart : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> prefabs;

    private const float MaxRotationY = 360f;
    
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
