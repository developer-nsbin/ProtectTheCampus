using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Transform baseSkill1_AttackPos;
    public Transform baseSkill2_AttackPos;
    public Transform skill1_AttackPos;
    public Transform skill2_AttackPos;
    public Transform skill3_AttackPos;
    public Transform uniqueSkill3_AttackPos;

    public float baseSkill1_AttackRadio;
    public float baseSkill2_AttackRadio;
    public float skill1_AttackRadio;
    public float skill2_AttackRadio;
    public float skill3_AttackRadio;
    public float finalHitSkill1_AttackRadio;
    public float finalHitSkill2_AttackRadio;
    public float finalHitSkill3_AttackRadio;

    private Animator anim;
    private bool firstTimeDetection = false;
    private BoxCollider boxCollider;
     
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base_Skill1") && !firstTimeDetection)
        {
            firstTimeDetection = true;
            Collider[] enemyColliders = Physics.OverlapSphere(baseSkill1_AttackPos.position, baseSkill1_AttackRadio,LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().TakeDamage(19);
                }
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base_Skill4") && !firstTimeDetection)
        {
            firstTimeDetection = true;
            Collider[] enemyColliders = Physics.OverlapSphere(baseSkill2_AttackPos.position, baseSkill2_AttackRadio, LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().TakeDamage(21);
                }
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill1") && !firstTimeDetection)
        {
            firstTimeDetection = true;
            Collider[] enemyColliders = Physics.OverlapSphere(skill1_AttackPos.position, skill1_AttackRadio, LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().TakeDamage(35);
                }
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill2") && !firstTimeDetection)
        {
            firstTimeDetection = true;
            Collider[] enemyColliders = Physics.OverlapSphere(skill2_AttackPos.position, skill2_AttackRadio, LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().TakeDamage(37);
                }
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill3") && !firstTimeDetection)
        {
            firstTimeDetection = true;
            Collider[] enemyColliders = Physics.OverlapSphere(skill3_AttackPos.position, skill3_AttackRadio, LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().TakeDamage(39);
                }
            }
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Unique_Skill1") )
        {
            Collider[] enemyColliders = Physics.OverlapSphere(transform.position, finalHitSkill1_AttackRadio, LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().AnotherTakeDamage(2);
                }
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Unique_Skill2") || anim.GetCurrentAnimatorStateInfo(0).IsName("Unique_Skill22"))
        {
            Collider[] enemyColliders = Physics.OverlapSphere(transform.position, finalHitSkill2_AttackRadio, LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().AnotherTakeDamage(2);
                }
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Unique_Skill3") || anim.GetCurrentAnimatorStateInfo(0).IsName("Unique_Skill33"))
        {
            Collider[] enemyColliders = Physics.OverlapSphere(uniqueSkill3_AttackPos.position, finalHitSkill3_AttackRadio, LayerMask.GetMask("Enemy"));
            if (enemyColliders.Length > 0)
            {
                foreach (Collider collider in enemyColliders)
                {
                    collider.GetComponent<Enemy>().AnotherTakeDamage(2);
                }
            }
        }
    }

    void ChangeFirstTimeDetection()
    {
        firstTimeDetection = false;
    }
}
