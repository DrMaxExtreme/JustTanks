using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Button _soundOff;
    [SerializeField] private Button _soundOn;
    [SerializeField] private float _soundOnValue;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private string _masterMixerName = "MasterVolume";
    private string _SoundMixerName = "SoundVolume";
    private string _MusicMixerName = "MusicVolume";

    private const float SoundMinValue = -30;
    private const float SoundOffValue = -80;

    private void Start()
    {
        _mixer.audioMixer.SetFloat(_masterMixerName, _soundOnValue);
    }

    public void SwitchOffSound()
    {
        Switch(SoundOffValue, false, true);
    }
    
    public void SwitchOnSound()
    {
        Switch(_soundOnValue, true, false);
    }

    public void SetVolumeSound()
    {
        _mixer.audioMixer.SetFloat(_SoundMixerName, GetVolume(_soundSlider.value));
    }

    public void SetVolumeMusic()
    {
        _mixer.audioMixer.SetFloat(_MusicMixerName, GetVolume(_musicSlider.value));
    }

    private void Switch(float soundOffValue, bool isActiveOffButton, bool isActiveOnButton)
    {
        _mixer.audioMixer.SetFloat(_masterMixerName, soundOffValue);
        _soundOff.gameObject.SetActive(isActiveOffButton);
        _soundOn.gameObject.SetActive(isActiveOnButton);
    }

    private float GetVolume(float sliderValue)
    {
        if(sliderValue == 0)
            return SoundOffValue;

        return Mathf.Lerp(SoundMinValue, 0, sliderValue);
    }
}
