using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int destroySelfTime = 2;

    public AudioClip hurtClip;
    public AudioClip dieClip;
    public AudioClip attackClip;

    private Animator anim;
    private AudioSource audioSource;
    private bool isDie = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die")) return;
        hp -= Random.Range(damage - 10, damage + 10);
        GameObject damageText = GameManager.Instance.ShowDamage(hp);
        damageText.transform.position = transform.position + new Vector3(0, 2, 0);
        audioSource.clip = hurtClip;
        audioSource.Play();
        if (hp <= 0)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die")) return;
            InitEnemySpawn.Instance.SpawnOneEnemy();
            hp = 0;
            audioSource.clip = dieClip;
            audioSource.Play();
            GetComponent<Rigidbody>().isKinematic = true;
            anim.SetTrigger("Die");
            TowerManager.Instance.currentMoney += 150;
            TowerManager.Instance.SetCoinText();
            Destroy(gameObject, destroySelfTime);
        }
    }

    public void AnotherTakeDamage(int damage)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die")) return;
        hp -= Random.Range(damage - 1, damage + 1);
        GameObject damageText = GameManager.Instance.ShowDamage(hp);
        damageText.transform.position = transform.position + new Vector3(0, 2, 0);
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hurtClip;
            audioSource.Play();
        }
        if (hp <= 0)
        {
            if (isDie) return;
            InitEnemySpawn.Instance.SpawnOneEnemy();
            hp = 0;
            if (!audioSource.isPlaying)
            {
                audioSource.clip = dieClip;
                audioSource.Play();
            }
            GetComponent<Rigidbody>().isKinematic = true;
            anim.SetTrigger("Die");
            TowerManager.Instance.currentMoney += 150;
            TowerManager.Instance.SetCoinText();
            Destroy(gameObject, destroySelfTime);
            isDie = true;
        }
    }

    public void PlayAttackClip()
    {
        audioSource.clip = attackClip;
        audioSource.Play();
    }
}
