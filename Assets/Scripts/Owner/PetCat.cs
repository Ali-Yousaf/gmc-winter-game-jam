using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PetCat : MonoBehaviour
{
    [Header("Heart Setup")]
    [SerializeField] private Sprite[] heartSprites;      // assign your 5 heart pngs here
    [SerializeField] private RectTransform heartsParent;  // a Canvas or panel RectTransform where hearts will spawn
    [SerializeField] private RectTransform spawnPoint;    // optional: where hearts appear from (e.g. above the cat)

    [Header("Timing")]
    [SerializeField] private float displayDuration = 3.5f;
    [SerializeField] private int heartsPerPet = 5;
    [SerializeField] private float spawnInterval = 0.1f;

    [Header("Movement")]
    [SerializeField] private float floatDistance = 150f;
    [SerializeField] private float horizontalSpread = 60f;
    [SerializeField] private float heartScale = 1f;

    public void PetTheCat()
    {
        for (int i = 0; i < heartsPerPet; i++)
        {
            float delay = i * spawnInterval;
            DOVirtual.DelayedCall(delay, SpawnHeart);
        }
    }

    private void SpawnHeart()
    {
        if (heartSprites == null || heartSprites.Length == 0 || heartsParent == null)
        {
            Debug.LogWarning("PetCat: heartSprites or heartsParent not assigned.");
            return;
        }

        // Create the heart GameObject
        GameObject heartObj = new GameObject("Heart", typeof(RectTransform), typeof(Image));
        heartObj.transform.SetParent(heartsParent, false);

        Image heartImage = heartObj.GetComponent<Image>();
        heartImage.sprite = heartSprites[Random.Range(0, heartSprites.Length)];
        heartImage.raycastTarget = false;

        RectTransform rt = heartObj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(80, 80);

        // Position it at spawnPoint (or center of parent if none set)
        Vector2 startPos = spawnPoint != null
            ? heartsParent.InverseTransformPoint(spawnPoint.position)
            : Vector2.zero;

        startPos.x += Random.Range(-horizontalSpread, horizontalSpread);
        rt.anchoredPosition = startPos;
        rt.localScale = Vector3.zero;
        rt.localRotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));

        // Make sure it's fully visible at start
        Color c = heartImage.color;
        c.a = 1f;
        heartImage.color = c;

        // Build the animation sequence
        Sequence seq = DOTween.Sequence();

        // Pop-in
        seq.Append(rt.DOScale(heartScale, 0.35f).SetEase(Ease.OutBack));

        // Float upward + slight horizontal drift, running alongside the rest
        float endX = startPos.x + Random.Range(-30f, 30f);
        seq.Join(rt.DOAnchorPos(new Vector2(endX, startPos.y + floatDistance), displayDuration)
                   .SetEase(Ease.OutSine));

        // Gentle wobble rotation
        seq.Join(rt.DORotate(new Vector3(0, 0, Random.Range(-10f, 10f)), 0.6f)
                   .SetLoops(4, LoopType.Yoyo)
                   .SetEase(Ease.InOutSine));

        // Fade out near the end
        seq.Insert(displayDuration - 0.8f, heartImage.DOFade(0f, 0.8f));

        // Cleanup
        seq.OnComplete(() => Destroy(heartObj));
    }
}