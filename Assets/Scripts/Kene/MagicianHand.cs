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
    public float appearSpeedMultipler;

    private void Start()
    {
        StartCoroutine(ShadowHandAppear(1f));
    }

    private void Update()
    {

    }

    public IEnumerator ShadowHandAppear(float duration)
    {
            ItemInShadow = null;
            shadowAppearance.gameObject.GetComponent<Collider2D>().enabled = false;
            shadowAppearance.color = new Color(0, 0, 0, 0);

            handObject.transform.position = new Vector3(shadowAppearance.transform.position.x, shadowAppearance.transform.position.y + farHandOffscreenOffset.y, shadowAppearance.transform.position.z);
            handObject.SetActive(true);

            float lerptime = 0;
            Color c = shadowAppearance.color;

            while (lerptime < duration)
            {
                c.a = Mathf.Clamp01(lerptime + appearSpeedMultipler / duration);
                Vector3 s = Vector3.Lerp(Vector3.zero, shadowGrowthSize, lerptime + appearSpeedMultipler / duration);
                handObject.transform.position = Vector3.Lerp(handObject.transform.position, shadowAppearance.transform.position, lerptime / duration);

                lerptime += Time.deltaTime;
                shadowAppearance.color = c;
                shadowAppearance.transform.localScale = s;

                yield return null;
            }

        ItemCollisionCheck();
    }
    public void ItemCollisionCheck()
    {
        handObject.transform.position = transform.position;
        shadowAppearance.gameObject.GetComponent<Collider2D>().enabled = true;

        Collider2D collider = shadowAppearance.gameObject.GetComponent<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();

        if (collider.OverlapCollider(contactFilter, results) > 0)
        {

        }
        else
        {
            print("Nothing was overlapped");
        }

        StartCoroutine(ShadowHandDissappear(5f));
    }

    public IEnumerator ShadowHandDissappear(float duration)
    {
            shadowAppearance.gameObject.GetComponent<Collider2D>().enabled = false;
            shadowAppearance.transform.localScale = shadowGrowthSize;

            handObject.SetActive(true);
            handObject.transform.position = shadowAppearance.transform.position;

            float lerptime = 1;
            Color c = shadowAppearance.color;

            while (lerptime < duration)
            {
                c.a = Mathf.Clamp01(lerptime / duration);
                Vector3 s = Vector3.Lerp(shadowAppearance.transform.localScale, Vector3.zero, lerptime / duration);
                handObject.transform.position = Vector3.Lerp(handObject.transform.position, shadowAppearance.transform.position + Vector3.up + farHandOffscreenOffset, lerptime / duration);

                lerptime -= Time.deltaTime;
                shadowAppearance.color = c;
                shadowAppearance.transform.localScale = s;

                yield return null;
            }
    }

}
