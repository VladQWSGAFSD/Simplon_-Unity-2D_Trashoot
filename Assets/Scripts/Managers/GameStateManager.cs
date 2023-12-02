using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour, IGameOver
{
    //you have to subscribe to onFadeComplete from SceneTransitionManager in order to restart the game once fade is done
    // why doesnt         SceneTransitionManager.Instance.FadeFromBlack(() => seem to work???
    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// <exception cref="https://forum.unity.com/threads/pause-using-timescale-0.616324/">description</exception> 
    /// </summary>
    public void TriggerGameOver()
    {
        SceneTransitionManager.Instance.FadeToBlack(() =>
        {
            Time.timeScale = 0; // Pause the game?
            UIManager.Instance.ShowGameOverScreen(ScoreManager.Instance.score);
        });
    }

    public void RestartGame()
    {
        UIManager.Instance.HideGameOverScreen();
        SceneTransitionManager.Instance.FadeFromBlack(() =>
        {
            Time.timeScale = 1; // Resume the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the current scene
        });
    }
}
