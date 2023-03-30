using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverCollider : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            _reached?.Invoke();
        }
    }
}
