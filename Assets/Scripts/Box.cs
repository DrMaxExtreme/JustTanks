using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    [SerializeField] private float _delayDestroy;
    [SerializeField] private ParticleSystem _spawnEffect;

    private void Start()
    {
        StartCoroutine(DelayOpen());
        Instantiate(_spawnEffect, transform.position, Quaternion.identity, null);
    }

    private void OnDisable()
    {
        StopCoroutine(DelayOpen());
    }

    private IEnumerator DelayOpen()
    {
        float delaySeconds = _delayDestroy;
        var waitForDelaySeconds = new WaitForSeconds(delaySeconds);

        yield return waitForDelaySeconds;

        Destroy(gameObject);
    }
}
