using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public GameObject defeatMenuPanel;
    public GameObject victoryMenuPanel;
    public RhythmSpawner spawner;

    public CanvasGroupFader defeatFader;
    public CanvasGroupFader victoryFader;

    public void ShowDefeatScreen()
    {
        if (defeatFader != null) defeatFader.FadeIn(); //fades clean wow
        else defeatMenuPanel.SetActive(true);
        StopGame();
    }

    public void ShowVictoryScreen()
    {
        if (victoryFader != null) victoryFader.FadeIn();
        else victoryMenuPanel.SetActive(true);
        StopGame();
    }

    void StopGame()
    {
        spawner.enabled = false; //stops spawning
        spawner.musicSource.Pause();
        Time.timeScale = 0f; //freezes everything (important ) cus unscaled time stuff will still work (like all thje ui)
    }

    public void RetryStage()
    {
        Time.timeScale = 1f; //gotta unfreeze or else it goes over
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextStage()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); //main menu awlays first scene (0 in buildsettinfs)
    }
}
