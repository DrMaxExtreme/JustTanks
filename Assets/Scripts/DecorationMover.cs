using UnityEngine;

namespace JustTanks.Decorations
{
    public class DecorationMover : MonoBehaviour
    {
        private const float _deadTime = 225;

        [SerializeField] private float _speed = 0.0055f;
        [SerializeField] private float _distance = 1;

        private Vector3 _targetPosition;

        private float _lifeTime = 0;

        private void FixedUpdate()
        {
            var position = transform.position;
            _targetPosition = new Vector3(position.x, position.y, position.z - _distance);
            position = Vector3.MoveTowards(position, _targetPosition, _speed);
            transform.position = position;

            _lifeTime += Time.deltaTime;

            if (_lifeTime > _deadTime)
                Destroy(gameObject);
        }
    }
}
