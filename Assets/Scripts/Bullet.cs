using UnityEngine;

namespace JustTanks.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _lifeEffect;

        private Vector3 _targetPoint;
        private ParticleSystem _particle;

        public int Damage => _damage;

        public void Destroy()
        {
            Destroy(gameObject);
            Destroy(_particle.gameObject);
        }

        public void GetTargetTransform(Vector3 targetTransform)
        {
            _targetPoint = targetTransform;
            transform.LookAt(_targetPoint);
        }

        public int ShowDamage()
        {
            int damage = _damage;
            return damage;
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        private void Start()
        {
            _particle = Instantiate(_lifeEffect, transform.position, Quaternion.identity, null);
        }

        private void FixedUpdate()
        {
            if (_particle != null)
                Move();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Cube cube))
            {
                if (cube != null)
                    cube.TakeDamage(_damage);

                Destroy();
            }
        }

        private void Move()
        {
            Vector3 targetPosition = Vector3.MoveTowards(transform.position, _targetPoint, _speed);

            transform.position = targetPosition;
            _particle.transform.position = targetPosition;

            if (transform.position == _targetPoint)
                Destroy();
        }
    }
}
