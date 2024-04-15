using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using TMPro;

/// <summary> Made for the Pause panel text buttons </summary>
public class TweenButtonFeedback : MonoBehaviour
{
    [SerializeField] private float _animationSpeed = 0.5f;
    [SerializeField] private Color _activeColor = Color.green;

    public void EVENT_OnPointerEnter()
    {
        transform.DOKill();
        transform.DOScale(Vector3.one * 1.1f, _animationSpeed);
        transform.GetComponentInChildren<TMP_Text>().DOColor(_activeColor, _animationSpeed);
        // problems with killing sequence that is not saved
        // Sequence sequence = DOTween.Sequence();
        // sequence
        //     .Append(transform.DOScale(Vector3.one * 1.1f, _animationSpeed))
        //     .Append(transform.DOScale(Vector3.one, _animationSpeed))
        //     .SetLoops(-1);
        // transform
        //     .DOScale(Vector3.one * 1.1f, _animationSpeed)
        //     .OnComplete(() => transform.DOScale(Vector3.one, _animationSpeed))
        //     .SetLoops(-1);
    }

    public void EVENT_OnPointerExit()
    {
        transform.DOKill();
        transform.DOScale(Vector3.one, _animationSpeed);
        transform.GetComponentInChildren<TMP_Text>().DOColor(Color.white, _animationSpeed);
    }

    /// <summary> Panel closes so fast that this animation is worthless.</summary>
    public void EVENT_OnPointerClick()
    {
        var time = 0.15f; // total time is time * 2
        var scaleMin = 0.8f;
        transform.DOKill();
        transform.DOScale(Vector3.one * scaleMin, time).OnComplete(() => transform.DOScale(1, time));
    }

}
