using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] int appealCount;
    [SerializeField] int spellCount;
    [SerializeField] int waveCount;

    public Slider audienceMeter;
    public int points;

    public MagicianHand magicHand;
    public ItemSpawner spawner;

    public List<Spell> spells;

    public float timeValue = 5;
    public bool dropTimerRunning = true;

    public UnityEvent OnWin;
    public UnityEvent OnLose;

    // Start is called before the first frame update
    void Start()
    {
        audienceMeter.minValue = 0;
        audienceMeter.maxValue = 100;

        StartNewWave();
    }

    private void StartNewWave()
    {
        magicHand.HandAppear();
        //spells[spellCount].waves[waveCount].items
        //timeValue = 10;
    }

    public void ItemInspection(Item item)
    {

    }

     // Update is called once per frame
    void Update()
    {
        if (timeValue > 0 && dropTimerRunning == false)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 5;
            dropTimerRunning = false;
        }

    }

    public void GoodOutcome()
    {

    }

    public void BadOutcome()
    {

    }

    public void UpdatePoints(int increment)
    {
        points += increment;
        audienceMeter.value = points;
    }


}
