using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject stageSelectPanel;

    public CanvasGroupFader titleFader;
    public CanvasGroupFader stageSelectFader;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip backSound;

    public void ShowStageSelect()
    {
        audioSource.PlayOneShot(clickSound); //click sound fades title for stage select blahblah
        if (titleFader != null) titleFader.FadeOut();
        else titlePanel.SetActive(false);
        if (stageSelectFader != null) stageSelectFader.FadeIn();
        else stageSelectPanel.SetActive(true);
    }

    public void BackToTitle()
    {
        audioSource.PlayOneShot(backSound); //opposite of ^^^
        if (stageSelectFader != null) stageSelectFader.FadeOut();
        else stageSelectPanel.SetActive(false);
        if (titleFader != null) titleFader.FadeIn();
        else titlePanel.SetActive(true);
    }

    public void LoadStage(int stageIndex) //stageIndex for the button number in inspcector
    {
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene(stageIndex);
    }
}
