using System.Collections.Generic;
using UnityEngine;
using MPUIKIT;
using Agava.YandexGames;

public class Boost : MonoBehaviour
{
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private MPImage _timerFill;
    [SerializeField] protected int _multiplier;

    private float _remainingTime;
    private float _normalTimeScale;
    private bool _isActive = false;
    
    protected List<GameObject> CubesPool;
    
    private const float ActivityTime = 60;
    
    protected virtual void Start()
    {
        ResetTimer();
        _normalTimeScale = Time.timeScale;
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

    public void SetPauseMode(bool isPause)
    {
        _isActive = isPause;
    }

    public void ShowAd()
    {
        VideoAd.Show(onOpenCallback: Pause, onRewardedCallback: Activate, onCloseCallback: Continue);
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
        if(_timerFill != null)
            _timerFill.fillAmount = fillValue;
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void Continue()
    {
        Time.timeScale = _normalTimeScale;
    }
}
