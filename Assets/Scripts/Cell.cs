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
    [SerializeField] private UnityEvent _opened;

    private bool _isHaveObject = false;
    private Box _currentBox;
    private Tank _currentTank;

    public bool IsHaveObject => _isHaveObject;

    private void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var meshCollider = GetComponent<MeshCollider>();
        SetPreferences(rigidbody, meshCollider);
    }

    public void InstatiateBox()
    {
        _currentBox = Instantiate(_boxPrefab, _spawnPoint);
    }

    public void OpenBox()
    {
        Destroy(_currentBox);
        _currentTank = Instantiate(_tankPrefab, _spawnPoint);
    }

    public bool TryFindHaveObject()
    {
        if (_currentBox == null && _currentTank == null)
            return true;
        else 
            return false;
    }

    private void SetPreferences(Rigidbody rigidbody, MeshCollider meshCollider)
    {
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }
}
