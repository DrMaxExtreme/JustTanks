using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SpawnerCubes : ObjectPool
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private LevelManager _levelManager;

    private const float MinHealth = 1f;
    private const float MaxHealth = 4f;
    private const float GrowthHealthUpRow = 4f;

    private void Awake()
    {
        Initialize(_cubePrefab.gameObject);
    }

    public void Generate(int currentLevel)
    {
        for (var i = 0; i < currentLevel; i++)
        {
            foreach (var point in _points)
            {
                var newPoint = point.position;
                newPoint.z += i;

                if(TryGetObject(out var cube))
                {
                    SetPrefab(cube, newPoint);
                    cube.GetComponent<Cube>().SetHealth(Mathf.RoundToInt(Random.RandomRange(MinHealth + i * GrowthHealthUpRow, MaxHealth + i * GrowthHealthUpRow)));
                    cube.GetComponent<Cube>().SetSpawner(this);
                }
            }
        }
    }

    public void TryFinishLevel()
    {
        if (TryFindObject())
            _levelManager.ShowWin();
    }

    public List<GameObject> ShowPool()
    {
        return Pool;
    }
    
    private void SetPrefab(GameObject cube, Vector3 spawnPosition)
    {
        cube.SetActive(true);
        cube.transform.position = spawnPosition;
    }
}
