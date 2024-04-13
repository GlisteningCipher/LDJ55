using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class MagicianHand : MonoBehaviour
{
    public SpriteRenderer shadowAppearance;
    public float shadowSize;
    public float shadowAppearSpeedMultipler;

    public List<Collider2D> ItemInShadow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //for debug purposes
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shadowAppearance.transform.position = mouseWorld;

            StartCoroutine(ShadowAppear(10f));
        }
    }


    public IEnumerator ShadowAppear(float duration)
    {
        ItemInShadow = new List<Collider2D>();
        shadowAppearance.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            float lerptime = 0;
            Color c = shadowAppearance.color;

            while (lerptime < duration)
            {
                c.a = Mathf.Clamp01(lerptime / duration);
                float s = Mathf.Lerp(0.1f, shadowSize, lerptime / duration);

                lerptime += Time.deltaTime / shadowAppearSpeedMultipler;

                shadowAppearance.color = c;
                shadowAppearance.transform.localScale = new Vector3(s, s, s);
                
                yield return null;
        }

        shadowAppearance.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        ItemCollisionCheck();

        yield return new WaitForSeconds(duration);
    }

    public void ItemCollisionCheck()
    {
        Collider2D collider = shadowAppearance.gameObject.GetComponent<CircleCollider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D> ();

        if (collider.OverlapCollider(contactFilter, results) > 0)
        {
            foreach (var item in results)
            {
                ItemInShadow.Add(item);
            }

        }
        else 
        {
            print("Nothing was overlapped");
        };
    }



}
