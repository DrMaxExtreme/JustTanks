using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BulletDestroyer : MonoBehaviour
{
    private Collider _triggerCollider;

    private void Start()
    {
        _triggerCollider = GetComponent<Collider>();
        _triggerCollider.isTrigger = true;
    }

    public void Activate()
    {
        var bounds = _triggerCollider.bounds;
        
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;

            if (obj.GetComponent<Bullet>() != null)
            {
                obj.GetComponent<Bullet>().Destroy();
            }
        }
    }
}
