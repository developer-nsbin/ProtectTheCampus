using UnityEngine;

public class TaskSceneNPC : MonoBehaviour 
{
    void OnMouseDown()
    {
        TaskUIManager.Instance.OnTaskNPCClick();
    }
}
