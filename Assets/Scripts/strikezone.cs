using UnityEngine;

public class StrikeZone : MonoBehaviour
{
    public GameObject currentTarget; //whatever enemy is standing in this zone

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
            currentTarget = null; //only clear it if the thing leaving is actually the current target
        }
    }
}
