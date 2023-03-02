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
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _startLevel;

    private const string _currentLevelText = "Lv";
    private const string _startLevelText = "Start Lv: ";
    
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

    public void UpdateText(int currentLevel)
    {
        _currentLevel.text = _currentLevelText + currentLevel;
        _startLevel.text = _startLevelText + currentLevel;
    }
}
