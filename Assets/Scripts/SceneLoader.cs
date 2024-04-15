using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool _loadGameplaySceneAtStart;
    [SerializeField] private string _gameplaySceneName = "Game";

    void Start()
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
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_gameplaySceneName);
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            timer.Stop();
            print("done loading level, it took " + timer.ElapsedMilliseconds / 1000f + " seconds");
        }
    }
}
