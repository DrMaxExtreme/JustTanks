using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBoxes : MonoBehaviour
{
    [SerializeField] private List<Cell> _allCells;
    [SerializeField] private float _delayActivate;

    private readonly List<Cell> _freeCells = new List<Cell>();
    private bool _isActive = false;
    private int _numberOfBoxes;
    private Coroutine _spawnedJob;

    private void Start()
    {
        _spawnedJob = StartCoroutine(GenerateBoxes());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnedJob);
    }

    public void Activate(int numberOfBoxes)
    {
        _isActive = true;
        _numberOfBoxes += numberOfBoxes;
        _spawnedJob = StartCoroutine(GenerateBoxes());
    }
    
    private void Stop()
    {
        if(_spawnedJob != null)
            StopCoroutine(_spawnedJob);
    }

    private IEnumerator GenerateBoxes()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayActivate);
        
        while (_isActive)
        {
            GenerateBox();

            yield return waitForDelaySeconds;
        }
    }

    private bool TryFindFreeCell()
    {
        _freeCells.Clear();

        foreach (var cell in _allCells.Where(cell => cell.TryFindHaveObject() == false))
        {
            _freeCells.Add(cell);
        }

        return _freeCells.Count > 0;
    }

    private void GenerateBox()
    {
        if (TryFindFreeCell() && _numberOfBoxes > 0)
        {
            _freeCells[Random.RandomRange(0, _freeCells.Count)].InstantiateBox();
            _numberOfBoxes--;

            if (_numberOfBoxes == 0)
            {
                Stop();
            }
        }
    }
}
