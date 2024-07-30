using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        // ������Ϸ����
        SaveLoadManager.Instance.Load();
    }

    public void GoBackToMenu()
    {
        var currentScene=SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");

        // ������Ϸ����
        SaveLoadManager.Instance.Save();
    }

    public void StartGameWeek(int gameWeek)
    {
        EventHandler.CallStarNewGameEvent(gameWeek);
    }

}
