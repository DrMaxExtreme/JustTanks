using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    [SerializeField] private ParticleSystem _spawnEffect;

    private void Start()
    {
        Instantiate(_spawnEffect, transform.position, Quaternion.identity, null);
    }
}
