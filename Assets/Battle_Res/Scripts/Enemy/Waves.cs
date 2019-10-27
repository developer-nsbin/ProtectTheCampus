using UnityEngine;

public class Waves
{
    public GameObject EnemyPrefab;
    public int EnemyCount;
    public float interval;

    public Waves(GameObject obj,int count,float val)
    {
        EnemyPrefab = obj;
        EnemyCount = count;
        interval = val;
    }
}
