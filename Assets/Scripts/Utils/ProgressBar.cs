using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar Instance;

    [SerializeField] private Slider progressSlider;

    void Awake()
    {
        if(Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }

    void Start()
    {
        progressSlider.minValue = 0;
        progressSlider.maxValue = 100;
        progressSlider.value = 0;
    }

    public void Progress(int amount)
    {
        progressSlider.value += amount;

        if (progressSlider.value >= progressSlider.maxValue)
        {
            //GameManager.Instance.FreeCat();
        }
}
}
