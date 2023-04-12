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
    [SerializeField] private TMP_Text _newTankLevel;
    [SerializeField] private TMP_Text _newTankPower;
    [SerializeField] private MPImage _tankRender;

    private const string _levelText = "Lv";
    private const string _startLevelText = "Start Lv: ";
    private const string _spawnedBoxesText = "x";
    
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
        _currentLevel.text = _levelText + currentLevel;
        _startLevel.text = _startLevelText + currentLevel;
    }

    public void UpdateTextCountBoxes(int currentBoxes, float fillValue)
    {
        _countBoxes.text = _spawnedBoxesText + currentBoxes;
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

    public void SetTextFeaturesNewTank(int levelNewTank, float powerNewTank)
    {
        _newTankLevel.text = _levelText + levelNewTank;
        _newTankPower.text = powerNewTank.ToString();
    }

    public void SetRenderNewTank(Sprite render)
    {
        _tankRender.sprite = render;
    }
}
