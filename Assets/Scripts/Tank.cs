using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private Transform[] _bulletSpawnPositions;
    [SerializeField] private Transform[] _bulletTargetPositions;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _delayBetweenShots;
    [SerializeField] private Sprite _render;
    
    private List<GameObject> _cubesPool;
    private bool _isAttacking;
    private Coroutine _shootJob;
    private Vector3 _targetPosition;
    private Vector3 _direction;
    
    public int Level => _level;
    public Sprite Render => _render;

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

    public void TakePool(List<GameObject> cubesPool)
    {
        if (_cubesPool == null)
            _cubesPool = cubesPool;
    }

    public float ShowPower()
    {
        return Convert.ToSingle(Math.Round(_bulletSpawnPositions.Length / _delayBetweenShots, 1));
    }

    private bool TrySelectNearestTarget()
    {
        float nearestDistance = float.MaxValue;
        bool isSelect = false;

        foreach (var cube in _cubesPool)
        {
            if (cube.active == true)
            {
                _direction = cube.transform.position - transform.position;
                var directionMagnitude = _direction.magnitude;

                if (directionMagnitude < nearestDistance)
                {
                    nearestDistance = directionMagnitude;
                    _targetPosition = cube.transform.position;
                    
                    transform.LookAt(_targetPosition);
                    transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    
                    isSelect = true;
                }
            }
        }

        return isSelect;
    }

    private void Shot()
    {
         for (int i = 0; i < _bulletSpawnPositions.Length; i++)
         {
              Vector3 target = new Vector3(Convert.ToSingle(_bulletTargetPositions[i].position.x), Convert.ToSingle(_bulletTargetPositions[i].position.y), Convert.ToSingle(_bulletTargetPositions[i].position.z));

              Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPositions[i].position, Quaternion.identity, null);
              
              bullet.gameObject.GetComponent<Bullet>().GetTargetTransform(target);
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
}
