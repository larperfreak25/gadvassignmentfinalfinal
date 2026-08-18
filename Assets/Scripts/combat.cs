using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public StrikeZone leftZone;
    public StrikeZone rightZone;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI scoreText;
    public GameMenuController menuController; //tells this to show win lose screens

    [Header("Health UI")]
    public Image[] hearts; //heart icons, index = which heart, used as health goes down
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Visual Effects")]
    public GameObject leftSlashPrefab;
    public GameObject rightSlashPrefab;
    public GameObject enemyRagdollPrefab;
    public Animator anim;
    public float slashDuration = 0.2f; //how long the slash stays before it gets destroyed

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip slashSound;
    public AudioClip hitSound;
    public AudioClip hurtSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    [Header("Sound Volumes")]
    [Range(0f, 2f)] public float slashVolume = 0.2f; //separate sliders so slash doesnt drown out everything else
    [Range(0f, 2f)] public float hitVolume = 1.3f;
    [Range(0f, 2f)] public float hurtVolume = 1.5f;
    [Range(0f, 2f)] public float winVolume = 1.4f;
    [Range(0f, 2f)] public float loseVolume = 1.4f;

    public UIPunch feedbackPunch; //makes the feedback text pop
    public ScoreCounter scoreCounter; //makes the score count up instead of like teleporting
    public UIPunch[] heartPunches;

    public int health = 3;
    public int score = 0;

    public int enemiesHandled = 0; //counts both kills and hits taken toward the win total
    public int enemiesToWin = 50;

    private bool isDead = false; //blocks input once the game is over

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.ResetTrigger("Attack"); //reset first so the animation can replay even mid swing
            anim.SetTrigger("Attack");

            SpawnSlash(leftSlashPrefab, leftZone.transform.position);

            Attack(leftZone);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            anim.ResetTrigger("Attack");
            anim.SetTrigger("Attack");

            SpawnSlash(rightSlashPrefab, rightZone.transform.position);

            Attack(rightZone);
        }
    }

    void SpawnSlash(GameObject slashPrefab, Vector3 spawnPosition)
    {
        GameObject newSlash = Instantiate(slashPrefab, spawnPosition, Quaternion.identity);

        if (slashPrefab == leftSlashPrefab)
        {
            newSlash.GetComponent<SpriteRenderer>().flipX = true; //reusing one sprite jsut flipping for both directions
        }

        Destroy(newSlash, slashDuration);
        audioSource.PlayOneShot(slashSound, slashVolume);
    }

    void Attack(StrikeZone zone)
    {
        if (zone.currentTarget != null)
        {
            //how far the enemy actually is from the center of teh zone
            float distanceToZone = Mathf.Abs(zone.currentTarget.transform.position.x - zone.transform.position.x);

            if (distanceToZone <= 0.3f)
            {
                feedbackText.text = "perfect!";
                if (feedbackPunch != null) feedbackPunch.Punch();
                score += 100;
            }
            else if (distanceToZone <= 0.8f)
            {
                feedbackText.text = "great!";
                if (feedbackPunch != null) feedbackPunch.Punch();
                score += 50;
            }
            else
            {
                feedbackText.text = "okay!";
                if (feedbackPunch != null) feedbackPunch.Punch();
                score += 10;
            }

            if (scoreCounter != null) scoreCounter.AnimateTo(score);
            else scoreText.text = "score:" + score;
            audioSource.PlayOneShot(hitSound, hitVolume);
            Instantiate(enemyRagdollPrefab, zone.currentTarget.transform.position, Quaternion.identity);
            Destroy(zone.currentTarget);

            CheckVictory();
        }
        else
        {
            feedbackText.text = "miss!"; //swung but nothing was in the zone just there to be there cus if it punished would be WAYYY too hard
            if (feedbackPunch != null) feedbackPunch.Punch();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy")) 
        {
            TakeDamage();
            Destroy(other.gameObject);
            CheckVictory();
        }
    }

    public void TakeDamage() //public cus wanted to make parrycheck on bsos stage punishing
    {
        health--;

        if (health >= 0 && health < hearts.Length)
        {
            hearts[health].sprite = emptyHeart; //health also doubles as the heart index

            if (heartPunches != null && health < heartPunches.Length && heartPunches[health] != null)
            {
                heartPunches[health].Punch();
            }
        }

        if (health <= 0)
        {
            StartCoroutine(GameOverRoutine());
        }
        else
        {
            feedbackText.text = "hurt";
            if (feedbackPunch != null) feedbackPunch.Punch();
            audioSource.PlayOneShot(hurtSound, hurtVolume);
        }
    }

    IEnumerator GameOverRoutine() //coroutine so i can wait before switching scenes
    {
        isDead = true;
        anim.SetTrigger("Die");
        feedbackText.text = "game over";
        if (feedbackPunch != null) feedbackPunch.Punch();
        audioSource.PlayOneShot(loseSound, loseVolume);

        yield return new WaitForSeconds(1.5f); //give the animation and sound time to actually play
        menuController.ShowDefeatScreen();
    }

    void CheckVictory()
    {
        enemiesHandled++;

        if (enemiesHandled >= enemiesToWin && health > 0)
        {
            feedbackText.text = "win";
            if (feedbackPunch != null) feedbackPunch.Punch();
            audioSource.PlayOneShot(winSound, winVolume);
            menuController.ShowVictoryScreen();
        }
    }
}
