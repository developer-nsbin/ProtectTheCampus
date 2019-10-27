using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [HideInInspector]
    public Transform[] spawnPoints;
    public static Transform[] middleWay;
    public static Transform[] leftWay;
    public static Transform[] rightWay;

    private void Awake()
    {
        InitialWayPoint();
    }

    void InitialWayPoint()
    {
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == 0)
            {
                middleWay = new Transform[spawnPoints[i].childCount];
                for (int j = 0; j < middleWay.Length; j++)
                {
                    middleWay[j] = spawnPoints[i].GetChild(j);
                }
            }
            else if (i == 1)
            {
                leftWay = new Transform[spawnPoints[i].childCount];
                for (int j = 0; j < leftWay.Length; j++)
                {
                    leftWay[j] = spawnPoints[i].GetChild(j);
                }
            }
            else if (i == 2)
            {
                rightWay = new Transform[spawnPoints[i].childCount];
                for (int j = 0; j < rightWay.Length; j++)
                {
                    rightWay[j] = spawnPoints[i].GetChild(j);
                }
            }
        }
    }
}
