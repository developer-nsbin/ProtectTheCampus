using System.Collections;
using UnityEngine;

public class InitEnemySpawn : Singleton<InitEnemySpawn>
{
    public GameObject[] enemies;

    private Transform[] spawnPointTrans;
    private int spawnNum=0;

    void Start()
    {
        spawnNum = transform.childCount;
        spawnPointTrans=new Transform[spawnNum];
        for (int i = 0; i < spawnNum; i++)
        {
            spawnPointTrans[i] = transform.GetChild(i);
        }

        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            Instantiate(enemies[Random.Range(0, 2)], spawnPointTrans[i].position, spawnPointTrans[i].rotation,
                spawnPointTrans[i]);
        }
    }

    public void SpawnOneEnemy()
    {
        StartCoroutine(SpawnOneEnemyIenumeraotr());
    }

    IEnumerator SpawnOneEnemyIenumeraotr()
    {
        yield return new WaitForSeconds(20);
        for (int i = 0; i < spawnNum; i++)
        {
            if (transform.GetChild(i).childCount <= 0)
            {
                Instantiate(enemies[Random.Range(0, 2)], transform.GetChild(i).position, transform.GetChild(i).rotation,
                    transform.GetChild(i));
            }
        }

    }
}
