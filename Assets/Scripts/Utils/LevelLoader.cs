using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            LoadOwnerScene();
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNoOwnerScene();
        }
    }

    public void LoadOwnerScene()
    {
        StartCoroutine(LoadScene("Owner"));
    }  

    public void LoadNoOwnerScene()
    {
    StartCoroutine(LoadScene("No Owner"));
    }  

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
