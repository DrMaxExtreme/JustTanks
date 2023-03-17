using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private float _damage;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, _speed);
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
