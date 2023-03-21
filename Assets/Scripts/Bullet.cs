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

    private Transform _directionTransform;
    
    private void FixedUpdate()
    {
        Move();
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

    private void Move()
    {
        var position = _directionTransform.localPosition;
        Vector3 targetPosition = new Vector3(position.x, position.y,position.z + _distance);
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition , _speed);
    }

    public void GetTransform(Transform directionTransform)
    {
        _directionTransform = directionTransform;
        transform.rotation = directionTransform.localRotation;
    }
}
