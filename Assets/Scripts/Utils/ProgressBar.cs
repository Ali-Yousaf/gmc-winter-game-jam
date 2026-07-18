using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;

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
