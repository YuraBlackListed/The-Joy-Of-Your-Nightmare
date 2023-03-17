using UnityEngine;
using UnityEngine.Audio;

public class AudioMixersController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private SettingsScrObj scriptableOnject;

    private void FixedUpdate() 
    {
        SetMixerVolume(mixer, "MasterVolume", scriptableOnject.masterVolume);
        SetMixerVolume(mixer, "MusicVolume", scriptableOnject.musicVolume);
        SetMixerVolume(mixer, "OtherSoundsVolume", scriptableOnject.otherVolume);
    }
    private void SetMixerVolume(AudioMixer _mixer, string parameterName, float value)
    {
        _mixer.SetFloat(parameterName, value);
    }
}
