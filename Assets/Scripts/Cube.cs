using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private TextMesh _textHealth;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private float _health;
    private Vector3 _targetPosition;

    private void FixedUpdate()
    {
        _targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - _distance);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
    }

    public void SetHealth(float health)
    {
        _health = health;
        TextUpdate();
    }

    public void TankDamage(float damage)
    {
        if(damage > 0)
            _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);

        TextUpdate();
    }

    private void TextUpdate()
    {
        _textHealth.text = Convert.ToString(Mathf.Round(_health));
    }
}
