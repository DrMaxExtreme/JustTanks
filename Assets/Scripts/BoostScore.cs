using JustTanks.Gameplay;

namespace JustTanks.Boosts
{
    public class BoostScore : Boost
    {
        protected override void SetBoostActivity(bool isBoosted)
        {
            foreach (var cube in CubesPool)
            {
                cube.GetComponent<Cube>().SetBoostScoreMode(isBoosted);
            }
        }
    }
}
