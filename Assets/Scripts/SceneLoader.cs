using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool _loadGameplaySceneAtStart;
    [SerializeField] private string _gameplaySceneName = "Game";

    public static SceneLoader Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_loadGameplaySceneAtStart)
        {
            LoadGameScene();
        }
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadGamePlayScene());
        
        IEnumerator LoadGamePlayScene()
        {
            // var minLoadingTimeInMs = 2000;
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_gameplaySceneName);

            UIManager.Instance.ShowLoadingScreen();
            while (!asyncLoad.isDone) //|| timer.ElapsedMilliseconds < minLoadingTimeInMs)
            {
                yield return null;
            }
            UIManager.Instance.HideLoadingScreen();

            timer.Stop();
            print("done loading level, it took " + timer.ElapsedMilliseconds / 1000f + " seconds");
        }
    }

    public void UnloadGameScene()
    {
        AsyncOperation asyncUnload = SceneManager.LoadSceneAsync("EmptyScene");
    }
}
