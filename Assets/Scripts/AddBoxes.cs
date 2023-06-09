using UnityEngine;

public class AddBoxes : Boost
{
    [SerializeField] private int _countAddBoxes;
    [SerializeField] private SpawnerBoxes _spawnerBoxes;

    protected override void SetBoost(bool isBoosted)
    {
        if(isBoosted)
            _spawnerBoxes.AddBoxes(_countAddBoxes);
    }
}
