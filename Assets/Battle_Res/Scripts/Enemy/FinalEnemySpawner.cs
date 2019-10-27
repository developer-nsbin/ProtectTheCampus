using System.Collections;
using UnityEngine;

public class FinalEnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    [HideInInspector] public int EnemyLiveCount = 0;
    [HideInInspector] public Transform[] wayPoints;

    private Waves[] waves;

    void Awake()
    {
        wayPoints =new Transform[transform.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }

    void Start()
    {
        WavesAssignment();
        StartCoroutine(SpawerEnemy());
    }

    void WavesAssignment()
    {
        int wavesNum = Random.Range(10, 20);
        waves=new Waves[wavesNum];

        for (int i = 0; i < waves.Length; i++)
        {
            GameObject obj = enemies[Random.Range(0, enemies.Length)];
            int enemyCount = Random.Range(1, 10);
            float interval = Random.Range(0.5f, 2f);
            waves[i] = new Waves(obj, enemyCount, interval);
        }
    }


    private IEnumerator SpawerEnemy()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            for (int j = 0; j < waves[i].EnemyCount; j++)
            {
                GameObject temp = Instantiate(waves[i].EnemyPrefab, transform.position, Quaternion.identity, transform);
                EnemyLiveCount++;
                yield return new WaitForSeconds(waves[i].interval);
            }
            while (EnemyLiveCount > 0)
            {
                yield return 0;
            }

            if (i == waves.Length - 1)
            {
                GameManager.Instance.allWaveOver++;
                if (GameManager.Instance.allWaveOver >= 3)
                {
                    GameManager.Instance.ShowGameWinPanel();
                }
            }

            if (GameManager.Instance.gameOver) yield return 0;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
