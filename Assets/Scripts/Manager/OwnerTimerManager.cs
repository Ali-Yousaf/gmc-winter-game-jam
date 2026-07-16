using UnityEngine;
using UnityEngine.SceneManagement;

public class OwnerTimerManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            DialogManager.Instance.ShowDialog("Owner is LEAVING!");
            SceneManager.LoadScene("No Owner");
        }
    }
}
