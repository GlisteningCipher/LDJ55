using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyManager : MonoBehaviour
{
    [SerializeField] private KeyCode _pauseHotkey = KeyCode.Escape;

    void Update()
    {
        if (Input.GetKeyDown(_pauseHotkey))
        {
            MainMenu.Instance.OpenPauseMenu();
        }
    }
}
