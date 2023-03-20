using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tank : ObjectPool
{
    [SerializeField] private int _level;
    [SerializeField] private Transform[] _bulletSpawnPositions;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _delayBetweenShots;
    
    private List<GameObject> _cubesPool;
    private bool _isAttacking;
    private Coroutine _shootJob;
    private Vector3 _targetPosition;
    private Vector3 _directoion;
    
    public int Level => _level;

    private void Start()
    {
        Initialize(_bulletPrefab.gameObject);
    }

    private void OnDisable()
    {
        StopCoroutine(Shoot());
        ClearPool();
    }

    public void SetAttackMode(bool isAttacking)
    {
        _isAttacking = isAttacking;

        if (_shootJob == null)
        {
            _shootJob = StartCoroutine(Shoot());
        }
        else
        {
            StopCoroutine(_shootJob);
            _shootJob = null;
        }
    }

    public void TakePool(List<GameObject> cubesPool)
    {
        if (_cubesPool == null)
            _cubesPool = cubesPool;
    }

    private void Shot()
    {
        foreach (var bulletSpawnPosition in _bulletSpawnPositions)
        {
            if(TryGetObject(out var bullet))
            {
                SetBullet(bullet, bulletSpawnPosition.position);
            }
        }
    }
    
    private IEnumerator Shoot()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayBetweenShots);
        
        while (_isAttacking)
        {
            yield return waitForDelaySeconds;

            if(TrySelectNearestTarget() == true)
                Shot();
        }
    }

    private void SetBullet(GameObject bullet, Vector3 spawnPosition)
    {
        bullet.SetActive(true);
        bullet.transform.position = spawnPosition;
    }

    private bool TrySelectNearestTarget()
    {
        float nearestDistance = float.MaxValue;
        float distanceMagnitude;
        bool isSecect = false;

        foreach (var cube in _cubesPool)
        {
            if(cube.active == true)
            {
                _directoion = cube.transform.position - transform.position;
                distanceMagnitude = _directoion.magnitude;

                if (distanceMagnitude < nearestDistance)
                {
                    nearestDistance = distanceMagnitude;
                    _targetPosition = cube.transform.position;
                    transform.LookAt(_targetPosition);
                    isSecect = true;
                }
            }
        }

        return isSecect;
    }
}
