using UnityEngine;

//old static spawner befopre the mapped one not used lazy to remove incase it breaks smth
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    private float timer = 0f;

    public int enemiesSpawned = 0;
    public int maxEnemies = 50;

    void Update()
    {
        if (enemiesSpawned >= maxEnemies)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int side = Random.Range(0, 2); //0 or 1, picks a side randomly
        Vector3 spawnPosition;

        if (side == 0)
        {
            spawnPosition = new Vector3(-10f, 0f, 0f);
        }
        else
        {
            spawnPosition = new Vector3(10f, 0f, 0f);
        }

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemiesSpawned++;
    }
}
