using MPUIKIT;

namespace JustTanks.Boosts
{
    public class BoostTimer
    {
        private float _remainingTime;
        private MPImage _timerFill;

        public BoostTimer(MPImage timerFill)
        {
            _timerFill = timerFill;
            ResetTimer();
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

        public bool IsTimeOver() => _remainingTime <= 0;

        private void UpdateUIField(float fillValue)
        {
            if (_timerFill != null)
                _timerFill.fillAmount = fillValue;
        }
    }
}
