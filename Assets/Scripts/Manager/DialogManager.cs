using TMPro;
using UnityEngine;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Animation Settings")]
    [SerializeField] private float hiddenYOffset = 250f;   // how far above the landing position it starts from
    [SerializeField] private float extraDownOffset = 80f;  // how much lower than the original position it lands
    [SerializeField] private float slideDuration = 0.4f;
    [SerializeField] private Ease slideInEase = Ease.OutBack;
    [SerializeField] private Ease slideOutEase = Ease.InBack;
    [SerializeField] private float displayDuration = 2.5f;

    private RectTransform dialogRect;
    private Vector2 originalPosition;
    private Vector2 shownPosition;
    private Sequence dialogSequence;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialogRect = dialogBox.GetComponent<RectTransform>();
        originalPosition = dialogRect.anchoredPosition;
        shownPosition = originalPosition - new Vector2(0f, extraDownOffset);
    }

    public void ShowDialog(string text)
    {
        dialogText.text = text;
        dialogBox.SetActive(true);

        dialogSequence?.Kill();

        Vector2 hiddenPosition = shownPosition + new Vector2(0f, hiddenYOffset);
        dialogRect.anchoredPosition = hiddenPosition;

        dialogSequence = DOTween.Sequence();

        dialogSequence.Append(dialogRect.DOAnchorPos(shownPosition, slideDuration).SetEase(slideInEase));
        dialogSequence.AppendInterval(displayDuration);
        dialogSequence.Append(dialogRect.DOAnchorPos(hiddenPosition, slideDuration).SetEase(slideOutEase));
        dialogSequence.OnComplete(() => dialogBox.SetActive(false));
    }
}