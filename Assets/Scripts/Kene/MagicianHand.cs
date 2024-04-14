using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianHand : MonoBehaviour
{
    public SpriteRenderer shadowAppearance;
    public GameObject handObject;

    public Collider2D itemGrabbed;

    [Header("Hand and Shadow Settings")]
    public Vector3 farHandOffscreenOffset;
    public Vector3 shadowGrowthSize;
    public float duration;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GoToPosition(mouseWorld);
        }
    }

    public void HandAppear()
    {
        Vector2 height = new Vector3(shadowAppearance.transform.position.x, shadowAppearance.transform.position.y + farHandOffscreenOffset.y);
        Vector2 upDirection = shadowAppearance.transform.position + farHandOffscreenOffset;
        handObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(handObject.transform.DOLocalMove(shadowAppearance.transform.position, duration).From(height))
        .Join(shadowAppearance.transform.DOScale(shadowGrowthSize, duration).OnStepComplete(ItemCollisionCheck))
        .Append(handObject.transform.DOLocalMove(upDirection, duration).From(shadowAppearance.transform.position).OnStepComplete(RemoveItemFromHand))
        .Join(shadowAppearance.transform.DOScale(Vector3.zero, 1));
    }

    public void ItemCollisionCheck()
    {
        Collider2D collider = shadowAppearance.gameObject.GetComponent<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();

        if (collider.OverlapCollider(contactFilter, results) > 0)
        {
            //results.Add(collider);
            results[0].transform.parent = handObject.transform;
            itemGrabbed = results[0];
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

    private void GoToPosition(Vector3 destionation)
    {
        shadowAppearance.transform.position = destionation;
    }

}
