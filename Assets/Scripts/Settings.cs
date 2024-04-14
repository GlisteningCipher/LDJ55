using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterVolSlider, _musicVolSlider, _sfxVolSlider;

    public static Settings Instance;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
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

    public void MuffleMusic()
    {
        _audioMixer.DOSetFloat("LowpassCutoffFreq", 500, 0.5f);
    }

    public void UnMuffleMusic()
    {
        _audioMixer.DOSetFloat("LowpassCutoffFreq", 5000, 0.5f);
    }

}
