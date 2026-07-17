using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeoGuessMinigame : MonoBehaviour
{
    [System.Serializable]
    public class GeoGuessData
    {
        public Sprite locationImage;
        public string[] choices;      // e.g. "Japan", "Italy", "Brazil", "Canada"
        public int correctChoiceIndex; // index into choices[] that's the right answer
    }

    [Header("Puzzle Pools")]
    [SerializeField] private GeoGuessData[] easyPuzzles;
    [SerializeField] private GeoGuessData[] mediumPuzzles;
    [SerializeField] private GeoGuessData[] hardPuzzles;

    [Header("UI References")]
    [SerializeField] private Image displayImage;
    [SerializeField] private Button[] choiceButtons;   // must match choices[] length (e.g. 4 buttons)
    [SerializeField] private TextMeshProUGUI[] choiceLabels; // text component on each button

    private int correctIndex;

    public void Launch(Difficulty difficulty)
    {
        GeoGuessData[] pool = difficulty switch
        {
            Difficulty.Easy => easyPuzzles,
            Difficulty.Medium => mediumPuzzles,
            Difficulty.Hard => hardPuzzles,
            _ => easyPuzzles
        };

        GeoGuessData puzzle = pool[Random.Range(0, pool.Length)];

        displayImage.sprite = puzzle.locationImage;
        correctIndex = puzzle.correctChoiceIndex;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < puzzle.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceLabels[i].text = puzzle.choices[i];

                int capturedIndex = i; // avoid closure bug in loop
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => SubmitAnswer(capturedIndex));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SubmitAnswer(int chosenIndex)
    {
        bool isCorrect = chosenIndex == correctIndex;

        if (isCorrect)
        {
            gameObject.SetActive(false);
        }

        MinigameManager.Instance.OnMinigameComplete(isCorrect);
    }
}