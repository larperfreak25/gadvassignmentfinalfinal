using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    public StrikeZone leftZone;
    public StrikeZone rightZone;
    public TextMeshProUGUI feedbackText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack(leftZone);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Attack(rightZone);
        }
    }

    void Attack(StrikeZone zone)
    {
        if (zone.currentTarget != null)
        {
            Destroy(zone.currentTarget);
            feedbackText.text = "perfect";
        }
        else
        {
            feedbackText.text = "miss";
        }
    }
}