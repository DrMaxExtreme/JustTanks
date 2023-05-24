using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandMover : MonoBehaviour
{
    [SerializeField] private RawImage _land;

    private float _positionY;
    
    private const float Speed = 0.0275f;
    
    private void Update()
    {
        _positionY += Speed * Time.deltaTime;

        if (_positionY > 1)
            _positionY = 0;
        
        _land.uvRect = new Rect(0, _positionY, _land.uvRect.width, _land.uvRect.height);
    }
}
