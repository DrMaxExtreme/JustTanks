using JustTanks.GameLogic;
using MPUIKIT;
using System.Collections.Generic;
using UnityEngine;

namespace JustTanks.Boosts
{
    public abstract class Boost : MonoBehaviour
    {
        private const float ActivityTime = 60;

        [SerializeField] private GameFocusManager _gameFocusManager;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private SpawnerCubes _spawnerCubes;
        [SerializeField] private MPImage _timerFill;
        [SerializeField] private int _multiplier;

        private BoostTimer _boostTimer;
        private AdManager _adManager;

        private List<GameObject> _cubesPool;

        protected List<GameObject> CubesPool
        {
            get { return _cubesPool; }
            set { _cubesPool = value; }
        }

        protected int Multiplier
        {
            get { return _multiplier; }
            set { _multiplier = value; }
        }

        private void Awake()
        {
            CubesPool = _spawnerCubes.ShowPool();
            _boostTimer = new BoostTimer(_timerFill);
            _adManager = new AdManager(_gameFocusManager);
        }

        public void ResetTimer()
        {
            _boostTimer.ResetTimer();
        }

        public void ShowAd()
        {
            if (_boostTimer.IsTimeOver())
                _adManager.ShowAd();
        }

        protected virtual void Update()
        {
            if (!_levelController.IsPauseBoost)
                _boostTimer.UpdateTimer(Time.deltaTime, ActivityTime);

            if (_boostTimer.IsTimeOver())
                Deactivate();
        }

        protected virtual void Activate()
        {
            _boostTimer.ResetTimer();
            _boostTimer.UpdateTimer(ActivityTime, ActivityTime);
            SetBoostActivity(true);
        }

        protected virtual void Deactivate()
        {
            SetBoostActivity(false);
        }

        protected abstract void SetBoostActivity(bool isBoosted);

    }
}
