using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPUIKIT;

public class BoostDamage : MonoBehaviour
{
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private int _multiplierBoostDamage;
    [SerializeField] private MPImage _timerFill;

    private List<GameObject> _cubesPool;
    private bool _isActive = false;
    private float _remainingTime;
    private int[] _normalDamageBullets;
    
    private const float ActivityTime = 30;

    private void Start()
    {
        _timerFill.fillAmount = 0;
        _cubesPool = _spawnerCubes.ShowPool();
    }

    private void Update()
    {
        if (_isActive)
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
                Deactivate();

            UpdateUIField(_remainingTime / ActivityTime);
        }
    }
    
    public void Activate()
    {
        _isActive = true;
        _remainingTime = ActivityTime;
        SetBoostDamage(true);
    }

    private void Deactivate()
    {
        _isActive = false;
        SetBoostDamage(false);
    }

    private void SetBoostDamage(bool isBoosted)
    {
        foreach (var cube in _cubesPool)
        {
            cube.GetComponent<Cube>().SetBoostDamageMode(isBoosted);
        }
    }
    
    private void UpdateUIField(float fillValue)
    {
        _timerFill.fillAmount = fillValue;
    }
}
