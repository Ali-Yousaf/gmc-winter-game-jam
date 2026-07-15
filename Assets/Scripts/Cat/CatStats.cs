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

    [Header("Tween Settings")]
    [SerializeField] private float tweenDuration = 0.4f;
    [SerializeField] private Ease tweenEase = Ease.OutQuad;
    [SerializeField] private float punchScale = 0.15f;
    [SerializeField] private float punchDuration = 0.3f;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
            
    }

    private void Start()
    {
        moodSlider.value = moodSlider.maxValue;
        hungerSlider.value = hungerSlider.maxValue;
        loveSlider.value = loveSlider.maxValue;
    }

    private void Update()
    {
        DecaySlider(moodSlider, moodDecayRate);
        DecaySlider(hungerSlider, hungerDecayRate);
        DecaySlider(loveSlider, loveDecayRate);
    }

    private void DecaySlider(Slider slider, float rate)
    {
        slider.value = Mathf.Max(slider.minValue, slider.value - rate * Time.deltaTime);
    }

    public void IncreaseMood() => AnimateIncrease(moodSlider, moodIncreaseAmount);
    public void IncreaseHunger() => AnimateIncrease(hungerSlider, hungerIncreaseAmount);
    public void IncreaseLove() => AnimateIncrease(loveSlider, loveIncreaseAmount);

    private void AnimateIncrease(Slider slider, float amount)
    {
        float target = Mathf.Min(slider.maxValue, slider.value + amount);

        slider.DOKill();
        slider.DOValue(target, tweenDuration).SetEase(tweenEase);

        slider.transform.DOKill();
        slider.transform.DOPunchScale(Vector3.one * punchScale, punchDuration, 4, 0.5f);
    }
}