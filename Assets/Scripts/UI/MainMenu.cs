using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField] private float _uiPanelOpenTime = 0.5f, _uiPanelCloseTime = 0.3f;

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
        AudioManager.Instance.SfxUiPause();

        _pauseContainer.SetActive(true);
        _pauseContainer.transform.DOKill();
        _pauseContainer.transform.DOScale(1, _uiPanelOpenTime).From(Vector3.zero);

        _dimmerPanel.SetActive(true);
        State = MenuState.Paused;
    }

    public void BUTTON_ResumeClosePauseMenu()
    {
        AudioManager.Instance.SfxUiMenuBack();

        _pauseContainer.transform.DOKill();
        _pauseContainer.transform.DOScale(0, _uiPanelCloseTime).OnComplete(() =>
        {
            State = MenuState.Playing;
            _pauseContainer.SetActive(false);
            _dimmerPanel.SetActive(false);
        });
    }

    public void BUTTON_StartGame()
    {
        AudioManager.Instance.SfxUiStartGame();
        DOVirtual.DelayedCall(0.5f, () =>
        {
            _mainMenuContainer.SetActive(false);
            // todo Game Manager / Scene loader Show Level 1
            AudioManager.Instance.MusicGameplayPlay();
        });
    }

    public void BUTTON_MainMenuOpen()
    {
        _mainMenuContainer.SetActive(true);
        _pauseContainer.transform.DOKill();
        _pauseContainer.transform.DOScale(0, _uiPanelCloseTime).OnComplete(() => _pauseContainer.SetActive(false));

        _dimmerPanel.SetActive(false);
        _dimmerPanelBlockingPause.SetActive(false);
        // todo game logic because we are leaving from the gameplay view
    }

    public void BUTTON_SettingsOpen()
    {
        AudioManager.Instance.SfxUiSelect();

        // todo Stop game speed in case started from game view
        _settingsContainer.SetActive(true);
        _settingsContainer.transform.DOScale(1, _uiPanelOpenTime).From(Vector3.zero);

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
        AudioManager.Instance.SfxUiMenuBack();

        _settingsContainer.transform.DOScale(0, _uiPanelCloseTime).OnComplete(() =>
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
        });
    }

    public void BUTTON_CreditsOpen()
    {
        _creditsContainer.SetActive(true);
        _creditsContainer.transform.DOScale(1, _uiPanelOpenTime).From(Vector3.zero);

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

        AudioManager.Instance.SfxUiSelect();
    }

    public void BUTTON_CreditsClose()
    {
        AudioManager.Instance.SfxUiMenuBack();

        _creditsContainer.transform.DOScale(0, _uiPanelCloseTime).OnComplete(() =>
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
        });
    }

    public void BUTTON_QuitGame()
    {
        AudioManager.Instance.SfxUiMenuBack();
        foreach (var item in GameObject.FindObjectsOfType<Button>())
        {
            item.transform.DOScale(0, 0.4f);
        }

        DOVirtual.DelayedCall(0.5f, () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
