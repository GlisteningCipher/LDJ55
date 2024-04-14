using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianHand : MonoBehaviour
{
    public SpriteRenderer shadowAppearance;
    public GameObject handObject;

    public Collider2D ItemInShadow;

    [Header("Hand and Shadow Settings")]
    public Vector3 farHandOffscreenOffset;
    public Vector3 shadowGrowthSize;
    public float duration;

    private void Start()
    {
        ShadowHandAppear();
    }

    private void Update()
    {

    }

    public void ShadowHandAppear()
    {
        ItemInShadow = null;
        shadowAppearance.gameObject.GetComponent<Collider2D>().enabled = false;
        Vector3 height = new Vector3(shadowAppearance.transform.position.x, shadowAppearance.transform.position.y + farHandOffscreenOffset.y, shadowAppearance.transform.position.z);
        Vector3 upDirection = shadowAppearance.transform.position + farHandOffscreenOffset;
        handObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(handObject.transform.DOLocalMove(shadowAppearance.transform.position, duration).From(height))
        .Join(shadowAppearance.transform.DOScale(shadowGrowthSize, duration).OnStepComplete(ItemCollisionCheck))
        .Append(handObject.transform.DOLocalMove(upDirection, duration).From(shadowAppearance.transform.position))
        .Join(shadowAppearance.transform.DOScale(Vector3.zero, 1));
    }

    public void ItemCollisionCheck()
    {
        shadowAppearance.gameObject.GetComponent<Collider2D>().enabled = true;
        Collider2D collider = shadowAppearance.gameObject.GetComponent<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();

        if (collider.OverlapCollider(contactFilter, results) > 0)
        {
            results.Add(collider);

            results[0].transform.parent = handObject.transform;
        }
        else
        {
            print("Nothing was overlapped");
        }
    }

    private void GoToPosition(Vector3 destionation)
    {
        throw new System.NotImplementedException();
    }

}
