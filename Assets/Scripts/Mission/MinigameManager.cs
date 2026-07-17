using UnityEngine;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    [Header("Minigame References")]
    [SerializeField] private ChessPuzzleMinigame chessMinigame;

    [SerializeField] private GameObject minigamePanelRoot;

    // Defines the fixed order every mission plays through
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
        DeactivateAllMinigames();

        if (currentSequence.Count == 0)
        {
            // All minigames in this mission finished
            minigamePanelRoot.SetActive(false);
            DialogManager.Instance.ShowDialog("Mission Complete!");
            return;
        }

        MinigameType nextType = currentSequence.Dequeue();
        LaunchSingleMinigame(nextType, currentDifficulty);
    }

    private void LaunchSingleMinigame(MinigameType type, Difficulty difficulty)
    {
        switch (type)
        {
            case MinigameType.Chess:
                chessMinigame.gameObject.SetActive(true);
                chessMinigame.Launch(difficulty);
                break;
        }
    }

    private void DeactivateAllMinigames()
    {
        chessMinigame.gameObject.SetActive(false);
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