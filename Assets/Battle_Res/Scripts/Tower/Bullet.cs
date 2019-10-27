using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;

    private Transform target;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        if (target = null)
        {
            Destroy(gameObject);
        }

        transform.LookAt(target);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost")
        {
            other.GetComponent<FinalEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SearchTarget(Transform trans)
    {
        target = trans;
    }
}
