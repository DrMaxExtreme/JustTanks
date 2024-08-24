using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace JustTanks.Audio
{
    public class SoundManager : MonoBehaviour
    {
        private const float SoundMinValue = -30;
        private const float SoundOffValue = -80;

        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Button _soundOff;
        [SerializeField] private Button _soundOn;
        [SerializeField] private float _soundOnValue;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        private string _masterMixerName = "MasterVolume";
        private string _SoundMixerName = "SoundVolume";
        private string _MusicMixerName = "MusicVolume";

        private float _currentValueMasterMixer;

        private void Start()
        {
            SetSoundValueMixer(_soundOnValue);
        }

        public void SetSoundValueMixer(float value)
        {
            _mixer.audioMixer.SetFloat(_masterMixerName, value);
        }

        public void SwitchOffSound()
        {
            SwitchMasterMixer(SoundOffValue, false, true);
        }

        public void SwitchOnSound()
        {
            SwitchMasterMixer(_soundOnValue, true, false);
        }

        public void SetVolumeSound()
        {
            _mixer.audioMixer.SetFloat(_SoundMixerName, GetVolume(_soundSlider.value));
        }

        public void SetVolumeMusic()
        {
            _mixer.audioMixer.SetFloat(_MusicMixerName, GetVolume(_musicSlider.value));
        }

        public float GetMixerValue()
        {
            return _currentValueMasterMixer;
        }

        private void SwitchMasterMixer(float soundValue, bool isActiveOffButton, bool isActiveOnButton)
        {
            _mixer.audioMixer.SetFloat(_masterMixerName, soundValue);
            _soundOff.gameObject.SetActive(isActiveOffButton);
            _soundOn.gameObject.SetActive(isActiveOnButton);
            _currentValueMasterMixer = soundValue;
        }

        private float GetVolume(float sliderValue)
        {
            if (sliderValue == 0)
                return SoundOffValue;

            return Mathf.Lerp(SoundMinValue, 0, sliderValue);
        }
    }
}
