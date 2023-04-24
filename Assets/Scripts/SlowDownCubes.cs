using System;
using MPUIKIT;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownCubes : Boost
{
    private float _normalSpeed;

    private const float ActivityTime = 30;

    protected override void Start()
    {
        base.Start();
        _normalSpeed = Convert.ToSingle(CubesPool[0].GetComponent<Cube>().Speed);
    }

    protected override void Activate()
    {
        base.Activate();
        SetSpeedAndMaterial(_normalSpeed / _multiplier, true);
    }

    protected override void Deactivate()
    {
        base.Deactivate();
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
