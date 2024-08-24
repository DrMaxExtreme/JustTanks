using JustTanks.Gameplay;
using JustTanks.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustTanks.GameLogic
{
    public class SpawnerBoxes : MonoBehaviour
    {
        private readonly List<Cell> _freeCells = new List<Cell>();

        [SerializeField] private List<Cell> _allCells;
        [SerializeField] private float _delayBetweenActivate;
        [SerializeField] private CanvasComponent _canvasComponent;

        private bool _isActive = true;
        private float _remainingDelay;
        private int _numberOfBoxes;
        private float _delaySpawned = 0.03f;

        private void Start()
        {
            UpdateUIIconBoxes();
        }

        private void Update()
        {
            if (_numberOfBoxes == 0)
            {
                _isActive = false;
                UpdateUIIconBoxes();
                _remainingDelay = 0;
            }

            if (_isActive)
            {
                StartCoroutine(Spawned());
            }
        }

        private void OnDisable()
        {
            Stop();
        }

        public void AddBoxes(int numberOfBoxes)
        {
            _numberOfBoxes += numberOfBoxes;
            _canvasComponent.UpdateTextCountBoxes(_numberOfBoxes);
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Stop()
        {
            _isActive = false;
        }

        public void ResetCount()
        {
            _numberOfBoxes = 0;
            _remainingDelay = 0;
            _isActive = true;
        }

        private bool TryFindFreeCell()
        {
            _freeCells.Clear();

            foreach (var cell in _allCells.Where(cell => cell.TryFindHaveObject() == false))
            {
                _freeCells.Add(cell);
            }

            return _freeCells.Count > 0;
        }

        private void GenerateBox()
        {
            _freeCells[Random.RandomRange(0, _freeCells.Count)].InstantiateBox();
            _numberOfBoxes--;
        }

        private void UpdateUIIconBoxes()
        {
            float normalazeDelay = _remainingDelay / _delayBetweenActivate;
            _canvasComponent.UpdateTextCountBoxes(_numberOfBoxes);
            _canvasComponent.UpdateFillDelaySpawnBoxes(normalazeDelay);
        }

        private IEnumerator Spawned()
        {
            var waitForDelaySeconds = new WaitForSeconds(_delaySpawned);

            _remainingDelay -= Time.deltaTime;

            yield return waitForDelaySeconds;

            UpdateUIIconBoxes();

            if (TryFindFreeCell() && _numberOfBoxes > 0 && _remainingDelay <= 0 && _isActive)
            {
                GenerateBox();
                _remainingDelay = _delayBetweenActivate;
            }
        }
    }
}
