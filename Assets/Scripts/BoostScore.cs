using MPUIKIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScore : MonoBehaviour
{
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private int _multiplier;
    [SerializeField] private MPImage _timerFill;

    private List<GameObject> _cubesPool;
    private bool _isActive = false;
    private float _remainingTime;

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
        SetBoostScore(true);
    }

    private void Deactivate()
    {
        _isActive = false;
        SetBoostScore(false);
    }

    private void SetBoostScore(bool isBoosted)
    {
        foreach (var cube in _cubesPool)
        {
            cube.GetComponent<Cube>().SetBoostScoreMode(isBoosted);
        }
    }

    private void UpdateUIField(float fillValue)
    {
        _timerFill.fillAmount = fillValue;
    }
}
