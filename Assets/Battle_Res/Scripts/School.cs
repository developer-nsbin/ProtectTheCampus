using UnityEngine;
using UnityEngine.UI;

public class School : MonoBehaviour
{
    private int hp=100;
    private float totalHP;
    public Slider schoolSlider;

    void Start()
    {
        totalHP = hp;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost")
        {
            other.GetComponent<FinalEnemy>().TakeDamage(100);
            TakeDamage(Random.Range(3,5));
        }
    }

    void TakeDamage(int damage)
    {
        hp -= damage;
        schoolSlider.value = hp / totalHP;
        if (hp <= 0)
        {
            GameManager.Instance.ShowGameOverPanel();
        }
    }
}
