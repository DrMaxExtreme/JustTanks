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
    private float _delayOpenBox = 0.02f;

    private void OnDisable()
    {
        StopCoroutine(DelayOpenBox());
    }

    public bool TryFindHaveObject()
    {
        if (_currentBox == null && _currentTank == null)
            return false;
        else 
            return true;
    }

    public void InstatiateBox()
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
        var WaitForDelaySeconds = new WaitForSeconds(_delayOpenBox);

        yield return WaitForDelaySeconds;

        _currentTank = Instantiate(_tankPrefab, _spawnPoint);
        Destroy(_currentBox.gameObject);
    }
}
