using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameEndLoseContainer, _gameEndWinContainer;
    [SerializeField] private TMP_Text _loseScore, _winScore;

    private void Start()
    {
        _gameEndLoseContainer.SetActive(false);
        _gameEndWinContainer.SetActive(false);
    }

    public void WinGame(string score)
    {
        _gameEndWinContainer.SetActive(true);
        _gameEndWinContainer.transform.DOScale(1, 0.5f).From(0);
        if (_winScore)
        {
            _winScore.text = score;
        }
    }

    public void LoseGame(string score)
    {
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
}
