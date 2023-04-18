using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Button _soundOff;
    [SerializeField] private Button _soundOn;
    [SerializeField] private float _soundOnValue;

    private string _masterMixerName = "MasterVolume";
    private float _soundOffValue = -80;

    private void Start()
    {
        _mixer.audioMixer.SetFloat(_masterMixerName, _soundOnValue);
    }

    public void SwitchOffSound()
    {
        Switch(_soundOffValue, false, true);
    }
    
    public void SwitchOnSound()
    {
        Switch(_soundOnValue, true, false);
    }

    private void Switch(float soundOffValue, bool isActiveOffButton, bool isActiveOnButton)
    {
        _mixer.audioMixer.SetFloat(_masterMixerName, soundOffValue);
        _soundOff.gameObject.SetActive(isActiveOffButton);
        _soundOn.gameObject.SetActive(isActiveOnButton);
    }
}
