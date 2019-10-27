using UnityEngine;

public class MyThirdPersonCamera : MonoBehaviour
{

    public float distanceAway;          // distance from the back of the craft
    public float distanceUp;            // distance above the craft
    public float smooth;                // how smooth the camera movement is

    private GameObject hovercraft;      // to store the hovercraft
    private Vector3 targetPosition;     // the position the camera is trying to be in

    Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        // setting the target position to be the correct offset from the hovercraft
        targetPosition = player.position + Vector3.up * distanceUp - player.forward * distanceAway;

        // making a smooth transition between it's current position and the position it wants to be in
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        // make sure the camera is looking the right way!
        transform.LookAt(player);
    }

    #region 在一定范围内不移动

    //public float smoothing ;
    //public float minCriticalDistance;
    //public float maxCriticalDistance;

    //private Transform player;
    //private Vector3 offset;
    //private bool isBeyondMaxDistance = false;
    //public bool isBeyondMinDistance = false;
    //private float originalDistance;
    //private Vector3 velocity;

    //void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player").transform;
    //    offset = transform.position - player.position;
    //    originalDistance = Vector3.Distance(transform.position, player.position);
    //}

    //void LateUpdate()
    //{


    //}

    //void InRangeDontMove()
    //{
    //    float distance = Vector3.Distance(transform.position, player.position);
    //    //Debug.Log(distance);
    //    if (distance > maxCriticalDistance || isBeyondMaxDistance)
    //    {
    //        isBeyondMaxDistance = true;
    //        if (isBeyondMaxDistance)
    //        {
    //            transform.position =
    //                Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, smoothing);
    //            if (Vector3.Distance(transform.position, player.position) - originalDistance < 0.5f)
    //            {
    //                isBeyondMaxDistance = false;
    //            }
    //        }
    //    }
    //    else if (distance < minCriticalDistance || isBeyondMinDistance)
    //    {
    //        isBeyondMinDistance = true;
    //        if (isBeyondMinDistance)
    //        {
    //            transform.position =
    //                Vector3.Lerp(transform.position, player.position + offset, smoothing * Time.deltaTime * 2);
    //            if (originalDistance - Vector3.Distance(transform.position, player.position) < 0.5f)
    //            {
    //                isBeyondMinDistance = false;
    //            }
    //        }
    //    }
    //}
    #endregion
}
