using UnityEngine;

public class StrikeZone : MonoBehaviour
{
    public GameObject currentTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentTarget = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && currentTarget == other.gameObject)
        {
            currentTarget = null;
        }
    }
}