using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))] //needs text on teh same object or it will break
public class ScoreCounter : MonoBehaviour
{
    public float countDuration = 0.25f; 

    private TextMeshProUGUI label;
    private int displayedScore = 0; //whats actually shown on screen
    private Coroutine running;

    void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    public void AnimateTo(int newScore) //call whenever real score changes
    {
        if (running != null) StopCoroutine(running); //cancels the old count if a new hit comes in early
        running = StartCoroutine(CountRoutine(newScore));
    }

    private IEnumerator CountRoutine(int target)
    {
        int start = displayedScore;
        float t = 0f;
        while (t < countDuration)
        {
            t += Time.unscaledDeltaTime;
            displayedScore = Mathf.RoundToInt(Mathf.Lerp(start, target, t / countDuration)); //roundtoint cuz you cant show half a point
            label.text = "score:" + displayedScore;
            yield return null;
        }
        displayedScore = target; //makes sure it ends ujp being the final real number
        label.text = "score:" + displayedScore;
    }
}