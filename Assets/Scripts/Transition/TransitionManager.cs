using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>,Isaveable
{
    public string startScene;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;
    private bool canTransition;

    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
        EventHandler.StarNewGameEvent += OnStarNewGameEvent;
    }

    

    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
        EventHandler.StarNewGameEvent -= OnStarNewGameEvent;
    }
    private void OnStarNewGameEvent(int obj)
    {
        StartCoroutine(TransitionToScene("Menu", startScene)); // 执行协程
    }

    private void Start()
    {
        Isaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }
    /// <summary>
    /// 从A场景前往B场景
    /// </summary>
    public void Transition(string from,string to)
    {
        if(!isFade&& canTransition)
        StartCoroutine(TransitionToScene(from,to)); // 执行协程
    }

    /// <summary>
    /// 协程方法，卸载A场景，装载并激活B场景
    /// </summary>
    private IEnumerator TransitionToScene(string from,string to)
    {
        yield return Fade(1); //  等待执行完再执行下一个
                              //StartCoroutine(Fade(1)); // 让它们同时执行
        if (from != string.Empty)
        {
            // 不为空才卸载场景
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from); // 异步卸载A场景
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive); // 异步加载B场景，并叠加到Persistent场景之上

        // 设置新场景为激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();
        yield return Fade(0);
    }

    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="targetAlpha">1是黑0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed=Mathf.Abs(fadeCanvasGroup.alpha-targetAlpha)/fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            // 缓慢渐变
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade= false;
    }

    public GameSaveData GeneratesaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.currentScene = SceneManager.GetActiveScene().name;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        Transition("Menu",saveData.currentScene);
    }
}
