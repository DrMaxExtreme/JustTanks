using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Cell> _allCells;
    [SerializeField] private float _delayActivate;

    private List<Cell> _freeCells = new List<Cell>();
    private bool _isActive = false;

    private void OnDisable()
    {
        StopCoroutine(Activate());
    }

    private void Start()
    {
        _isActive = true;
        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        while (_isActive)
        {
            var WaitForDelaySeconds = new WaitForSeconds(_delayActivate);

            GenerateBox();

            yield return WaitForDelaySeconds;
        }
    }

    private bool TryFindFreeCell()
    {
        _freeCells.Clear();

        foreach (var cell in _allCells)
        {
            if (cell.TryFindHaveObject() == false)
            {
                _freeCells.Add(cell);
            }
        }

        if (_freeCells.Count > 0)
            return true;
        else
            return false;
    }

    private void GenerateBox()
    {
        if (TryFindFreeCell())
        {
            _freeCells[Random.RandomRange(0, _freeCells.Count)].InstatiateBox();
        }
    }
}
