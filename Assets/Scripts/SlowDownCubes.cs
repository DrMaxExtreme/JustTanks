using System;
using MPUIKIT;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownCubes : Boost
{
    [SerializeField] private Material _normalSpeedMaterial;
    [SerializeField] private Material _slowSpeedMaterial;

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
        SetSpeedAndMaterial(_normalSpeed / _multiplier, _slowSpeedMaterial);
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        SetSpeedAndMaterial(_normalSpeed, _normalSpeedMaterial);
    }

    private void SetSpeedAndMaterial(float speed, Material material)
    {
        foreach (var cube in CubesPool)
        {
            cube.GetComponent<Cube>().SetSpeed(speed);
            cube.GetComponent<Cube>().SetMaterial(material);
        }
    }
}
