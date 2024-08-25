using UnityEngine;

namespace JustTanks.GameLogic
{
    public class GameStateManager
    {
        private int _currentScore;
        private int _recordScore;
        private float _currentPowerActiveTanks;

        public GameStateManager()
        {
            _currentScore = 0;
            _recordScore = PlayerPrefs.GetInt("RecordScorePrefs");
            _currentPowerActiveTanks = 0;
        }

        public void AddScore(int score)
        {
            _currentScore += score;

            if (_currentScore > _recordScore)
            {
                _recordScore = _currentScore;
                PlayerPrefs.SetInt("RecordScorePrefs", _recordScore);
            }
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public int GetRecordScore()
        {
            return _recordScore;
        }

        public void ResetScore()
        {
            _currentScore = 0;
        }

        public void AddPowerToTanks(float power)
        {
            _currentPowerActiveTanks += power;
        }

        public float GetCurrentPowerActiveTanks()
        {
            return _currentPowerActiveTanks;
        }

        public void ResetCurrentPowerActiveTanks()
        {
            _currentPowerActiveTanks = 0;
        }

        public void SaveRecordLevel(int level)
        {
            PlayerPrefs.SetInt("RecordLevelPrefs", level);
        }

        public int LoadRecordLevel()
        {
            return PlayerPrefs.GetInt("RecordLevelPrefs");
        }
    }
}
