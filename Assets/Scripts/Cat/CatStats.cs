using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CatStats : MonoBehaviour
{
    public static CatStats Instance;

    [Header("Sliders")]
    [SerializeField] private Slider moodSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider loveSlider;

    [Header("Decay Rates (per second)")]
    [SerializeField] private float moodDecayRate = 1f;
    [SerializeField] private float hungerDecayRate = 1.5f;
    [SerializeField] private float loveDecayRate = 1f;

    [Header("Increase Amounts")]
    [SerializeField] private float moodIncreaseAmount = 20f;
    [SerializeField] private float hungerIncreaseAmount = 25f;
    [SerializeField] private float loveIncreaseAmount = 20f;

    [Header("UI Animation")]
    [SerializeField] private float sliderSmoothSpeed = 100f;
    [SerializeField] private float punchScale = 0.15f;
    [SerializeField] private float punchDuration = 0.3f;

    private float mood;
    private float hunger;
    private float love;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        mood = moodSlider.maxValue;
        hunger = hungerSlider.maxValue;
        love = loveSlider.maxValue;

        moodSlider.value = mood;
        hungerSlider.value = hunger;
        loveSlider.value = love;
    }

    private void Update()
    {
        // Decay stats
        mood = Mathf.Max(moodSlider.minValue, mood - moodDecayRate);
        hunger = Mathf.Max(hungerSlider.minValue, hunger - hungerDecayRate);
        love = Mathf.Max(loveSlider.minValue, love - loveDecayRate);

        // Smoothly update UI
        moodSlider.value = Mathf.MoveTowards(moodSlider.value, mood, sliderSmoothSpeed);
        hungerSlider.value = Mathf.MoveTowards(hungerSlider.value, hunger, sliderSmoothSpeed);
        loveSlider.value = Mathf.MoveTowards(loveSlider.value, love, sliderSmoothSpeed);
    }

    public void IncreaseMood()
    {
        mood = Mathf.Min(moodSlider.maxValue, mood + moodIncreaseAmount);
        PunchSlider(moodSlider);
    }

    public void IncreaseHunger()
    {
        hunger = Mathf.Min(hungerSlider.maxValue, hunger + hungerIncreaseAmount);
        PunchSlider(hungerSlider);
    }

    public void IncreaseLove()
    {
        love = Mathf.Min(loveSlider.maxValue, love + loveIncreaseAmount);
        PunchSlider(loveSlider);
    }

    private void PunchSlider(Slider slider)
    {
        slider.transform.DOKill();

        slider.transform.DOPunchScale(
            Vector3.one * punchScale,
            punchDuration,
            4,
            0.5f
        );
    }
}