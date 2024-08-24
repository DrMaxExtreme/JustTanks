using JustTanks.GameLogic;
using System;
using TMPro;
using UnityEngine;

namespace JustTanks.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cube : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _textHealths;
        [SerializeField] private float _speed;
        [SerializeField] private float _distance;
        [SerializeField] private ParticleSystem _dieEffect;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private int _multiplierBoostScore;
        [SerializeField] private Material _normalMaterial;
        [SerializeField] private Material _heavyMaterial;
        [SerializeField] private Material _slowdownMaterial;

        private int _health;
        private Vector3 _targetPosition;
        private SpawnerCubes _spawnerCubes;

        private bool _isBoostedDamage = false;
        private bool _isBoostedScore = false;
        private bool _isSlowdownSpeed = false;
        private bool _isHeavy = false;

        private int _multiplierHeavy = 5;

        public float Speed => _speed;
        public bool IsSlowdownSpeed => _isSlowdownSpeed;

        private void FixedUpdate()
        {
            var position = transform.position;
            _targetPosition = new Vector3(position.x, position.y, position.z - _distance);
            position = Vector3.MoveTowards(position, _targetPosition, _speed);
            transform.position = position;
        }

        private void OnEnable()
        {
            UpdateMaterial();
        }

        public void SetSpawner(SpawnerCubes spawnerCubes)
        {
            _spawnerCubes = spawnerCubes;
        }

        public void SetHealth(int health)
        {
            _health = health;
            TextUpdate();
        }

        public void SetBoostDamageMode(bool isBoosted)
        {
            _isBoostedDamage = isBoosted;
        }

        public void SetBoostScoreMode(bool isBoosted)
        {
            _isBoostedScore = isBoosted;
        }

        public void TakeDamage(int damage)
        {
            if (_isBoostedDamage)
                damage += damage;

            if (damage > 0)
            {
                if (damage > _health)
                    GetScore(_health);
                else
                    GetScore(damage);

                _health -= damage;
            }

            if (_health <= 0)
            {
                Instantiate(_dieEffect, transform.position, Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), null);
                _isHeavy = false;
                gameObject.SetActive(false);
                _spawnerCubes.TryFinishLevel();
            }

            TextUpdate();
        }

        public void SetSpeed(float speed, bool isSlowdownSpeed)
        {
            _speed = speed;
            _isSlowdownSpeed = isSlowdownSpeed;

            UpdateMaterial();
        }

        public void SetMaterial(Material material)
        {
            if (_isHeavy == false)
                _renderer.material = material;
        }

        public void SetHeavyType(bool isHeavy)
        {
            _isHeavy = isHeavy;

            if (_isHeavy)
                _health *= _multiplierHeavy;

            UpdateMaterial();
            TextUpdate();
        }

        private void GetScore(int score)
        {
            if (_isBoostedScore)
                score *= _multiplierBoostScore;

            _spawnerCubes.TakeScore(score);
        }

        private void TextUpdate()
        {
            foreach (var textHealth in _textHealths)
            {
                textHealth.text = Convert.ToString(_health);
            }
        }

        private void UpdateMaterial()
        {
            if (_isHeavy)
                _renderer.material = _heavyMaterial;
            else if (_isSlowdownSpeed)
                _renderer.material = _slowdownMaterial;
            else
                _renderer.material = _normalMaterial;
        }
    }
}
