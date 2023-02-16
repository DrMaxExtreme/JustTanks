using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Box : MonoBehaviour
{
    [SerializeField] private Tank _tankPrefab;
    [SerializeField] private float _delayDestroy;

    private void Start()
    {
        var boxCollider = GetComponent<BoxCollider>();
        SetPreferences(boxCollider);

        StartCoroutine(DelayOpen());
    }

    private void OnDisable()
    {
        StopCoroutine(DelayOpen());
    }

    private IEnumerator DelayOpen()
    {
        float delaySeconds = _delayDestroy;
        var WaitForDelaySeconds = new WaitForSeconds(delaySeconds);

        yield return WaitForDelaySeconds;

        Destroy(gameObject);
    }

    private void SetPreferences(BoxCollider boxCollider)
    {
        boxCollider.isTrigger = true;
    }
}
