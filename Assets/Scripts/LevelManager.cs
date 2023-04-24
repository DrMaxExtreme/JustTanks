using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _delayStartLevelAnimation;
    [SerializeField] private BulletDestroyer _bulletDestroyer;
    [SerializeField] private CanvasComponent _canvas;
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private SpawnerBoxes _spawnerBoxes;
    [SerializeField] private Cell[] _allCells;
    [SerializeField] private int _maxCountNewBoxes;
    [SerializeField] private float _offsetSpawnerCubes = 0.5f;
    [SerializeField] private float _maxOffsetSpawnerCubes = 5f;
    [SerializeField] private AudioSource _LevelWinSound;
    [SerializeField] private AudioSource _GameOverSound;
    [SerializeField] private AudioSource _GameMusic;
    [SerializeField] private BoostDamage _boostDamage;
    [SerializeField] private BoostScore _boostScore;
    [SerializeField] private SlowDownCubes _slowDownCubes;

    private int _bestCurrentLevelTank = -1;
    private int _currentLevel = 1;
    private int _firstLevel = 1;
    private float _normalTimeScale;
    private float _currentOffsetSpawnerCubes = 0;
    private int _addBoxesForDieHeavyCube = 1;
    
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
        SetPauseModeBoosts(false);
        _LevelWinSound.Play();
    }

    public void StartNextLevel()
    {
        StartCoroutine(StartedNextLevel());
    }

    public void GameOver()
    {
        _bestCurrentLevelTank = -1;
        _canvas.SetVisibleGameOverIcon(true);
        _spawnerCubes.ReleasePool();
        _currentLevel = _firstLevel;
        _spawnerCubes.Offset(-_currentOffsetSpawnerCubes);
        _currentOffsetSpawnerCubes = 0;
        ClearAllCells();
        _GameOverSound.Play();
        _canvas.ResetScore();
        _boostDamage.ResetTimer();
        _boostScore.ResetTimer();
        _slowDownCubes.ResetTimer();
        Time.timeScale = 0;
    }

    public void CheckTankLevel(int level)
    {
        if (level > _bestCurrentLevelTank)
        {
            _bestCurrentLevelTank = level;
            _canvas.SetVisibleNewTankIcon(true);
            _canvas.SetTextFeaturesNewTank(level + 1, _allCells[0].TankPrefabs[level].ShowPower());
            _canvas.SetRenderNewTank(_allCells[0].TankPrefabs[level].Render);
            Time.timeScale = 0;
        }
    }

    public void CloseNewTankIcon()
    {
        Time.timeScale = _normalTimeScale;
        _canvas.SetVisibleNewTankIcon(false);
    }

    public void DieHeavyCube()
    {
        _spawnerBoxes.Activate(_addBoxesForDieHeavyCube);
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
        ActivateSpawnerBoxes();
        _bulletDestroyer.Activate();
        SetPauseModeBoosts(true);

        if (_currentOffsetSpawnerCubes <= _maxOffsetSpawnerCubes)
        {
            _currentOffsetSpawnerCubes += _offsetSpawnerCubes;
            _spawnerCubes.Offset(_offsetSpawnerCubes);
        }
        
        yield return waitForDelaySeconds;
        
        _canvas.SetVisibleStartLevelLabel(false);
    }

    private void ActivateSpawnerBoxes()
    {
        _spawnerBoxes.Activate(_currentLevel <= _maxCountNewBoxes ? _currentLevel : _maxCountNewBoxes);
    }

    private void ClearAllCells()
    {
        foreach (var cell in _allCells)
        {
            cell.Clear();
        }
    }

    private void SetPauseModeBoosts(bool isPaube)
    {
        _boostDamage.SetPauseMode(isPaube);
        _boostScore.SetPauseMode(isPaube);
        _slowDownCubes.SetPauseMode(isPaube);
    }
}
