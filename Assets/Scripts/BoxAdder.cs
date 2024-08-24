using JustTanks.Boosts;
using JustTanks.GameLogic;
using UnityEngine;

namespace JustTanks.Gameplay
{
    public class BoxAdder : Boost
    {
        [SerializeField] private int _countAddBoxes;
        [SerializeField] private SpawnerBoxes _spawnerBoxes;

        protected override void SetBoostActivity(bool isBoosted)
        {
            if (isBoosted)
                _spawnerBoxes.AddBoxes(_countAddBoxes);
        }
    }
}
