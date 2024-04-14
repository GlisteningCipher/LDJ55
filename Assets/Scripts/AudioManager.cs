using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Music
    [SerializeField] private AudioSource _musicHatricia, _musicPiperMorgan;
    // Sfx by category
    [SerializeField]
    private AudioSource _crowdBoo, _crowdApplause,
    _pickUpObject, _dropObject,
    _snare, _cymbal, _handGrab,
    _magicSpell2,
    _uiMenuBack, _uiPause, _uiSelect, _uiStartGame;


}
