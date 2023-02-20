using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private Transform[] _bulletSpawnPositions;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _delayBetweenShots;

    private bool _isAttacking;
    private Coroutine _shootJob;
    
    public int Level => _level;

    private void OnDisable()
    {
        StopCoroutine(Shoot());
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

    private void Shot()
    {
        foreach (var bulletSpawnPosition in _bulletSpawnPositions)
        {
            Instantiate(_bullet, bulletSpawnPosition);
        }
    }
    
    private IEnumerator Shoot()
    {
        while (_isAttacking)
        {
            var waitForDelaySeconds = new WaitForSeconds(_delayBetweenShots);
            
            yield return waitForDelaySeconds;
            
            Shot();
        }
    }
}
