namespace JustTanks.Boosts
{
    public interface IBoostTimer
    {
        void ResetTimer();
        void UpdateTimer(float deltaTime, float activityTime);
        bool IsTimeOver();
    }
}