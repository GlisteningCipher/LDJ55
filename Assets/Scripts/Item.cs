//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    public bool IsGood => gameObject.CompareTag("ItemGood");
    public bool IsBad => gameObject.CompareTag("ItemBad");

    [SerializeField] float tweenDuration = 0.1f;
    [SerializeField] float maxSpawnDelay = 0f;

    private Transform initialParent;
    private Tween carryTween;

    private static int globalSpawnIndex;
    private int spawnIndex;
    private float zFightingStep = -0.001f;

    void RepositionZ() => transform.position += (Vector3.forward * spawnIndex * zFightingStep);

    private void Awake()
    {
        globalSpawnIndex++;
        spawnIndex = globalSpawnIndex;
        initialParent = transform.parent;
    }

    private void Start()
    {
        transform.DOMoveY(transform.position.y, 1f).From(transform.position.y + 10).SetEase(Ease.OutCubic).SetDelay(Random.Range(0, maxSpawnDelay)).OnComplete(() => RepositionZ());
    }

    private void OnMouseDown()
    {
        var playerScript = Familiar.Instance;
        if (carryTween.IsActive()) carryTween.Complete();
        transform.SetParent(playerScript.transform);
        carryTween = transform.DOLocalMove(playerScript.carryTransform.localPosition, tweenDuration).OnComplete(() => RepositionZ());
    }

    private void OnMouseUp()
    {
        if (carryTween.IsActive()) carryTween.Complete();
        carryTween = transform.DOLocalMove(Familiar.Instance.dropTransform.localPosition, tweenDuration)
            .OnComplete(() => { transform.SetParent(initialParent); RepositionZ(); });
    }

}
