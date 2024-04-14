using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Music
    // Piper Morgan is for main menu
    [SerializeField] private AudioSource _musicHatricia, _musicPiperMorgan;
    // Sfx by category
    [SerializeField]
    private AudioSource _crowdBoo, _crowdApplause,
    _pickUpObject, _dropObject,
    _snare, _cymbal, _handGrab,
    _magicSpell2,
    _uiMenuBack, _uiPause, _uiSelect, _uiStartGame;

    public static AudioManager Instance;


    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        MusicMainMenuPlay();
    }

    //* MUSIC

    public void MusicMainMenuPlay()
    {
        _musicPiperMorgan.Play();
        _musicHatricia.Stop();
    }

    /// <summary> Can also use just the other music pieces play method.</summary>
    public void MusicMainMenuStop() => _musicPiperMorgan.Play();

    public void MusicGameplayPlay()
    {
        _musicPiperMorgan.Stop();
        _musicHatricia.Play();
    }

    /// <summary> Can also use just the other music pieces play method.</summary>
    public void MusicGameplayStop() => _musicHatricia.Stop();

    //* SOUND EFFECTS

    public void SfxCrowdApplause() => _crowdApplause.Play();
    public void SfxCrowdBoo() => _crowdBoo.Play();

    public void SfxObjectPickUp() => _pickUpObject.Play();
    public void SfxObjectDrop() => _dropObject.Play();

    public void SfxSnare() => _snare.Play();
    public void SfxCymbal() => _cymbal.Play();
    public void SfxHandGrab() => _handGrab.Play();

    public void SfxMagicSpell2() => _magicSpell2.Play();

    public void SfxUiMenuBack() => _uiMenuBack.Play();
    public void SfxUiPause() => _uiPause.Play();
    public void SfxUiSelect() => _uiSelect.Play();
    public void SfxUiStartGame() => _uiStartGame.Play();






}
