using UnityEngine;
using System.Collections;

public class UIPunch : MonoBehaviour
{
    public float punchScale = 1.3f;
    public float duration = 0.15f; //punchscale is how big the object increases to and duration is for how long then returns to the editor scale

    private Vector3 baseScale;
    private Coroutine running;

    void Awake() 
    {
        baseScale = transform.localScale;
    }
    public void Punch() //method other scripts call
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(PunchRoutine()); //so that it doesnt stack
    }
    private IEnumerator PunchRoutine()
    {
        float t = 0f;
        while (t < duration) //got some help with this part, understanding is that progress will go at a constant rate for the duration, and1f + (punchScale - 1f) * Mathf.Sin(progress * Mathf.PI) makes it so taht it like pops because sine will speed up the start and end parts, and then slows when its in the middle
        {
            t += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(t / duration);
            float scaleAmount = 1f + (punchScale - 1f) * Mathf.Sin(progress * Mathf.PI);
            transform.localScale = baseScale * scaleAmount;
            yield return null;
        }
        transform.localScale = baseScale;
    }
}
