using UnityEngine;
using UnityEngine.UI;

public class FinalEnemy : MonoBehaviour
{
    public float hp;
    public Slider hpSlider;

    private float totalHp;

    void Start()
    {
        totalHp = hp;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpSlider.value = hp / totalHp;
        if (hp <= 0)
        {
            GetComponentInParent<FinalEnemySpawner>().EnemyLiveCount--;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
