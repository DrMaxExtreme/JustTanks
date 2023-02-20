using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet bullet))
            Destroy(bullet.gameObject);
    }
}
