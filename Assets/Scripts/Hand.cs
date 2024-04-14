//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] Vector3 loweredPosition;
    [SerializeField] Vector3 raisedPosition;
    [SerializeField] float tweenDuration = 2f;

    private Tween tween;

    [ContextMenu("Lower")]
    private void Lower()
    {
        if (tween.IsActive()) tween.Complete();
        tween = hand.transform.DOLocalMove(loweredPosition, tweenDuration).From(raisedPosition);
    }

    [ContextMenu("Raise")]
    private void Raise()
    {
        if (tween.IsActive()) tween.Complete();
        tween = hand.transform.DOLocalMove(raisedPosition, tweenDuration).From(loweredPosition);
    }

    private void GoToPosition(Vector3 destionation)
    {
        throw new System.NotImplementedException();
    }

    //private void Start()
    //{
    //    float t = 0;
    //    DOTween.To(()=>t, v=>t=v, 2f, 5f)
    //        .OnPlay(() => Debug.Log("play"))
    //        .OnUpdate(() => Debug.Log("update"))
    //        .OnComplete(()=>Debug.Log("complete"));
    //}
}
