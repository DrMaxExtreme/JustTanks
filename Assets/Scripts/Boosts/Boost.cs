using Agava.YandexGames;
using JustTanks.GameLogic;
using MPUIKIT;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JustTanks.Boosts
{
    public class Boost : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private GameFocusManager _gameFocusManager;
        [SerializeField] private SpawnerCubes _spawnerCubes;
        [SerializeField] private MPImage _timerFill;
        [SerializeField] private int _multiplier;

        private float _remainingTime;

        private const float ActivityTime = 60;

        private List<GameObject> _cubesPool;

        protected int Multiplier
        {
            get { return _multiplier; }
            set { _multiplier = value; }
        }

        protected List<GameObject> CubesPool
        {
            get { return _cubesPool; }
            set { _cubesPool = value; }
        }

        public void ResetTimer()
        {
            _remainingTime = 0;
            UpdateUIField(_remainingTime);
        }

        public void ShowAd()
        {
            if (_remainingTime <= 0)
                VideoAd.Show(PauseGame, Activate, ContinueGame);
        }

        protected virtual void Start()
        {
            ResetTimer();
            CubesPool = _spawnerCubes.ShowPool();
        }

        protected virtual void Activate()
        {
            _remainingTime = ActivityTime;
            SetBoostActivity(true);
        }

        protected virtual void Deactivate()
        {
            SetBoostActivity(false);
        }

        protected virtual void SetBoostActivity(bool isBoosted)
        {

        }

        private void Update()
        {
            if (_levelController.IsPauseBoost == false)
                _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
                Deactivate();

            UpdateUIField(_remainingTime / ActivityTime);
        }

        private void UpdateUIField(float fillValue)
        {
            if (_timerFill != null)
                _timerFill.fillAmount = fillValue;
        }

        private void PauseGame()
        {
            _gameFocusManager.SetOpenAdMarker(true);
        }

        private void ContinueGame()
        {
            _gameFocusManager.SetOpenAdMarker(false);
        }
    }
}
