using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _level;

    private Cube _cube;

    private const float MinHealth = 1f;
    private const float MaxHealth = 3f;

    public void Generate()
    {
        for (int i = 0; i < _level; i++)
        {
            foreach (var point in _points)
            {
                Vector3 newPoint = point.position;
                newPoint.z += i;

                _cube = Instantiate(_cubePrefab, newPoint, Quaternion.identity);
                _cube.SetHealth(Mathf.RoundToInt(Random.RandomRange(MinHealth + i, MaxHealth + i)));
            }
        }
    }
}
