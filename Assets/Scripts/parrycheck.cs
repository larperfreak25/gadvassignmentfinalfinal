using UnityEngine;
using TMPro;
using System.Collections;

public class ParryCheck : MonoBehaviour
{
    public PlayerCombat player;
    public TextMeshProUGUI promptText;

    [Header("Timing")]
    public float minInterval = 3f;
    public float maxInterval = 6f;
    public float windowDuration = 0.6f; //reaction window, really tight lowkey but like boss stage is meant to be hard
    public float resultDisplayTime = 0.5f;

    void Start()
    {
        promptText.text = "";
        StartCoroutine(ParryLoop());
    }

    IEnumerator ParryLoop() //loops for thbe boss fight at a custom interval (i put 6 7 lol)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            yield return StartCoroutine(ParryWindow());
        }
    }

    IEnumerator ParryWindow()
    {
        promptText.text = "PARRY! [SPACE]";

        float t = 0f;
        bool success = false;

        while (t < windowDuration)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                success = true;
                break; //reacted stop check
            }
            t += Time.deltaTime;
            yield return null;
        }

        promptText.text = success ? "parried!" : "missed!";

        if (!success)
        {
            player.TakeDamage(); //didnt react DIEEEE
        }

        yield return new WaitForSeconds(resultDisplayTime);
        promptText.text = "";
    }
}
