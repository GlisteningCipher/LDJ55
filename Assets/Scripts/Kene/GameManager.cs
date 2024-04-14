using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public Slider audienceMeter;
    public int points;

    public GameObject handObject;
    public MagicianHand magicHand;
    public float handAppearanceSpeed;

    public float timeValue = 90;
    public bool timerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        audienceMeter.minValue = 0; 
        audienceMeter.maxValue = 100;
        timerRunning = true;
    }

     // Update is called once per frame
    void Update()
    {
        if (timeValue > 0 && timerRunning == true)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            timerRunning = false;
            StartCoroutine(HandIsGrabbing());
        }
    }

    IEnumerator HandIsGrabbing()
    {
        StartCoroutine(magicHand.ShadowAppear(10f));

        Vector3 getShadowPos = new Vector3(magicHand.transform.position.x, magicHand.transform.position.y + 5, magicHand.transform.position.z);

        yield return StartCoroutine(LerpHandToPosition(2, getShadowPos, magicHand.transform.position));

        if (magicHand.ItemInShadow.Any(x => x.gameObject.CompareTag("bad") || x == null))
        {
            UpdatePoints(1);
        }
        else
        {
            UpdatePoints(-1);
        }
    }

    private IEnumerator LerpHandToPosition(float duration, Vector3 startPos, Vector3 endPos)
    {
        handObject.transform.position = startPos;
        handObject.SetActive(true);

        float lerptime = 0;

        while (lerptime < duration)
        {
           handObject.transform.position = Vector3.Lerp(handObject.transform.position, endPos, lerptime/duration);

           lerptime += Time.deltaTime / handAppearanceSpeed;
           yield return null;
        }

        handObject.transform.position = endPos;

        yield return new WaitForSeconds(duration);

    }

    public void UpdatePoints(int increment)
    {
        points += increment;
        audienceMeter.value = points;
    }


}
