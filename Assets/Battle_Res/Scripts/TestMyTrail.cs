using UnityEngine;

public class TestMyTrail : MonoBehaviour 
{
    public WeaponTrail baseSkill_Trail1;
    public WeaponTrail baseSkill_Trail2;
    public WeaponTrail Skill_Trail1;
    public WeaponTrail Skill_Trail2;
    public WeaponTrail Skill_Trail3;


    private WeaponTrail currentWeaponTrail;
    private float t = 0.033f;
    private float tempT = 0;
    private float animationIncrement = 0.003f;
    private CharacterAnim characterAnim;

    void Awake()
    {
        currentWeaponTrail = baseSkill_Trail1;
        characterAnim = GetComponent<CharacterAnim>();
    }

    void LateUpdate()
    {
        if (characterAnim.doBaseSkill1) currentWeaponTrail = baseSkill_Trail1;
        else if (characterAnim.doBaseSkill2) currentWeaponTrail = baseSkill_Trail2;
        else if (characterAnim.doSkill1) currentWeaponTrail = Skill_Trail1;
        else if (characterAnim.doSkill2) currentWeaponTrail = Skill_Trail2;
        else if (characterAnim.doSkill3) currentWeaponTrail = Skill_Trail3;

        t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);

        if (t > 0)
        {
            while (tempT < t)
            {
                tempT += animationIncrement;

                if (currentWeaponTrail.time > 0)
                {
                    currentWeaponTrail.Itterate(Time.time - t + tempT);
                }
                else
                {
                    currentWeaponTrail.ClearTrail();
                }
            }

            tempT -= t;

            if (currentWeaponTrail.time > 0)
            {
                currentWeaponTrail.UpdateTrail(Time.time, t);
            }
        }
    }

    void Start()
    {
        // 默认没有拖尾效果
        currentWeaponTrail.SetTime(0.0f, 0.0f, 1.0f);
    }

    public void heroAttack()
    {
        //设置拖尾时长
        currentWeaponTrail.SetTime(2.0f, 0.0f, 1.0f);
        //开始进行拖尾
        currentWeaponTrail.StartTrail(0.5f, 0.4f);
    }

    public void heroIdle()
    {
        //清除拖尾
        currentWeaponTrail.ClearTrail();
    }
}
