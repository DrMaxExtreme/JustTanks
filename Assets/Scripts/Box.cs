using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    [SerializeField] private float _delayDestroy;

    private void Start()
    {
        StartCoroutine(DelayOpen());
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
