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
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, _speed);
    }
}
