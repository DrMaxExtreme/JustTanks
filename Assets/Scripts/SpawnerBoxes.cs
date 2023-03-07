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
    private bool _isActive = true;
    private int _numberOfBoxes;
    private Coroutine _isSpawned;

    private void Start()
    {
        _isSpawned = StartCoroutine(GenerateBoxes());
    }

    private void OnDisable()
    {
        Stop();
    }

    public void Activate(int numberOfBoxes)
    {
        _numberOfBoxes += numberOfBoxes;
        _isSpawned = StartCoroutine(GenerateBoxes());
    }

    public void Stop()
    {
        if(_isSpawned != null)
            StopCoroutine(_isSpawned);
    }

    private IEnumerator GenerateBoxes()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayActivate);
        
        while (_isActive)
        {
            if (TryFindFreeCell() && _numberOfBoxes > 0)
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
        }
    }
}
