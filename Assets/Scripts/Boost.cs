using System;
using System.Collections.Generic;
using UnityEngine;
using MPUIKIT;

public class Boost : MonoBehaviour
{
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private MPImage _timerFill;
    [SerializeField] protected int _multiplier;

    private float _remainingTime;
    private bool _isActive = false;
    
    protected List<GameObject> CubesPool;
    
    private const float ActivityTime = 60;
    
    protected virtual void Start()
    {
        ResetTimer();
        CubesPool = _spawnerCubes.ShowPool();
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

    public void ResetTimer()
    {
        _remainingTime = 0;
        UpdateUIField(_remainingTime);
    }

    protected virtual void Activate()
    {
        _isActive = true;
        _remainingTime = ActivityTime;
        SetBoost(true);
    }

    protected virtual void Deactivate()
    {
        _isActive = false;
        SetBoost(false);
    }

    protected virtual void SetBoost(bool isBoosted)
    {
        
    }

    private void UpdateUIField(float fillValue)
    {
        _timerFill.fillAmount = fillValue;
    }
}
