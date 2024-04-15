using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] PlayerHUD playerHUD;

    [Header("Game Loop Property Settings")]
    [SerializeField] Spell[] allSpells;
    [SerializeField] List<Sprite> allItemSprites;

    public UnityEvent onHealthChange;

    [Header("Game References")]
    public MagicianHand magicHand;
    public ItemSpawner spawner;

    //public float timeValue = 5;
    //public bool dropTimerRunning = true;

    [Header("Condition Events")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    public UnityEvent GoodPick;
    public UnityEvent BadPick;

    [Header("Floor Settings")]
    [SerializeField] private float minWidthFloor;
    [SerializeField] private float maxWidthFloor;
    [SerializeField] private float minHeightFloor;
    [SerializeField] private float maxHeightFloor;



    private const int startingHealth = 5;
    private const int maxHealth = 10;
    private int healthCount = 5;

    private int currentSpell = 0;
    private int currentWave = 0;

    private Sprite[] currentRecipe;

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        currentSpell = 0;
        currentWave = 0;
        healthCount = startingHealth;
        StartNewWave();
    }

    private void StartNewWave()
    {
        Debug.Log("StartNewWave");
        var spell = allSpells[currentSpell];
        var wave = spell.waves[currentWave];

        //go to next spell if finished all waves
        if (currentWave == spell.waves.Length)
        {
            currentWave = 0;
            currentSpell += 1;
        }

        //create new recipe at start of each spell
        if (currentSpell == 0)
        {
            currentRecipe = GetNewRecipe();
            playerHUD.SetSpellRecipe(currentRecipe);
        }

        //exit game loop
        if (currentSpell == allSpells.Length)
        {
            OnWin.Invoke();
            return;
        }

        var badItems = new List<Sprite>(allItemSprites);
        var goodItemSprite = currentRecipe[currentWave];
        var goodItemIndex = badItems.IndexOf(goodItemSprite);
        badItems.RemoveAt(goodItemIndex);

        spawner.SpawnWave(wave.item, wave.amount, goodItemSprite, badItems.ToArray());
        playerHUD.SetFlash(currentWave);

        magicHand.transform.position = PlaceLocationOnShadow();
        magicHand.HandAppear();
    }

    public Sprite[] GetNewRecipe()
    {
        var recipe = new Sprite[3];
        for (int i = 0; i < 3; i++)
        {
            recipe[i] = allItemSprites[Random.Range(0, allItemSprites.Count)];
        }
        return recipe;
    }

    public void ItemInspection(Item item)
    {
        Debug.Log("Inspect");
        if (item.IsGood)
        {
            Debug.Log("Good");
            GoodOutcome();
        }
        else
        {
            Debug.Log("Bad");
            BadOutcome();
        }
    }

    public void GoodOutcome()
    {
        healthCount = Mathf.Min(healthCount+1, maxHealth);
        currentWave += 1;
        GoodPick.Invoke();
        StartNewWave();
    }

    public void BadOutcome()
    {
        healthCount -= 1;

        if (healthCount == 0)
        {
            OnLose.Invoke();
        }
        else
        {
            StartNewWave();
        }
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
