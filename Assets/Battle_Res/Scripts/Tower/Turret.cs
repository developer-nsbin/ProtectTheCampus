using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float attackRate;
    public Transform firePos;
    public GameObject bullet;
    public Transform head;
    public bool isLaser = false;
    private LineRenderer laserRenderer;
    private AudioSource audioSource;

    private float timer;
    private float laserDamage = 5;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (isLaser)
        {
            laserRenderer = GetComponent<LineRenderer>();
        }
        timer = attackRate;
    }

    void Update()
    {
        if (GameManager.Instance.gameOver) return;
        timer += Time.deltaTime;
        //旋转
        if (enemies.Count > 0 && enemies[0] != null)
        {
            Vector3 tempPos = enemies[0].transform.position;
            tempPos.y = head.position.y;
            head.LookAt(tempPos);
        }

        if (!isLaser)
        {
            if (enemies.Count > 0)
            {
                if (timer >= attackRate && enemies.Count > 0)
                {
                    timer = 0;
                    Attack();
                }
            }
        }
        else
        {
            if (enemies.Count > 0)
            {
                if (laserRenderer.enabled == false) laserRenderer.enabled = true;

                if (enemies[0] == null)
                {
                    UpdateEnemies();
                }

                if (enemies.Count > 0)
                {
                    if(!audioSource.isPlaying)audioSource.Play();
                    laserRenderer.SetPositions(new Vector3[] { firePos.position, enemies[0].transform.position });
                    enemies[0].GetComponent<FinalEnemy>().TakeDamage(laserDamage * Time.deltaTime);
                }
            }
            else
            {
                laserRenderer.enabled = false;
            }
        }
    }

    void Attack()
    {
        if (GameManager.Instance.gameOver) return;
        if (enemies[0] == null) UpdateEnemies();
        if (enemies.Count > 0)
        {
            if (!audioSource.isPlaying) audioSource.Play();
            GameObject temp = Instantiate(bullet, firePos.position, firePos.rotation, firePos);
            temp.GetComponent<Bullet>().SearchTarget(enemies[0].transform);
        }
        else
        {
            timer = attackRate;
        }
    }

    void UpdateEnemies()
    {
        List<int> emptyList = new List<int>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) emptyList.Add(i);
        }

        for (int i = 0; i < emptyList.Count; i++)
        {
            enemies.RemoveAt(emptyList[i] - i);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost")
        {
            enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ghost")
        {
            enemies.Remove(other.gameObject);
        }
    }

    //void OnMouseDown()
    //{
    //    GetComponentInParent<MapCube>().
    //}
}
