using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Slider audienceMeter;
    public int points;

    public MagicianHand magicHand;

    public float timeValue = 90;
    public bool timerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        audienceMeter.minValue = 0; 
        audienceMeter.maxValue = 100;

    }

     // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 10;
            timerRunning = false;
            magicHand.HandAppear();
        }

    }

    public void UpdatePoints(int increment)
    {
        points += increment;
        audienceMeter.value = points;
    }


}
