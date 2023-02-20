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
    
    public int Level => _level;

    private void OnDisable()
    {
        StopCoroutine(Shoot());
    }

    public void SetAttackMode(bool isAttacking)
    {
        _isAttacking = isAttacking;
        //сохранить корутину, чтобы проверять работает она или нет
        if(_isAttacking)
            StartCoroutine(Shoot());
        else
            StopCoroutine(Shoot());
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
            
            Shot();

            yield return waitForDelaySeconds;
        }
    }
}
