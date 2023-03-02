using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _delayAnimation;
    [SerializeField] private CanvasComponent _canvas;
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private void Start()
    {
        StartCoroutine(StartNextLevel());
        _spawnerCubes.Generate();
    }

    private void OnDisable()
    {
        StopCoroutine(StartNextLevel());
    }

    public void ShowWin()
    {
        print("Уровень пройден");
    }
    
    private IEnumerator StartNextLevel()
    {
        var waitForDelaySeconds = new WaitForSeconds(_delayAnimation);
        
        _canvas.SetVisibleTextStartLevel(true);
        
        yield return waitForDelaySeconds;
        
        _canvas.SetVisibleTextStartLevel(false);
    }
}
