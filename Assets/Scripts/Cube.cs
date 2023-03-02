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
    private SpawnerCubes _spawnerCubes;

    private void FixedUpdate()
    {
        var position = transform.position;
        _targetPosition = new Vector3(position.x, position.y, position.z - _distance);
        position = Vector3.MoveTowards(position, _targetPosition, _speed);
        transform.position = position;
    }

    public void SetSpawner(SpawnerCubes spawnerCubes)
    {
        _spawnerCubes = spawnerCubes;
    }
    
    public void SetHealth(float health)
    {
        _health = health;
        TextUpdate();
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _health -= damage;
        }
        
        if (_health <= 0)
        {
            gameObject.SetActive(false);
            _spawnerCubes.TryFinishLevel();
        }

        TextUpdate();
    }

    private void TextUpdate()
    {
        _textHealth.text = Convert.ToString(Math.Ceiling(_health));
    }
}
