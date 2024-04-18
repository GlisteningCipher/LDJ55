using UnityEngine;
using DG.Tweening;

public class GameTime : MonoBehaviour
{
    // /// <summary> The speed at which the game runs. E.g. 1x, 1.5x, 2x. </summary>
    // public float gameRunSpeed1x = 1f, gameRunSpeed2x = 1.5f;

    /// <summary> Will be used instead of Time.DeltaTime for player controlled gamespeed.</summary>
    public static float GameTimeDelta;
    /// <summary> Will be used instead of Time.FixedDeltaTime for player controlled gamespeed.</summary>
    public static float GameTimeDeltaFixed;
    //[ShowInInspector] 
    public static float GameTimeMultiplier = 1f;
    public float pauseMultiplier = 1;
    public float slowdownMultiplier = 1;
    public float slowdownUpdatePanel = 0.7f, slowdownDragging = 0.7f, slowdownTarget;
    public float slowdownChangeSpeed = 2f;
    public static GameTime Instance;


    void Start()
    {
        Instance = this;
        StartOfRound();
    }

    public void Unpause() => pauseMultiplier = 1;
    public void Pause() => pauseMultiplier = 0;

    public void StartOfRound()
    {
        GameTimeMultiplier = 1f;
        pauseMultiplier = 1;
    }

    void Update()
    {
        GameTimeDelta = Time.deltaTime * GameTimeMultiplier * pauseMultiplier * slowdownMultiplier;
        DOTween.ManualUpdate(GameTimeDelta, 1f);
    }

    void FixedUpdate()
    {
        GameTimeDeltaFixed = Time.fixedDeltaTime * GameTimeMultiplier * pauseMultiplier * slowdownMultiplier;
        // slowdownMultiplier = Mathf.MoveTowards(slowdownMultiplier, slowdownTarget, GameTimeDeltaFixed * slowdownChangeSpeed);
    }

}
