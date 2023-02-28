using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCubes : ObjectPool
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _level;

    private Cube _cube;

    private const float MinHealth = 1f;
    private const float MaxHealth = 3f;

    private void Start()
    {
        Initialize(_cubePrefab.gameObject);
    }

    public void Generate()
    {
        for (var i = 0; i < _level; i++)
        {
            foreach (var point in _points)
            {
                var newPoint = point.position;
                newPoint.z += i;

                if(TryGetObject(out var cube))
                {
                    SetPrefab(cube, newPoint);
                    cube.GetComponent<Cube>().SetHealth(Mathf.RoundToInt(Random.RandomRange(MinHealth + i, MaxHealth + i)));
                }
                
                //_cube.SetHealth(Mathf.RoundToInt(Random.RandomRange(MinHealth + i, MaxHealth + i)));
            }
        }
    }
    
    private void SetPrefab(GameObject cube, Vector3 spawnPosition)
    {
        cube.SetActive(true);
        cube.transform.position = spawnPosition;
    }
}
