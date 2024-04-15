//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public bool IsGood = false;

    public UnityEvent OnCarry = default;
    public UnityEvent OnDrop = default;

    [SerializeField] float tweenDuration = 0.1f;
    [SerializeField] float maxSpawnDelay = 0f;
    [SerializeField] SpriteRenderer spriteRenderer;

    private Transform initialParent;
    private Tween spawnTween;
    private Tween carryTween;
    private Transform t;

    private void Awake()
    {
        t = transform;
        initialParent = transform.parent;
    }

    private void Start()
    {
        spawnTween = transform.DOMoveY(transform.position.y, 1f).From(transform.position.y + 10).SetEase(Ease.OutCubic).SetDelay(Random.Range(0, maxSpawnDelay))
            .OnComplete(() =>
            {
                OnDrop.Invoke();
            });
    }

    public void Kill()
    {
        const float killTime = 0.2f;
        spriteRenderer.DOFade(0f, killTime);
        transform.DOMoveY(transform.position.y - 1f, killTime)
            .OnComplete(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        if (spawnTween.IsActive()) spawnTween.Kill();
        if (carryTween.IsActive()) spawnTween.Kill();
    }

    private void OnMouseDown()
    {
        var playerScript = Familiar.Instance;
        if (carryTween.IsActive()) carryTween.Complete();
        transform.SetParent(playerScript.transform);
        carryTween = transform.DOLocalMove(playerScript.carryTransform.localPosition, tweenDuration);
        OnCarry.Invoke();
        spriteRenderer.sortingOrder = 1;
        AudioManager.Instance.SfxObjectPickUp();
    }

    private void OnMouseUp()
    {
        if (carryTween.IsActive()) carryTween.Complete();
        carryTween = transform.DOLocalMove(Familiar.Instance.dropTransform.localPosition, tweenDuration)
            .OnComplete(() =>
            {
                transform.SetParent(initialParent);
                OnDrop.Invoke();
                spriteRenderer.sortingOrder = 0;
                AudioManager.Instance.SfxObjectDrop();
            });
    }

    void FixedUpdate()
    {
        // For pseudo 3d effect
        // todo perf: do only if moving
        //t.position = new Vector3(t.position.x, t.position.y, 10 + t.position.y);
    }

    public void SetSprite(Sprite spr)
    {
        spriteRenderer.sprite = spr;
    }

}
