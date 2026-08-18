using UnityEngine;
using System.Collections.Generic;

public class RhythmSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AudioSource musicSource;
    public TextAsset beatmapFile; //beatmpa files i got from osu and had it converted to just text files
    public float spawnDistance = 10f;

    [Header("Get Ready Prompt")]
    public GameObject getReadyPanel;
    public CanvasGroupFader getReadyFader;

    private List<float> beatTimes = new List<float>(); //final beats list (omitted early beats cus of travel time and also like these osu maps were TOO hard had to trim down a LOT)
    private int nextBeatIndex = 0;
    private float travelTime; //time taken for enemy to travel to center
    private bool nextIsLeft = true;

    void Start()
    {
        float enemySpeed = enemyPrefab.GetComponent<EnemyMovement>().speed;
        travelTime = spawnDistance / enemySpeed;

        LoadBeatmap();
        musicSource.Play();

        if (getReadyFader != null) getReadyFader.FadeIn();
        else if (getReadyPanel != null)
        {
            getReadyPanel.SetActive(true);
        }
    }

    void LoadBeatmap()
    {
        string[] lines = beatmapFile.text.Split('\n');
        foreach (string line in lines)
        {
            if (float.TryParse(line.Trim(), out float t))
            {
                if (t >= travelTime) //makin sure enemy can reach
                {
                    beatTimes.Add(t);
                }
            }
        }
    }

    void Update()
    {
        if (nextBeatIndex >= beatTimes.Count) return;

        float spawnTime = beatTimes[nextBeatIndex] - travelTime; //spawns early to make sure enemy lines up w the beat
        if (musicSource.time >= spawnTime)
        {
            SpawnEnemy();
            nextBeatIndex++;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = nextIsLeft ? new Vector3(-spawnDistance, 0f, 0f) : new Vector3(spawnDistance, 0f, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        nextIsLeft = !nextIsLeft; //alternate sides each spawn

        if (getReadyFader != null)
        {
            if (getReadyFader.gameObject.activeSelf) getReadyFader.FadeOut(); //only fade once
        }
        else if (getReadyPanel != null && getReadyPanel.activeSelf)
        {
            getReadyPanel.SetActive(false);
        }
    }
}
