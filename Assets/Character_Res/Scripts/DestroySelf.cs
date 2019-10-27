using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
