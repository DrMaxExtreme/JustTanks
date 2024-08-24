using JustTanks.Gameplay;

namespace JustTanks.Boosts
{
    public class BoostDamage : Boost
    {
        protected override void SetBoostActivity(bool isBoosted)
        {
            foreach (var cube in CubesPool)
            {
                cube.GetComponent<Cube>().SetBoostDamageMode(isBoosted);
            }
        }
    }
}
