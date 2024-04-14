using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterVolSlider, _musicVolSlider, _sfxVolSlider;


    void Start()
    {
        SetSoundLevelsBasedOnSliders(); // should be around 50 % of volume at start
    }

    private void SetSoundLevelsBasedOnSliders()
    {
        AudioSetMasterVol(_masterVolSlider.value);
        AudioSetMusicVol(_musicVolSlider.value);
        AudioSetSfxVol(_sfxVolSlider.value);
    }

    public void AudioSetMasterVol(float value)
    {
        _audioMixer.SetFloat("VolumeOfMaster", Mathf.Log10(value) * 20);
    }

    public void AudioSetMusicVol(float value)
    {
        _audioMixer.SetFloat("VolumeOfMusic", Mathf.Log10(value) * 20);
    }

    public void AudioSetSfxVol(float value)
    {
        _audioMixer.SetFloat("VolumeOfSfx", Mathf.Log10(value) * 20);
    }

}
