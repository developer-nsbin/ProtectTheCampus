using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnim : MonoBehaviour
{
    public float hp;
    public Slider hpSlider;
    public float uniqueSkill3Offset;
    public GameObject[] UniqueSkillEffects;
    public float plusHPSmoothing;
    public SkillSlot uniqueSkill1SkillSlot;
    public SkillSlot uniqueSkill2SkillSlot;
    public SkillSlot uniqueSkill3SkillSlot;

    [HideInInspector] public Animator anim;
    private bool doTurn = false;
    private bool doRun = false;
    [HideInInspector] public bool doBaseSkill1 = false;
    [HideInInspector] public bool doBaseSkill2 = false;
    [HideInInspector] public bool doSkill1 = false;
    [HideInInspector] public bool doSkill2 = false;
    [HideInInspector] public bool doSkill3 = false;
    private float targetTurn = 0;
    private float targetRun = 0;
    private SkinnedMeshRenderer skinnedMeshRenderer_Man;
    private TestMyTrail testMyTrail;
    private float totalHp;

    public float uniqueSkill1Timer = 0;
    private bool skill1CanUse = true;
    public float uniqueSkill2Timer = 0;
    private bool skill2CanUse = true;
    public float uniqueSkill3Timer = 0;
    private bool skill3CanUse = true;

    void Awake()
    {
        anim = GetComponent<Animator>();
        testMyTrail = GetComponent<TestMyTrail>();
        totalHp = hp;
        Init();
    }

    void Init()
    {
        skinnedMeshRenderer_Man = GameObject.Find("Man/char_ethan_body").GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer_Man.materials[0].color = UIManager.Instance.currentColor;
        skinnedMeshRenderer_Man.materials[1].color = UIManager.Instance.currentColor;
    }

    void Update()
    {
        if (hp <= 0) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        #region 技能
        anim.SetBool("Turn", doTurn);
        anim.SetBool("Run", doRun);
        anim.SetFloat("TurnDirection", targetTurn);
        anim.SetFloat("RunDirection", targetRun);

        doBaseSkill1 = Input.GetKeyUp(KeyCode.J) ? true : false;
        if (doBaseSkill1) testMyTrail.heroIdle();
        anim.SetBool("BaseSkill1", doBaseSkill1);

        doBaseSkill2 = Input.GetKeyUp(KeyCode.K) ? true : false;
        if (doBaseSkill2) testMyTrail.heroIdle();
        anim.SetBool("BaseSkill2", doBaseSkill2);

        doSkill1 = Input.GetKeyUp(KeyCode.L) ? true : false;
        if (doSkill1) testMyTrail.heroIdle();
        anim.SetBool("Skill1", doSkill1);

        doSkill2 = Input.GetKeyUp(KeyCode.I) ? true : false;
        if (doSkill2) testMyTrail.heroIdle();
        anim.SetBool("Skill2", doSkill2);

        doSkill3 = Input.GetKeyUp(KeyCode.O) ? true : false;
        if (doSkill3) testMyTrail.heroIdle();
        anim.SetBool("Skill3", doSkill3);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (skill1CanUse)
            {
                uniqueSkill1SkillSlot.OnSkillBtnClick();
                OnUnique_Skill1BtnClick();
                skill1CanUse = false; 
            }
        }

        if (!skill1CanUse)
        {
            uniqueSkill1Timer += Time.deltaTime*0.25f;
            if (uniqueSkill1Timer >= 5)
            {
                skill1CanUse = true;
                uniqueSkill1Timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (skill2CanUse)
            {
                uniqueSkill2SkillSlot.OnSkillBtnClick();
                OnUnique_Skill2BtnClick();
                skill2CanUse = false;
            }
        }

        if (!skill2CanUse)
        {
            uniqueSkill2Timer += Time.deltaTime * 0.25f;
            if (uniqueSkill2Timer >= 5)
            {
                skill2CanUse = true;
                uniqueSkill2Timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (skill3CanUse)
            {
                uniqueSkill3SkillSlot.OnSkillBtnClick();
                OnUnique_Skill3BtnClick();
                skill3CanUse = false;
            }
        }

        if (!skill3CanUse)
        {
            uniqueSkill3Timer += Time.deltaTime * 0.25f;
            if (uniqueSkill3Timer >= 5)
            {
                skill3CanUse = true;
                uniqueSkill3Timer = 0;
            }
        }

        #endregion

        #region 移动旋转
        if (h != 0) doTurn = true;
        else doTurn = false;

        if (doTurn) targetTurn = h * 97;

        if (v != 0)
        {
            doRun = true;
            if (doTurn)
            {
                targetRun = h * 4;
            }
        }
        else
        {
            doRun = false;
        }
        #endregion

        if (hp <= 100)
        {
            hp += Time.deltaTime * plusHPSmoothing;
            hpSlider.value = hp / totalHp;
            if (hp >= 100)
            {
                hp = 100;
            }
        }
    }

    public void OnUnique_Skill1BtnClick()
    {
        Instantiate(UniqueSkillEffects[0], transform.position, transform.rotation);
        anim.SetTrigger("UniqueSkill1");
    }

    public void OnUnique_Skill2BtnClick()
    {
        Instantiate(UniqueSkillEffects[1], transform.position, transform.rotation);
        anim.SetTrigger("UniqueSkill2");
    }

    public void OnUnique_Skill3BtnClick() 
    {
        Instantiate(UniqueSkillEffects[2], transform.position+transform.forward*uniqueSkill3Offset, transform.rotation);
        anim.SetTrigger("UniqueSkill3");
    }

    public void TakeDamage(int damage)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die")) return;
        hp -= damage;
        GameObject damageText = GameManager.Instance.ShowPlayerDamage((int)hp);
        damageText.transform.position = transform.position + new Vector3(0, 2, 0);
        hpSlider.value = hp / totalHp;
        GetComponent<CharacterSound>().PlayDamageClip();
        if (hp <= 0)
        {
            hp = 0;
            anim.SetTrigger("Die");
            GameManager.Instance.gameOver = true;
            GameManager.Instance.ShowGameOverPanel();
        }
    }
}
