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
        StartCoroutine(TransitionToScene("Menu", startScene)); // ִ��Э��
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
    /// ��A����ǰ��B����
    /// </summary>
    public void Transition(string from,string to)
    {
        if(!isFade&& canTransition)
        StartCoroutine(TransitionToScene(from,to)); // ִ��Э��
    }

    /// <summary>
    /// Э�̷�����ж��A������װ�ز�����B����
    /// </summary>
    private IEnumerator TransitionToScene(string from,string to)
    {
        yield return Fade(1); //  �ȴ�ִ������ִ����һ��
                              //StartCoroutine(Fade(1)); // ������ͬʱִ��
        if (from != string.Empty)
        {
            // ��Ϊ�ղ�ж�س���
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from); // �첽ж��A����
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive); // �첽����B�����������ӵ�Persistent����֮��

        // �����³���Ϊ�����
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();
        yield return Fade(0);
    }

    /// <summary>
    /// ���뵭������
    /// </summary>
    /// <param name="targetAlpha">1�Ǻ�0��͸��</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed=Mathf.Abs(fadeCanvasGroup.alpha-targetAlpha)/fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            // ��������
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
