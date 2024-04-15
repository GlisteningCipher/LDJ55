using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Loop Property Settings")]
    [SerializeField] int healthCount;
    [SerializeField] int spellCount;
    [SerializeField] int waveCount;

    public UnityEvent onHealthChange;

    [Header("Game References")]
    public MagicianHand magicHand;
    public ItemSpawner spawner;

    public List<Spell> spells;
    public GameObject item;
    public Sprite goodItemIcon;
    public Sprite[] badItemIcons;

    //public float timeValue = 5;
    //public bool dropTimerRunning = true;

    [Header("Condition Events")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    [Header("Floor Settings")]
    [SerializeField] private float minWidthFloor;
    [SerializeField] private float maxWidthFloor;
    [SerializeField] private float minHeightFloor;
    [SerializeField] private float maxHeightFloor;

    // Start is called before the first frame update
    void Start()
    {
        StartNewWave();
    }

    private void StartNewWave()
    {
        magicHand.transform.position = PlaceLocationOnShadow();
        spawner.SpawnWave(item, 10, goodItemIcon, badItemIcons);
        magicHand.HandAppear();
        //timeValue = 10;
    }

    public void ItemInspection(Item item)
    {
        if (item.IsGood)
        {
            GoodOutcome();
        }
        else
        {
            BadOutcome();
        }
    }

     // Update is called once per frame
    void Update()
    {
        //if (timeValue > 0 && dropTimerRunning == false)
        //{
        //    timeValue -= Time.deltaTime;
        //}
        //else
        //{
        //    timeValue = 5;
        //    dropTimerRunning = false;
        //}
    }

    public void GoodOutcome()
    {
        if (spellCount > spells.Count)
        {
            OnWin.Invoke();
        }
        else
        {
            StartNewWave();
        }

        healthCount += 1;
        waveCount += 1;
        //add sound here
    }

    public void BadOutcome()
    {
        if (healthCount < 0)
        {
            OnLose.Invoke();
        }

        healthCount -= 1;
        waveCount = 1;
        //add sound here
    }

    public Vector2 PlaceLocationOnShadow()
    {
        Vector2 spawnPos;

        float xRandom = UnityEngine.Random.Range(minWidthFloor, maxWidthFloor); //Grab min and max floor size values on X-AXIS
        float yRandom = UnityEngine.Random.Range(minHeightFloor, maxHeightFloor); //Grab min and max floor size values on Z-AXIS

        spawnPos = new Vector2(xRandom, yRandom); //Generating a spawn position for the enemy

        print(spawnPos);

        return spawnPos;
    }
}
