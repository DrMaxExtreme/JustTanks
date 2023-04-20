using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _textHealths;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private ParticleSystem _dieEffect;
    [SerializeField] private int _scoreForDestroy;

    private int _health;
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
    
    public void SetHealth(int health)
    {
        _health = health;
        TextUpdate();
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _health -= damage;
        }
        
        if (_health <= 0)
        {
            Instantiate(_dieEffect, transform.position, Quaternion.Euler(90,transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), null);
            gameObject.SetActive(false);
            _spawnerCubes.TryFinishLevel();
            _spawnerCubes.TakeScore(_scoreForDestroy);
        }

        _spawnerCubes.TakeScore(damage);
        TextUpdate();
    }

    private void TextUpdate()
    {
        foreach (var textHealth in _textHealths)
        {
            textHealth.text = Convert.ToString(_health);
        }
    }
}
