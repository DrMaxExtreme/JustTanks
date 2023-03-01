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

    private int _currentLevel;

    private const float MinHealth = 1f;
    private const float MaxHealth = 3f;
    private const int Level = 1;

    private void Awake()
    {
        Initialize(_cubePrefab.gameObject);
        _currentLevel = Level;
    }

    public void Generate()
    {
        for (var i = 0; i < _currentLevel; i++)
        {
            foreach (var point in _points)
            {
                var newPoint = point.position;
                newPoint.z += i;

                if(TryGetObject(out var cube))
                {
                    SetPrefab(cube, newPoint);
                    cube.GetComponent<Cube>().SetHealth(Mathf.RoundToInt(Random.RandomRange(MinHealth + i, MaxHealth + i)));
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
    
    private void SetPrefab(GameObject cube, Vector3 spawnPosition)
    {
        cube.SetActive(true);
        cube.transform.position = spawnPosition;
    }
}
