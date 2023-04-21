using MPUIKIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownCubes : MonoBehaviour
{
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private int _multiplier;
    [SerializeField] private MPImage _timerFill;
    [SerializeField] private Material _normalSpeedMaterial;
    [SerializeField] private Material _slowSpeedMaterial;

    private List<GameObject> _cubesPool;
    private bool _isActive = false;
    private float _remainingTime;
    private float _normalSpeed;

    private const float ActivityTime = 30;

    private void Start()
    {
        _timerFill.fillAmount = 0;
        _cubesPool = _spawnerCubes.ShowPool();
        _normalSpeed = _cubesPool[0].GetComponent<Cube>().Speed;
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
        SetSpeedAndMaterial(_normalSpeed / _multiplier, _slowSpeedMaterial);
    }

    private void Deactivate()
    {
        _isActive = false;
        SetSpeedAndMaterial(_normalSpeed, _normalSpeedMaterial);
    }

    private void SetSpeedAndMaterial(float speed, Material material)
    {
        foreach (var cube in _cubesPool)
        {
            cube.GetComponent<Cube>().SetSpeed(speed);
            cube.GetComponent<Cube>().SetMaterial(material);
        }
    }

    private void UpdateUIField(float fillValue)
    {
        _timerFill.fillAmount = fillValue;
    }
}
