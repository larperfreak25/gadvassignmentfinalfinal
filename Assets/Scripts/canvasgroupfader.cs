using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))] //forces the object to have a canvasgroup or it BREAKS
public class CanvasGroupFader : MonoBehaviour
{
    public float fadeDuration = 0.3f; 

    private CanvasGroup group;
    private Coroutine running;

    void Awake()
    {
        group = GetComponent<CanvasGroup>(); //grabs the canvasgroup so i can mess w alpha later (0 = invisible 1 = visible)
    }

    public void FadeIn() 
    {
        gameObject.SetActive(true); //has to be active first and fades form nothing always
        group.alpha = 0f; 
        if (running != null) StopCoroutine(running); 
        running = StartCoroutine(FadeTo(1f, null));
    }

    public void FadeOut() 
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(FadeTo(0f, () => gameObject.SetActive(false))); //fades out then truns off so it doesnt block clicks
    }

    private IEnumerator FadeTo(float target, System.Action onDone) //onDone runs right when the fade finishes 
    {
        float start = group.alpha;
        float t = 0f;
        while (t < fadeDuration) 
        {
            t += Time.unscaledDeltaTime;
            group.alpha = Mathf.Lerp(start, target, t / fadeDuration); //moves smoothly from start to target
            yield return null;
        }
        group.alpha = target; 
        onDone?.Invoke(); //only runs if ondone has smthf in it
    }
}