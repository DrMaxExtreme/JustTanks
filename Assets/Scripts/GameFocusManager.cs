using UnityEngine;

public class GameFocusManager : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;

    private float _normalTimeScale;

    private void Awake()
    {
        _normalTimeScale = Time.timeScale;
    }

    private void OnApplicationFocus(bool isApplicationHasFocus)
    {
        SetTimeScale(isApplicationHasFocus);
        SetVolumeSound(isApplicationHasFocus);
    }

    private void SetTimeScale(bool isApplicationHasFocus)
    {
        if (isApplicationHasFocus == false)
            Time.timeScale = 0;
        else
            Time.timeScale = _normalTimeScale;
    }

    private void SetVolumeSound(bool isApplicationHasFocus)
    {
        if (isApplicationHasFocus == false)
            _soundManager.SwitchOffSound();
        else
            _soundManager.SwitchOnSound();
    }
}
