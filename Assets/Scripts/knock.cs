using UnityEngine;

public class Knockaway : MonoBehaviour
{
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(Random.Range(-5f, 5f), 10f), ForceMode2D.Impulse); //random so its like chaotic and silly

        rb.AddTorque(Random.Range(-10f, 10f), ForceMode2D.Impulse); //random spin ^^^

        Destroy(gameObject, 2f); //cleans itself up
    }
}
