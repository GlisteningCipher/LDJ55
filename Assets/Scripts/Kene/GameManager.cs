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

    public MagicianHand magicHand;
    [SerializeField] bool _handAppeared;

    //public float timeValue = 90;
    //public bool timerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        audienceMeter.minValue = 0; 
        audienceMeter.maxValue = 100;

    }

     // Update is called once per frame
    void Update()
    {
        //if (timeValue > 0)
        //{
        //    timeValue -= Time.deltaTime;
        //}
        //else
        //{
        //    timeValue = 0;
        //    _handAppeared = true;
        //    timerRunning = false;
        //}

        if (_handAppeared == true)
        {
            
            _handAppeared = false;
        }
    }

    public void UpdatePoints(int increment)
    {
        points += increment;
        audienceMeter.value = points;
    }


}
