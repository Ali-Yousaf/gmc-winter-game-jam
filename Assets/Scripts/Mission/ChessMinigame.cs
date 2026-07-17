using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChessPuzzleMinigame : MonoBehaviour
{
    [System.Serializable]
    public class ChessPuzzleData
    {
        public Sprite boardImage;
        public string correctMove;
    }

    [Header("Puzzle Pools")]
    [SerializeField] private ChessPuzzleData[] easyPuzzles;
    [SerializeField] private ChessPuzzleData[] mediumPuzzles;
    [SerializeField] private ChessPuzzleData[] hardPuzzles;

    [Header("UI References")]
    [SerializeField] private Image boardImage;
    [SerializeField] private TMP_InputField answerInput;

    private string correctAnswer;

    void Update()
    {
        if (answerInput.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitAnswer();
        }
    }

    public void Launch(Difficulty difficulty)
    {
        ChessPuzzleData[] pool = difficulty switch
        {
            Difficulty.Easy => easyPuzzles,
            Difficulty.Medium => mediumPuzzles,
            Difficulty.Hard => hardPuzzles,
            _ => easyPuzzles
        };

        ChessPuzzleData puzzle = pool[Random.Range(0, pool.Length)];

        boardImage.sprite = puzzle.boardImage;
        boardImage.SetNativeSize();

        correctAnswer = puzzle.correctMove;
        answerInput.text = "";
    }

    public void SubmitAnswer()
    {
        bool isCorrect = answerInput.text.Trim().Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase);

        if (isCorrect)
        {
            gameObject.SetActive(false);
        }
        else
        {
            answerInput.text = "";
        }

        MinigameManager.Instance.OnMinigameComplete(isCorrect);
    }
}