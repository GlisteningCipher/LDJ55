using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Game Ending Panels")]
    [SerializeField] private GameObject _gameEndLoseContainer;
    [SerializeField] private GameObject _gameEndWinContainer;
    [SerializeField] private TMP_Text _loseScore, _winScore;
    [Header("Loading Screen")]
    [SerializeField] private GameObject _loadingScreenContainer;
    [SerializeField] private TMP_Text _loadingText;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameEndLoseContainer.SetActive(false);
        _gameEndWinContainer.SetActive(false);
    }

    [ContextMenu("Victory")]
    public void TestWin()
    {
        WinGame("5");
    }

    public void WinGame(string score)
    {
        AudioManager.Instance.SfxVictory();
        _gameEndWinContainer.SetActive(true);
        _gameEndWinContainer.transform.DOScale(1, 0.5f).From(0);
        if (_winScore)
        {
            _winScore.text = score;
        }
    }

    public void LoseGame(string score)
    {
        AudioManager.Instance.SfxFailure();
        _gameEndLoseContainer.SetActive(true);
        _gameEndLoseContainer.transform.DOScale(1, 0.5f).From(0);
        if (_loseScore)
        {
            _loseScore.text = score;
        }
    }

    public void HideGameEndPanels()
    {
        _gameEndLoseContainer.transform.DOScale(0, 0.3f).OnComplete(() => _gameEndLoseContainer.SetActive(false));
        _gameEndWinContainer.transform.DOScale(0, 0.3f).OnComplete(() => _gameEndWinContainer.SetActive(false));
    }

    public void ShowLoadingScreen()
    {
        _loadingScreenContainer.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        _loadingScreenContainer.SetActive(false);
    }
}
