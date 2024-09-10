using MPUIKIT;

namespace JustTanks.Boosts
{
    public class BoostTimer : IBoostTimer
    {
        private MPImage _timerFill;
        private float _remainingTime;

        public BoostTimer(MPImage timerFill)
        {
            _timerFill = timerFill;
            _remainingTime = 0;
        }

        public void ResetTimer()
        {
            _remainingTime = 0;
            UpdateUIField(_remainingTime);
        }

        public void UpdateTimer(float deltaTime, float activityTime)
        {
            _remainingTime -= deltaTime;
            UpdateUIField(_remainingTime / activityTime);
        }

        public bool IsTimeOver()
        {
            return _remainingTime <= 0;
        }

        private void UpdateUIField(float fillValue)
        {
            if (_timerFill != null)
                _timerFill.fillAmount = fillValue;
        }
    }
}
