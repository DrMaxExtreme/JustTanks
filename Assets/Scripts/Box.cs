using UnityEngine;

namespace JustTanks.Gameplay
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _spawnEffect;

        private void Start()
        {
            Instantiate(_spawnEffect, transform.position, Quaternion.identity, null);
        }
    }
}
