using System;
using System.Collections;
using System.Collections.Generic;
using MPUIKIT;
using UnityEngine;
using TMPro;

public class CanvasComponent : MonoBehaviour
{
    [SerializeField] private GameObject _startLevelLabel;
    [SerializeField] private GameObject _startGameIcon;
    [SerializeField] private GameObject _continueGameIcon;
    [SerializeField] private GameObject _gameOverIcon;
    [SerializeField] private GameObject _newTankIcon;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _startLevel;
    [SerializeField] private MPImage _timerFill;
    [SerializeField] private TMP_Text _countBoxes;

    private const string _currentLevelText = "Lv";
    private const string _startLevelText = "Start Lv: ";
    private const string _spawnedBoxes = "x";
    
    public void SetVisibleStartLevelLabel(bool isVisible)
    {
        _startLevelLabel.SetActive(isVisible);
    }
    
    public void SetVisibleStartGameIcon(bool isVisible)
    {
        _startGameIcon.SetActive(isVisible);
    }
    
    public void SetVisibleContinueGameIcon(bool isVisible)
    {
        _continueGameIcon.SetActive(isVisible);
    }

    public void UpdateTextLevel(int currentLevel)
    {
        _currentLevel.text = _currentLevelText + currentLevel;
        _startLevel.text = _startLevelText + currentLevel;
    }

    public void UpdateTextCountBoxes(int currentBoxes, float fillValue)
    {
        _countBoxes.text = _spawnedBoxes + currentBoxes;
        _timerFill.fillAmount = fillValue;
    }

    public void SetVisibleGameOverIcon(bool isVisible)
    {
        _gameOverIcon.SetActive(isVisible);
    }

    public void SetVisibleNewTankIcon(bool isVisible)
    {
        _newTankIcon.SetActive(isVisible);
    }
}
