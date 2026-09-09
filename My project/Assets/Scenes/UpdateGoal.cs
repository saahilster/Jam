using TMPro;
using UnityEngine;

public class UpdateGoal : MonoBehaviour
{
    private string goal = "Goal:";
    public string prompt;
    [SerializeField] TextMeshProUGUI text;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.text = goal + prompt;
            Destroy(gameObject);
        }
    }
}
