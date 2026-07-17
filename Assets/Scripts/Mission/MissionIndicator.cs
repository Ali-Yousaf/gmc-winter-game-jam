using UnityEngine;
using TMPro;

public enum MinigameType { Chess, GeoGuess, Unscramble, Simon }
public enum Difficulty { Easy, Medium, Hard }

public class MissionIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private string catTag = "Player";

    [Header("Mission Config")]
    [SerializeField] private Difficulty difficulty;

    private bool catNearby;

    void Start()
    {
        interactionText.text = "";
    }

    void Update()
    {
        if (catNearby && Input.GetKeyDown(KeyCode.E))
        {
            StartMission();
            Destroy(gameObject);
        }
    }

    void StartMission()
    {
        interactionText.text = "";
        MinigameManager.Instance.LaunchMissionSequence(difficulty);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(catTag))
        {
            catNearby = true;
            interactionText.text = "Press E to Start Mission";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(catTag))
        {
            catNearby = false;
            interactionText.text = "";
        }
    }
}