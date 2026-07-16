using UnityEngine;

public class OwnerTimerManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            DialogManager.Instance.ShowDialog("Owner is LEAVING!");
        }
    }
}
