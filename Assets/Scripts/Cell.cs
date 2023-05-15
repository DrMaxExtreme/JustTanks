using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Cell : MonoBehaviour
{
    [SerializeField] private Box _boxPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Tank[] _tankPrefabs;
    [SerializeField] private UnityEvent _selectedTank;
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private bool _isActivatingAttackingTank;
    [SerializeField] private CanvasComponent _canvas;

    private Box _currentBox;
    private Tank _currentTank;
    
    private const int IndexSpawnedTank = 0;
    private const float SecondsDelayOpenBox = 0.01f;

    public Tank[] TankPrefabs => _tankPrefabs;

    public Tank CurrentTank => _currentTank;

    private void OnDisable()
    {
        StopCoroutine(DelaySpawnedTank(IndexSpawnedTank));
    }

    public bool TryFindHaveObject()
    {
        return _currentBox != null || _currentTank != null;
    }

    public void InstantiateBox()
    {
        _currentBox = Instantiate(_boxPrefab, _spawnPoint);
    }

    public void Select()
    {
        if(_currentBox != null)
            OpenBox();
        
        if (_currentTank != null)
            SelectTank();
    }

    public void SetPositionTank(Vector3 tempPosition)
    {
        _currentTank.transform.position = tempPosition;
    }

    public Tank GiveTank()
    {
        return _currentTank;
    }

    public void ClearTank()
    {
        if (_isActivatingAttackingTank)
            _canvas.UpdateTextCurrentPowerActiveTanks(-CurrentTank.ShowPower());

        _currentTank = null;
    }

    public void DestroyTank()
    {
        Destroy(_currentTank.gameObject);

        if(_isActivatingAttackingTank)
            _canvas.UpdateTextCurrentPowerActiveTanks(- CurrentTank.ShowPower());
    }

    public void TakeTank(Tank tank)
    {
        _currentTank = tank;
        _currentTank.transform.position = _spawnPoint.position;
        SetTankAttackMode();

        if (_isActivatingAttackingTank)
            _canvas.UpdateTextCurrentPowerActiveTanks(CurrentTank.ShowPower());
    }

    public bool IsHaveTankForUpgrade(int newTankLevel)
    {
        return _currentTank.Level == newTankLevel && _currentTank.Level < _tankPrefabs.Length - 1;
    }

    public void UpgradeTank()
    {
        int upgradeTankIndex = _currentTank.Level;
        Destroy(_currentTank.gameObject);
        StartCoroutine(DelaySpawnedTank(upgradeTankIndex));

        if (_isActivatingAttackingTank)
            _canvas.UpdateTextCurrentPowerActiveTanks(-CurrentTank.ShowPower());
    }
    
    public void SetTankAttackMode()
    {
        if(_currentTank != null)
            _currentTank.SetAttackMode(_isActivatingAttackingTank);
    }

    public void Clear()
    {
        if(_currentTank != null )
            Destroy(_currentTank.gameObject);

        if (_currentBox != null)
            Destroy(_currentBox.gameObject);
    }

    private void OpenBox()
    {
        StartCoroutine(DelaySpawnedTank(IndexSpawnedTank));
        Destroy(_currentBox.gameObject);
    }

    private void SelectTank()
    {
        _selectedTank?.Invoke();
        _currentTank.SetAttackMode(false);
    }

    private IEnumerator DelaySpawnedTank(int indexSpawnedTank)
    {
        var waitForDelaySeconds = new WaitForSeconds(SecondsDelayOpenBox);

        yield return waitForDelaySeconds;

        _currentTank = Instantiate(_tankPrefabs[indexSpawnedTank], _spawnPoint);
        _currentTank.TakePool(_spawnerCubes.ShowPool());
        SetTankAttackMode();
        _levelManager.CheckTankLevel(indexSpawnedTank);

        if (_isActivatingAttackingTank)
            _canvas.UpdateTextCurrentPowerActiveTanks(CurrentTank.ShowPower());
    }
}
