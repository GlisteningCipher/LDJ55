//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject hand;
    [SerializeField] SpriteRenderer shadow;
    [SerializeField] Collider2D grabArea;

    [Header("AnimationSettings")]
    [SerializeField] Vector3 loweredPosition;
    [SerializeField] Vector3 raisedPosition;
    [SerializeField] float tweenDuration = 1f;
    [SerializeField] Bounds bounds;

    private const float SMALL_WAIT_DURATION = 0.2f;

    private Item currentItem;

    private Sequence grabSequence;

    private void OnDestroy()
    {
        if (grabSequence.IsActive()) grabSequence.Kill();
    }

    [ContextMenu("Grab Random Spot")]
    public void GrabRandomSpot(float timeToShadow, float timeToGrab)
    {
        var xMin = bounds.center.x - bounds.extents.x;
        var xMax = bounds.center.x + bounds.extents.x;
        var yMin = bounds.center.y - bounds.extents.y;
        var yMax = bounds.center.y + bounds.extents.y;

        var xRan = Random.Range(xMin, xMax);
        var yRan = Random.Range(yMin, yMax);

        grabSequence = DOTween.Sequence()
            .AppendCallback(SequenceInit)
            .Append(GoToPosition(new Vector3(xRan, yRan))).AppendInterval(timeToShadow)
            .Append(RevealShadow()).AppendInterval(timeToGrab - 1f)
            .AppendCallback(()=>AudioManager.Instance.SfxHandIncoming()).AppendInterval(1f)
            .Append(LowerHand())
            .Join(MaximizeShadow()).AppendInterval(SMALL_WAIT_DURATION)
            .AppendCallback(GrabItem).AppendInterval(SMALL_WAIT_DURATION)
            .Append(RaiseHand())
            .Join(HideShadow())
            .AppendCallback(ProcessItem);
    }

    private void SequenceInit()
    {
        hand.transform.localPosition = raisedPosition;
        shadow.DOFade(0f, 0f);
    }

    private Tween LowerHand()
    {
        return hand.transform.DOLocalMove(loweredPosition, tweenDuration);
    }

    private Tween RaiseHand()
    {
        return hand.transform.DOLocalMove(raisedPosition, tweenDuration);
    }

    private Tween RevealShadow()
    {
        return shadow.DOFade(0.2f, tweenDuration);
    }

    private Tween MaximizeShadow()
    {
        return shadow.DOFade(0.8f, tweenDuration);
    }

    private Tween HideShadow()
    {
        return shadow.DOFade(0f, tweenDuration);
    }

    private Tween GoToPosition(Vector3 destination)
    {
        return transform.DOMove(destination, 1f);
    }

    private void GrabItem()
    {
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> overlappingColliders = new List<Collider2D>();

        if (grabArea.OverlapCollider(contactFilter, overlappingColliders) > 0)
        {
            AudioManager.Instance.SfxHandGrab();
            float minDistance = Mathf.Infinity;
            Collider2D closestCollider = null;
            foreach (var col in overlappingColliders)
            {
                var distance = (col.transform.position - grabArea.transform.position).magnitude;
                if (minDistance > distance)
                {
                    closestCollider = col;
                    minDistance = distance;
                }
            }
            closestCollider.transform.SetParent(hand.transform);
            currentItem = closestCollider.GetComponent<Item>();

            //disable autonomous movement after grabbed
            if (currentItem.TryGetComponent<ItemWander>(out var wander)) Destroy(wander);
            if (currentItem.TryGetComponent<ItemTaxis>(out var taxis)) Destroy(taxis);
        }
    }

    private void ProcessItem()
    {
        GameManager.Instance.ProcessGrabbedItem(currentItem);
        currentItem = null;
    }

}
