using System.Collections.Generic;
using UnityEngine;

public class BoostScore : Boost
{
    protected override void SetBoost(bool isBoosted)
    {
        foreach (var cube in CubesPool)
        {
            cube.GetComponent<Cube>().SetBoostScoreMode(isBoosted);
        }
    }
}
