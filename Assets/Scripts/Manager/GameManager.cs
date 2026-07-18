using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float gameOverDisplayDuration = 2.5f;

    [Header("Animation Settings")]
    [SerializeField] private float animInDuration = 0.5f;
    [SerializeField] private float animOutDuration = 0.4f;
    [SerializeField] private Ease animInEase = Ease.OutBack;
    [SerializeField] private Ease animOutEase = Ease.InBack;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.zero;

        if (gameOverCanvasGroup != null)
            gameOverCanvasGroup.alpha = 0f;

        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        Sequence introSequence = DOTween.Sequence();
        introSequence.Append(gameOverPanel.transform.DOScale(1f, animInDuration).SetEase(animInEase));

        if (gameOverCanvasGroup != null)
            introSequence.Join(gameOverCanvasGroup.DOFade(1f, animInDuration));

        yield return introSequence.WaitForCompletion();

        yield return new WaitForSeconds(gameOverDisplayDuration);

        Sequence outroSequence = DOTween.Sequence();
        outroSequence.Append(gameOverPanel.transform.DOScale(0f, animOutDuration).SetEase(animOutEase));

        if (gameOverCanvasGroup != null)
            outroSequence.Join(gameOverCanvasGroup.DOFade(0f, animOutDuration));

        yield return outroSequence.WaitForCompletion();

        RestartNoOwner();
    }

    public void RestartNoOwner()
    {
        SceneManager.LoadScene("No Owner");
    }
}