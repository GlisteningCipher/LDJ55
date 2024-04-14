//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public bool IsGood => gameObject.CompareTag("ItemGood");
    public bool IsBad => gameObject.CompareTag("ItemBad");

    public UnityEvent OnCarry = default;
    public UnityEvent OnDrop = default;

    [SerializeField] float tweenDuration = 0.1f;
    [SerializeField] float maxSpawnDelay = 0f;

    private Transform initialParent;
    private Tween carryTween;
    private Transform t;

    private void Awake()
    {
        t = transform;
        initialParent = transform.parent;
    }

    private void Start()
    {
        transform.DOMoveY(transform.position.y, 1f).From(transform.position.y + 10).SetEase(Ease.OutCubic).SetDelay(Random.Range(0, maxSpawnDelay))
            .OnComplete(() =>
            {
                OnDrop.Invoke();
            });
    }

    private void OnMouseDown()
    {
        var playerScript = Familiar.Instance;
        if (carryTween.IsActive()) carryTween.Complete();
        transform.SetParent(playerScript.transform);
        carryTween = transform.DOLocalMove(playerScript.carryTransform.localPosition, tweenDuration);
        OnCarry.Invoke();
    }

    private void OnMouseUp()
    {
        if (carryTween.IsActive()) carryTween.Complete();
        carryTween = transform.DOLocalMove(Familiar.Instance.dropTransform.localPosition, tweenDuration)
            .OnComplete(() =>
            {
                transform.SetParent(initialParent);
                OnDrop.Invoke();
            });
    }

    void FixedUpdate()
    {
        // For pseudo 3d effect
        // todo perf: do only if moving
        t.position = new Vector3(t.position.x, t.position.y, 10 + t.position.y);
    }

}
