using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField] private GameObject _startLevelIcon;

    private void Start()
    {
        StartCoroutine(ActivateIconAnimation(_startLevelIcon, 2));
    }

    private void OnDisable()
    {
        StopCoroutine(ActivateIconAnimation(_startLevelIcon));
    }
    
    private IEnumerator ActivateIconAnimation(GameObject icon, float delayAnimation = 0)
    {
        var waitForDelaySeconds = new WaitForSeconds(delayAnimation);
        
        icon.SetActive(true);
        
        yield return waitForDelaySeconds;
            
        icon.SetActive(false);
    }
}
