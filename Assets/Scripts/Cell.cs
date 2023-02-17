using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Cell : MonoBehaviour
{
    [SerializeField] private Box _boxPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Tank _tankPrefab;
    [SerializeField] private UnityEvent _selectedTank;

    private Box _currentBox;
    private Tank _currentTank;
    
    private const float SecondsDelayOpenBox = 0.02f;

    private void OnDisable()
    {
        StopCoroutine(DelayOpenBox());
    }

    public bool TryFindHaveObject()
    {
        return _currentBox == null && _currentTank == null;
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

    public void TakeTank(Tank tank)
    {
        _currentTank = tank;
        _currentTank.transform.position = _spawnPoint.position;
    }

    private void OpenBox()
    {
        StartCoroutine(DelayOpenBox());
    }

    private void SelectTank()
    {
        _selectedTank?.Invoke();
    }

    private IEnumerator DelayOpenBox()
    {
        var waitForDelaySeconds = new WaitForSeconds(SecondsDelayOpenBox);

        yield return waitForDelaySeconds;

        _currentTank = Instantiate(_tankPrefab, _spawnPoint);
        Destroy(_currentBox.gameObject);
    }
}
