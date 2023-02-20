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
    [SerializeField] private bool _isActivatingAttackingTank;

    private Box _currentBox;
    private Tank _currentTank;
    
    private const int IndexSpawnedTank = 0;
    private const float SecondsDelayOpenBox = 0.02f;

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

    public void ClearCell()
    {
        _currentTank = null;
    }

    public void DestroyTank()
    {
        Destroy(_currentTank.gameObject);
    }

    public void TakeTank(Tank tank)
    {
        _currentTank = tank;
        _currentTank.transform.position = _spawnPoint.position;
        SetTankAttackMode();
    }

    public bool IsHaveTankForUpgrade(int newTankLevel)
    {
        return _currentTank.Level == newTankLevel && _currentTank.Level < _tankPrefabs.Length;
    }

    public void UpgradeTank()
    {
        int upgradeTankIndex = _currentTank.Level;
        Destroy(_currentTank.gameObject);
        StartCoroutine(DelaySpawnedTank(upgradeTankIndex));
    }

    private void OpenBox()
    {
        StartCoroutine(DelaySpawnedTank(IndexSpawnedTank));
        Destroy(_currentBox.gameObject);
    }

    private void SelectTank()
    {
        _selectedTank?.Invoke();
    }

    private IEnumerator DelaySpawnedTank(int indexSpawnedTank)
    {
        var waitForDelaySeconds = new WaitForSeconds(SecondsDelayOpenBox);

        yield return waitForDelaySeconds;

        _currentTank = Instantiate(_tankPrefabs[indexSpawnedTank], _spawnPoint);
        SetTankAttackMode();
    }

    private void SetTankAttackMode()
    {
        _currentTank.SetAttackMode(_isActivatingAttackingTank);
    }
}
