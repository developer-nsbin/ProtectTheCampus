using UnityEngine;

public class FinalEnemyMoving : MonoBehaviour 
{
    public float speed = 2f;

    private Transform[] wayPoints;
    private int index = 0;
    private Vector3 currentVelocity = Vector3.one;

    void Awake()
    {
        wayPoints = GetComponentInParent<FinalEnemySpawner>().wayPoints;
    }
     
    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (GameManager.Instance.gameOver) return;
        if (index <= wayPoints.Length - 1)
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, wayPoints[index].position, ref currentVelocity, speed);

            transform.LookAt(wayPoints[index]);

            if (Vector3.Distance(wayPoints[index].position, transform.position) <= 1f)
            {
                index++;
            }

            if (index > wayPoints.Length - 1)
            {
                ReachDestination();
            }
        }

    }

    void ReachDestination()
    {
        GetComponentInParent<FinalEnemySpawner>().EnemyLiveCount--;
        Destroy(gameObject);
    }
}
