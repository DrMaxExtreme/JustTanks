using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _delayStartLevelAnimation;
    [SerializeField] private CanvasComponent _canvas;
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private SpawnerBoxes _spawnerBoxes;
    [SerializeField] private Cell[] _activateAttackTankCells;

    private int _currentLevel = 1;
    private int _firstLevel = 1;
    private float _normalTimeScale;

    private void Start()
    {
        _normalTimeScale = Time.timeScale;
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

    public void ShowGameOver()
    {
        _canvas.SetVisibleGameOverIcon(true);
        _spawnerCubes.ReleasePool();
        _currentLevel = _firstLevel;
        Time.timeScale = 0;
    }
    
    private IEnumerator StartedNextLevel()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayStartLevelAnimation);

        Time.timeScale = _normalTimeScale;
        _spawnerCubes.Generate(_currentLevel);
        _canvas.SetVisibleStartGameIcon(false);
        _canvas.SetVisibleContinueGameIcon(false);
        _canvas.SetVisibleGameOverIcon(false);
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
