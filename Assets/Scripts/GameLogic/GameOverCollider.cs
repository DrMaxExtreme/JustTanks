using JustTanks.Gameplay;
using UnityEngine;
using System;

namespace JustTanks.GameLogic
{
    public class GameOverCollider : MonoBehaviour
    {
        public event Action onReached;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Cube cube))
            {
                onReached?.Invoke();
            }
        }
    }
}
