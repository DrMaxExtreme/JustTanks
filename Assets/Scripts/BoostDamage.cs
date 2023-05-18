public class BoostDamage : Boost
{
    protected override void SetBoost(bool isBoosted)
    {
        foreach (var cube in CubesPool)
        {
            cube.GetComponent<Cube>().SetBoostDamageMode(isBoosted);
        }
    }
}
