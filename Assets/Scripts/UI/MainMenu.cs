using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> Provides methods for the main menu buttons and other things in main menu if needed. 
/// Menu button methods call other more suitable scripts if needed. 
/// Also acts as an UI Controller of sorts. Could refactor that to some other script. </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuContainer;
    [SerializeField] private GameObject _settingsContainer;
    [SerializeField] private GameObject _dimmerPanel, _dimmerPanelBlockingPause;
    [SerializeField] private GameObject _creditsContainer;
    [SerializeField] private GameObject _pauseContainer;

    public enum MenuState { Paused, Playing }
    public MenuState State = MenuState.Playing;

    public void Start()
    {
        _mainMenuContainer.SetActive(true);
        _settingsContainer.SetActive(false);
        _dimmerPanel.SetActive(false);
        _creditsContainer.SetActive(false);
    }

    [ContextMenu("Open Pause Menu")]
    public void OpenPauseMenu()
    {
        State = MenuState.Paused;
        _pauseContainer.SetActive(true);
        _dimmerPanel.SetActive(true);
    }

    public void BUTTON_ResumeClosePauseMenu()
    {
        State = MenuState.Playing;
        _pauseContainer.SetActive(false);
        _dimmerPanel.SetActive(false);
    }

    public void BUTTON_StartGame()
    {
        _mainMenuContainer.SetActive(false);
        // todo Game Manager / Scene loader Show Level 1
    }

    public void BUTTON_MainMenuOpen()
    {
        _mainMenuContainer.SetActive(true);
        _pauseContainer.SetActive(false);

        _dimmerPanel.SetActive(false);
        _dimmerPanelBlockingPause.SetActive(false);
        // todo game logic because we are leaving from the gameplay view
    }

    public void BUTTON_SettingsOpen()
    {
        // todo Stop game speed in case started from game view
        _settingsContainer.SetActive(true);

        if (State == MenuState.Paused)
        {
            _dimmerPanelBlockingPause.SetActive(true);
            _dimmerPanel.SetActive(false);
        }
        else
        {
            _dimmerPanel.SetActive(true);
            _dimmerPanelBlockingPause.SetActive(false);
        }
    }

    public void BUTTON_SettingsClose()
    {
        _settingsContainer.SetActive(false);

        if (State == MenuState.Paused)
        {
            // Going back to paused
            _dimmerPanelBlockingPause.SetActive(false);
            _dimmerPanel.SetActive(true);
        }
        else
        {
            // Going back to plain old main menu
            _dimmerPanel.SetActive(false);
            _dimmerPanelBlockingPause.SetActive(false);
        }
    }

    public void BUTTON_CreditsOpen()
    {
        _creditsContainer.SetActive(true);

        if (State == MenuState.Paused)
        {
            _dimmerPanelBlockingPause.SetActive(true);
            _dimmerPanel.SetActive(false);
        }
        else
        {
            _dimmerPanel.SetActive(true);
            _dimmerPanelBlockingPause.SetActive(false);
        }
    }

    public void BUTTON_CreditsClose()
    {
        _creditsContainer.SetActive(false);

        if (State == MenuState.Paused)
        {
            // Going back to paused
            _dimmerPanelBlockingPause.SetActive(false);
            _dimmerPanel.SetActive(true);
        }
        else
        {
            // Going back to plain old main menu
            _dimmerPanel.SetActive(false);
            _dimmerPanelBlockingPause.SetActive(false);
        }
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
