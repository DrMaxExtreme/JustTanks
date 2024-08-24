using Agava.YandexGames;
using JustTanks.GameLogic;
using MPUIKIT;
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

        private BoostTimer _boostTimer;
        private AdManager _adManager;

        private const float ActivityTime = 60;

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

        protected virtual void Start()
        {
            _boostTimer = new BoostTimer(_timerFill);
            _adManager = new AdManager(_gameFocusManager);

            CubesPool = _spawnerCubes.ShowPool();
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

        protected virtual void SetBoostActivity(bool isBoosted)
        {

        }
    }
}
