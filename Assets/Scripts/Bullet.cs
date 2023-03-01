using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private float _damage;
    
    private Vector3 _targetPosition;

    private void FixedUpdate()
    {
        var position = transform.position;
        _targetPosition = new Vector3(position.x, position.y, position.z + _distance);
        position = Vector3.MoveTowards(position, _targetPosition, _speed);
        transform.position = position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent(out Cube cube) || collision.gameObject.TryGetComponent(out BulletDestroyer destroyer))
        {
            if (cube != null)
                cube.TakeDamage(_damage);

            gameObject.SetActive(false);
        }
    }
}
