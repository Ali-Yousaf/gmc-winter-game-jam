using UnityEngine;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    [Header("Minigame References")]
    [SerializeField] private ChessPuzzleMinigame chessMinigame;
    [SerializeField] private GeoGuessMinigame geoGuessMinigame;

    [SerializeField] private GameObject minigamePanelRoot;

    private readonly MinigameType[] sequenceOrder =
    {
        MinigameType.Chess,
        MinigameType.GeoGuess,
        MinigameType.Unscramble
    };

    private Queue<MinigameType> currentSequence;
    private Difficulty currentDifficulty;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        minigamePanelRoot.SetActive(false);
        chessMinigame.gameObject.SetActive(false);
        geoGuessMinigame.gameObject.SetActive(false);
    }

    public void LaunchMissionSequence(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
        currentSequence = new Queue<MinigameType>(sequenceOrder);

        minigamePanelRoot.SetActive(true);
        PlayNextInSequence();
    }

    private void PlayNextInSequence()
    {
        if (currentSequence.Count == 0)
        {
            chessMinigame.gameObject.SetActive(false);
            geoGuessMinigame.gameObject.SetActive(false);

            minigamePanelRoot.SetActive(false);
            DialogManager.Instance.ShowDialog("Mission Complete!");
            return;
        }

        MinigameType nextType = currentSequence.Dequeue();

        switch (nextType)
        {
            case MinigameType.Chess:
                chessMinigame.gameObject.SetActive(true);
                geoGuessMinigame.gameObject.SetActive(false);
                chessMinigame.Launch(currentDifficulty);
                break;

            case MinigameType.GeoGuess:
                chessMinigame.gameObject.SetActive(false);
                geoGuessMinigame.gameObject.SetActive(true);
                geoGuessMinigame.Launch(currentDifficulty);
                break;
        }
    }

    public void OnMinigameComplete(bool success)
    {
        if (success)
        {
            DialogManager.Instance.ShowDialog("Correct! Next up...");
            PlayNextInSequence();
        }
        else
        {
            DialogManager.Instance.ShowDialog("Wrong, try again!");
        }
    }
}