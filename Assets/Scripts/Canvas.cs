using System;
using System.Collections;
using System.Collections.Generic;
using MPUIKIT;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField] private GameObject _startLevelIcon;
    
    public void SetVisibleTextStartLevel(bool isVisible)
    {
        _startLevelIcon.SetActive(isVisible);
    }
}
