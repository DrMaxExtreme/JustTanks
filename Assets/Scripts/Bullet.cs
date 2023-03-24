using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private float _damage;

    private Vector3 _targetPoint;
    
    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube != null)
                cube.TakeDamage(_damage);

            Disactivate();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed);

        if (transform.position == _targetPoint)
            Disactivate();
    }

    public void GetTargetTransform(Vector3 targetTransform)
    {
        _targetPoint = targetTransform;
        transform.LookAt(_targetPoint);
    }

    private void Disactivate()
    {
        gameObject.SetActive(false);
    }
}
