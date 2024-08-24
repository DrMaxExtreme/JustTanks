using JustTanks.Audio;
using UnityEngine;

namespace JustTanks.GameLogic
{
    public class GameFocusManager : MonoBehaviour
    {
        private const float SoundOffValue = -100;

        [SerializeField] private SoundManager _soundManager;

        private float _currentTimeScale;
        private float _currentValueSound;
        private bool _isPause = false;
        private bool _isOpenAd = false;

        private void Awake()
        {
            _currentTimeScale = Time.timeScale;
        }

        private void Start()
        {
            _currentValueSound = _soundManager.GetMixerValue();
        }

        public void SwitchPauseGame(bool isPause)
        {
            if (_isOpenAd)
                isPause = true;

            if (isPause != _isPause)
            {
                _isPause = isPause;

                if (_isPause)
                {
                    _currentTimeScale = Time.timeScale;
                    _currentValueSound = _soundManager.GetMixerValue();
                }

                SetTimeScale();
                SetVolumeSound();
            }
        }

        public void SetOpenAdMarker(bool isOpenAd)
        {
            _isOpenAd = isOpenAd;
            SwitchPauseGame(_isOpenAd);
        }

        private void OnApplicationFocus(bool isApplicationHasFocus)
        {
            SwitchPauseGame(!isApplicationHasFocus);
        }

        private void SetTimeScale()
        {
            if (_isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = _currentTimeScale;
        }

        private void SetVolumeSound()
        {
            _soundManager.SetSoundValueMixer(_isPause ? SoundOffValue : _currentValueSound);
        }
    }
}
