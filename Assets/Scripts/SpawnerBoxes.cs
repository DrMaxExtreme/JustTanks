using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBoxes : MonoBehaviour
{
    [SerializeField] private List<Cell> _allCells;
    [SerializeField] private float _delayBetweenActivate;
    [SerializeField] private CanvasComponent _canvasComponent;

    private readonly List<Cell> _freeCells = new List<Cell>();
    private bool _isActive = true;
    private float _remainingDelay;
    private int _numberOfBoxes;

    private void Start()
    {
        UpdateUIIconBoxes();
    }

    private void Update()
    {
        if (_numberOfBoxes == 0)
        {
            _isActive = false;
            UpdateUIIconBoxes();
            _remainingDelay = 0;
        }
        
        if (_isActive)
        {
            _remainingDelay -= Time.deltaTime;
            UpdateUIIconBoxes();

            if (TryFindFreeCell() && _numberOfBoxes > 0 && _remainingDelay <= 0)
            {
                GenerateBox();
                _remainingDelay = _delayBetweenActivate;
            }
        }
    }

    private void OnDisable()
    {
        Stop();
    }

    public void Activate(int numberOfBoxes)
    {
        _numberOfBoxes += numberOfBoxes;
        _isActive = true;
    }

    public void Stop()
    {
        _isActive = false;
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

    private void UpdateUIIconBoxes()
    {
        float normalazeDelay = _remainingDelay / _delayBetweenActivate;
        _canvasComponent.UpdateTextCountBoxes(_numberOfBoxes, normalazeDelay);
    }
}
