using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _delayAnimation;
    [SerializeField] private CanvasComponent _canvas;
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private SpawnerBoxes _spawnerBoxes;
    [SerializeField] private Cell[] _activateAttackTankCells;

    private int _currentLevel = 1;
    
    private void Start()
    {
        _canvas.SetVisibleStartGameIcon(true);
    }

    private void OnDisable()
    {
        StopCoroutine(StartedNextLevel());
    }

    public void ShowWin()
    {
        _currentLevel++;
        _spawnerBoxes.Stop();
        _canvas.SetVisibleContinueGameIcon(true);
        SetActivateAttackModeTankCells(false);
    }

    public void StartNextLevel()
    {
        StartCoroutine(StartedNextLevel());
        SetActivateAttackModeTankCells(true);
    }
    
    private IEnumerator StartedNextLevel()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayAnimation);
        
        _spawnerCubes.Generate(_currentLevel);
        _canvas.SetVisibleStartGameIcon(false);
        _canvas.SetVisibleContinueGameIcon(false);
        _canvas.SetVisibleStartLevelLabel(true);
        _canvas.UpdateTextLevel(_currentLevel);
        _spawnerBoxes.Activate(_currentLevel);
        
        yield return waitForDelaySeconds;
        
        _canvas.SetVisibleStartLevelLabel(false);
    }

    private void SetActivateAttackModeTankCells(bool attackingMode)
    {
        foreach (var cell in _activateAttackTankCells)
        {
            cell.SetActivatingAttackingTank(attackingMode);
        }
    }
}
