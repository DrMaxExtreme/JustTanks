using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLife : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private float _delayDestroy;
    
    private void Start()
    {
        _delayDestroy = _particle.duration;
        StartCoroutine(DelayDie());
    }

    private void OnDisable()
    {
        StopCoroutine(DelayDie());
    }

    private IEnumerator DelayDie()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayDestroy);

        yield return waitForDelaySeconds;

        Destroy(gameObject);
    }
}
