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
    [SerializeField] private CanvasComponent _canvas;

    private const float MinHealth = 1f;
    private const float MaxHealth = 3f;
    private const float GrowthHealthUpRow = 3f;
    private int _probability = 33;

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
                    cube.GetComponent<Cube>().SetHealth(Mathf.RoundToInt(Random.RandomRange(MinHealth + i * GrowthHealthUpRow + i, MaxHealth + i * GrowthHealthUpRow + i) + i));
                    cube.GetComponent<Cube>().SetSpawner(this);

                    if (SetRandomHeavyType())
                        cube.GetComponent<Cube>().SetHeavyType(true);
                    else
                        cube.GetComponent<Cube>().SetHeavyType(false);
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

    public void ReleasePool()
    {
        Release();
    }

    public void Offset(float distance)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
    }

    public void TakeScore(int score)
    {
        _canvas.GetScore(score);
    }

    public void DieHeavyCube()
    {
        _levelManager.DieHeavyCube();
    }

    private void SetPrefab(GameObject cube, Vector3 spawnPosition)
    {
        cube.SetActive(true);
        cube.transform.position = spawnPosition;
    }

    private bool SetRandomHeavyType()
    {
        return 0 == Random.RandomRange(0, _probability);
    }
}
