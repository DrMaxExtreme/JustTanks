using System;
using System.Collections;
using System.Collections.Generic;
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
            _cubesPool = cubesPool;  //Найти ближайший куб и стрелять в него. Если кубов нет, не стрелять.
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
            
            Shot();
        }
    }

    private void SetBullet(GameObject bullet, Vector3 spawnPosition)
    {
        bullet.SetActive(true);
        bullet.transform.position = spawnPosition;
    }
}
