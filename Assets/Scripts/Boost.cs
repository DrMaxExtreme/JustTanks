using System.Collections.Generic;
using UnityEngine;
using MPUIKIT;
using Agava.YandexGames;

public class Boost : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameFocusManager _gameFocusManager;
    [SerializeField] private SpawnerCubes _spawnerCubes;
    [SerializeField] private MPImage _timerFill;
    [SerializeField] protected int _multiplier;

    private float _remainingTime;
    
    protected List<GameObject> CubesPool;
    
    private const float ActivityTime = 60;
    
    protected virtual void Start()
    {
        ResetTimer();
        CubesPool = _spawnerCubes.ShowPool();
    }

    private void Update()
    {
        if (_levelManager.IsPauseBoost == false)
            _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0)
            Deactivate();
        
        UpdateUIField(_remainingTime / ActivityTime);
    }

    public void ResetTimer()
    {
        _remainingTime = 0;
        UpdateUIField(_remainingTime);
    }

    public void ShowAd()
    {
        if (_remainingTime <= 0)
            VideoAd.Show(PauseGame, Activate, ContinueGame);
    }

    protected virtual void Activate()
    {
        _remainingTime = ActivityTime;
        SetBoost(true);
    }

    protected virtual void Deactivate()
    {
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

    private void PauseGame()
    {
        _gameFocusManager.SetOpenAdMarker(true);
    }

    private void ContinueGame()
    {
        _gameFocusManager.SetOpenAdMarker(false);
    }
}
