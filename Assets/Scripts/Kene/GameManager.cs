using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
//using System.Linq;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Object References")]
    [SerializeField] PlayerHUD playerHUD;
    [SerializeField] Hand magicianHand;
    [SerializeField] ItemSpawner spawner;

    [Header("Data Containers")]
    [SerializeField] Spell[] allSpells;
    [SerializeField] List<Sprite> allItemSprites;

    [Header("Game Events")]
    public UnityEvent<float> onHealthChange;
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    public UnityEvent GoodPick;
    public UnityEvent BadPick;

    private const int wavesPerSpell = 3;

    private const int startingHealth = 5;
    private const int maxHealth = 10;
    private int healthCount = 5;

    private int currentSpell = 0;
    private int currentWave = 0;

    private Sprite[] currentRecipe;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        StartGame();
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        currentSpell = 0;
        currentWave = 0;
        healthCount = startingHealth;
        currentRecipe = GetNewRecipe();
        playerHUD.SetSpellRecipe(currentRecipe);
        StartNewWave();
    }

    private void StartNewWave()
    {

        //go to next spell if finished all waves
        if (currentWave == allSpells[0].waves.Length)
        {
            currentWave = 0;
            currentSpell += 1;
            currentRecipe = GetNewRecipe();
            playerHUD.SetSpellRecipe(currentRecipe);
        }

        //exit game loop if all spells finished
        if (currentSpell == allSpells.Length)
        {
            OnWin.Invoke();
            return;
        }

        var badItems = new List<Sprite>(allItemSprites);
        var goodItemSprite = currentRecipe[currentWave];
        var goodItemIndex = badItems.IndexOf(goodItemSprite);
        badItems.RemoveAt(goodItemIndex);

        var spell = allSpells[currentSpell];
        var wave = spell.waves[currentWave];
        spawner.SpawnWave(wave.item, wave.amount, goodItemSprite, badItems.ToArray());
        playerHUD.SetFlash(currentWave);
        magicianHand.GrabRandomSpot(wave.timeToShadow, wave.timeToGrab);
    }

    public Sprite[] GetNewRecipe()
    {
        var recipe = new Sprite[wavesPerSpell];
        for (int i = 0; i < wavesPerSpell; i++)
        {
            recipe[i] = allItemSprites[Random.Range(0, allItemSprites.Count)];
        }
        return recipe;
    }

    public void ProcessGrabbedItem(Item item)
    {
        if (item != null && item.IsGood) GoodOutcome();
        else BadOutcome();
    }

    public void GoodOutcome()
    {
        //only increase on final wave success
        if (currentWave == wavesPerSpell - 1)
        {
            healthCount = Mathf.Min(healthCount + 1, maxHealth);
            onHealthChange.Invoke(healthCount);
        }
        currentWave += 1;
        GoodPick.Invoke();
        StartNewWave();
    }

    public void BadOutcome()
    {
        healthCount -= 1;
        onHealthChange.Invoke(healthCount);
        BadPick.Invoke();

        if (healthCount == 0) OnLose.Invoke();
        else StartNewWave();
    }
}
