using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _delayAnimation;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private void Start()
    {
        _spawnerCubes.Generate();
    }

    private void OnDisable()
    {
        StopCoroutine(StartNextLevel());
    }

    public void ShowWin()
    {
        print("Уровень пройден");
        StartCoroutine(StartNextLevel());
    }
    
    private IEnumerator StartNextLevel()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayAnimation);
        
        yield return waitForDelaySeconds;
    }
}
