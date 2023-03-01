using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private void Start()
    {
        _spawnerCubes.Generate();
    }

    public void ShowWin()
    {
        print("Победа");
    }
}
