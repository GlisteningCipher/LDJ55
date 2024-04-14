using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> Provides methods for the main menu buttons and other things in main menu if needed. 
/// Menu button methods call other more suitable scripts if needed. </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuContainer;
    [SerializeField] private GameObject _settingsContainer;
    [SerializeField] private GameObject _dimmerPanel;
    [SerializeField] private GameObject _creditsContainer;

    public void Start()
    {
        _mainMenuContainer.SetActive(true);
        _settingsContainer.SetActive(false);
        _dimmerPanel.SetActive(false);
        _creditsContainer.SetActive(false);
    }

    public void BUTTON_StartGame()
    {
        _mainMenuContainer.SetActive(false);
        // todo Game Manager / Scene loader Show Level 1
    }

    public void BUTTON_MainMenuOpen()
    {
        _mainMenuContainer.SetActive(true);
        // todo game logic because we are leaving from the gameplay view
    }

    public void BUTTON_SettingsOpen()
    {
        // todo Stop game speed in case started from game view
        _dimmerPanel.SetActive(true);
        _settingsContainer.SetActive(true);
    }

    public void BUTTON_SettingsClose()
    {
        // todo Start game speed in case started from game view
        _dimmerPanel.SetActive(false);
        _settingsContainer.SetActive(false);
    }

    public void BUTTON_CreditsOpen()
    {
        // todo Stop game speed in case started from game view
        _dimmerPanel.SetActive(true);
        _creditsContainer.SetActive(true);
    }

    public void BUTTON_CreditsClose()
    {
        // todo Stop game speed in case started from game view
        _dimmerPanel.SetActive(false);
        _creditsContainer.SetActive(false);
    }

    public void BUTTON_QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
