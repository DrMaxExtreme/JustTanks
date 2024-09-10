using System;
using JustTanks.Gameplay;

namespace JustTanks.Boosts
{
    public class SlowDownCubes : Boost
    {
        private float _normalSpeed;

        private void Start()
        {
            _normalSpeed = Convert.ToSingle(CubesPool[0].GetComponent<Cube>().Speed);
        }

        protected override void Activate()
        {
            base.Activate();
            SetBoostActivity(true);
        }

        protected override void Deactivate()
        {
            base.Deactivate();
            SetBoostActivity(false);
        }

        protected override void SetBoostActivity(bool isBoosted)
        {
            if(isBoosted)
                SetSpeedAndMaterial(_normalSpeed / Multiplier, true);
            else
                SetSpeedAndMaterial(_normalSpeed, false);
        }

        private void SetSpeedAndMaterial(float speed, bool isSlow)
        {
            foreach (var cube in CubesPool)
            {
                cube.GetComponent<Cube>().SetSpeed(speed, isSlow);
            }
        }
    }
}
