using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OwnerTimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float minTime = 30f;
    [SerializeField] private float maxTime = 45f;

    [Header("Dialog Settings")]
    [SerializeField] private string dialogMessage = "Owner is LEAVING!";
    [SerializeField] private float delayBeforeSceneChange = 3f;

    public LevelLoader levelLoader;

    private void Start()
    {
        StartCoroutine(OwnerLeavesRoutine());
    }

    private IEnumerator OwnerLeavesRoutine()
    {
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);

        DialogManager.Instance.ShowDialog(dialogMessage);

        yield return new WaitForSeconds(delayBeforeSceneChange);
        
        levelLoader.LoadNoOwnerScene();
    }
}