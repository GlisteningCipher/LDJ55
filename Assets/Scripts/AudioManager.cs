using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Music
    // Piper Morgan is for main menu
    [SerializeField] private AudioSource _musicHatricia, _musicPiperMorgan, _musicMenu, _musicGameplay;
    // Sfx by category
    [SerializeField]
    private AudioSource _crowdBoo, _crowdApplause,
    _pickUpObject, _dropObject,
    _snare, _cymbal, _handGrab, _handIncoming, _badComponent, _goodComponent, _nextLevel,
    _magicSpell1, _magicSpell2,
    _failure, _victory,
    _uiMenuBack, _uiPause, _uiSelect, _uiStartGame,
    _uiPaperButtonPress, _uiPaperGoingBack, _uiPaperButtonHover;

    public enum MusicState { Gameplay, MainMenu }
    public MusicState State = MusicState.MainMenu;

    [Header("Music loop settings")]
    [SerializeField] private float _musicLoopPointInSeconds = 10f;

    public static AudioManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        MusicMainMenuPlay();
        State = MusicState.MainMenu;
    }

    void FixedUpdate()
    {
        if (State == MusicState.Gameplay && _musicGameplay.isPlaying == false)
        {
            _musicGameplay.Play();
            _musicGameplay.time = _musicLoopPointInSeconds;
        }
    }

    //* MUSIC

    public void MusicMainMenuPlay()
    {
        _musicMenu.Play();
        _musicGameplay.Stop();
        State = MusicState.MainMenu;
    }

    /// <summary> Can also use just the other music pieces play method.</summary>
    public void MusicMainMenuStop() => _musicMenu.Stop();

    public void MusicGameplayPlay()
    {
        _musicMenu.Stop();
        _musicGameplay.Play();
        State = MusicState.Gameplay;
    }

    /// <summary> Can also use just the other music pieces play method.</summary>
    public void MusicGameplayStop() => _musicGameplay.Stop();

    //* SOUND EFFECTS

    public void SfxCrowdApplause() => _crowdApplause.Play();
    public void SfxCrowdBoo() => _crowdBoo.Play();

    public void SfxObjectPickUp() => _pickUpObject.Play();
    public void SfxObjectDrop() => _dropObject.Play();

    public void SfxSnare() => _snare.Play();
    public void SfxCymbal() => _cymbal.Play();
    public void SfxHandGrab() => _handGrab.Play();
    public void SfxHandIncoming() => _handIncoming.Play();
    public void SfxBadComponent() => _badComponent.Play();
    public void SfxGoodComponent() => _goodComponent.Play();
    public void SfxNextLevel() => _nextLevel.Play();

    public void SfxFailure() => _failure.Play();
    public void SfxVictory() => _victory.Play();

    public void SfxPaperHover() => _uiPaperButtonHover.Play();
    public void SfxPaperPressButton() => _uiPaperButtonPress.Play();
    public void SfxPaperGoBack() => _uiPaperGoingBack.Play();

    public void SfxMagicSpell1() => _magicSpell1.Play();
    public void SfxMagicSpell2() => _magicSpell2.Play();
    public void SfxRandomMagicSpell()
    {
        if (Random.Range(0, 2) == 1)
        {
            SfxMagicSpell1();
        }
        else
        {
            SfxMagicSpell2();
        }
    }

    public void SfxUiMenuBack() => _uiMenuBack.Play();
    public void SfxUiPause() => _uiPause.Play();
    public void SfxUiSelect() => _uiSelect.Play();
    public void SfxUiStartGame() => _uiStartGame.Play();






}
