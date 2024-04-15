using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianHand : MonoBehaviour
{
    public GameManager gameManager;
    public SpriteRenderer shadowAppearance;
    public GameObject handObject;

    public Collider2D itemGrabbed;

    [Header("Hand and Shadow Settings")]
    public Vector2 farHandOffscreenOffset;
    public Vector2 shadowStartSize;
    public Vector2 shadowGrowthSize;
    public float duration;

    private Item currentItem;

    [ContextMenu("Fire Grab")]
    public void HandAppear()
    {
        Vector2 height = shadowAppearance.transform.position + new Vector3(0, farHandOffscreenOffset.y);
        Vector2 upDirection = new Vector2(0, shadowAppearance.transform.position.y + farHandOffscreenOffset.y);

        handObject.transform.position = height;

        Sequence seq = DOTween.Sequence();
        //Starting State
        seq.PrependInterval(2)
           .Append(shadowAppearance.transform.DOScale(shadowStartSize, duration))
           .Append(handObject.transform.DOMove(shadowAppearance.transform.position, duration).SetDelay(2))

        //Grab n Drop State
           .Join(shadowAppearance.transform.DOScale(shadowGrowthSize, duration))
           .AppendCallback(ItemCollisionCheck)
           .Append(handObject.transform.DOMove(upDirection, duration).OnStepComplete(RemoveItemFromHand))
           .Join(shadowAppearance.transform.DOScale(Vector3.zero, duration))
           .Join(shadowAppearance.DOColor(new Color(0, 0, 0, 0), duration))
           .AppendCallback(Evaluate);
    }

    public void ItemCollisionCheck()
    {
        Collider2D collider = shadowAppearance.gameObject.GetComponent<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();

        if (collider.OverlapCollider(contactFilter, results) > 0)
        {
            //TODO: grab item closest to center
            //iterate all results, find minimum distance result

            //results.Add(collider);
            results[0].transform.parent = handObject.transform;
            itemGrabbed = results[0];
            currentItem = itemGrabbed.GetComponent<Item>();
        }
        else
        {
            print("Nothing was overlapped");
        }
    }

    public void RemoveItemFromHand()
    {
        itemGrabbed.transform.parent = null;
        itemGrabbed = null;
    }

    private void Evaluate()
    {
        gameManager.ProcessGrabbedItem(currentItem);
        currentItem = null;
    }



}
