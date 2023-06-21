using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _sfxSlider;

    const string _sfxMixerParam = "SFXVolume";

    private void Awake()
    {
        _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXSliderValue", 1f);

        if (_sfxSlider.value != 1f)
        {
            _mixer.SetFloat(_sfxMixerParam, Mathf.Log10(_sfxSlider.value) * 20);
            Debug.Log("Loaded Volume Value");
        }
    }

    void SetSFXVolume(float value)
    {
        _mixer.SetFloat(_sfxMixerParam, Mathf.Log10(value) * 20);
        
        PlayerPrefs.SetFloat("SFXSliderValue", _sfxSlider.value);
    }
}
