using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    #region 换肤颜色
    public Color guiMei;
    public Color cangLong;
    public Color ziDian;
    public Color lvLuo;
    public Color ziYe;
    public Color siSheng;

    [HideInInspector] public Color currentColor;
    #endregion
    public GameObject man;
    public GameObject woman;
    public GameObject[] particles;
    public Button[] changeClothesBtns;

    private GameObject changeClothesBtnGameObject;
    private GameObject pinchFaceBtnGameObject;

    private GameObject changeClothes_EmptyGameObject;

    private Animator changeClothes_EmptyAnimator;
    private Animator changeCharacterBtn_EmptyAnimator;
    private Animator pinchFace_HolderAnimator;

    private SkinnedMeshRenderer skinnedMeshRenderer_Man;
    private SkinnedMeshRenderer[] skinnedMeshRenderers_Woman=new SkinnedMeshRenderer[3];

    private Slider bigEar_Slider;
    private Slider smallEar_Slider;
    private Slider bigHead_Slider;

    private GameObject reminderTxt_GameObject;
    private Text gunnerTipTxt;

    private bool isMan = true;

    void Start()
    {
        Init();
        OnSliderValueChangeAddListener();
    }

    void Init()
    {
        #region 获取游戏物体
        changeClothesBtnGameObject = transform.Find("System_Holder/ChangeClothes_Btn").gameObject;
        pinchFaceBtnGameObject = transform.Find("System_Holder/PinchFace_Btn").gameObject;
        changeClothes_EmptyGameObject = transform.Find("ChangeClothes_Empty").gameObject;
        reminderTxt_GameObject = GameObject.Find("Reminder_Txt");
        gunnerTipTxt = transform.Find("Gunner_TipTxt").GetComponent<Text>();
        gunnerTipTxt.enabled = false;
        #endregion

        HideReminderTxt();

        #region 获取动画组件
        changeCharacterBtn_EmptyAnimator = GameObject.Find("ChangeCharacter_Empty").GetComponent<Animator>();
        pinchFace_HolderAnimator = GameObject.Find("PinchFace_Holder").GetComponent<Animator>();
        changeClothes_EmptyAnimator = changeClothes_EmptyGameObject.GetComponent<Animator>();
        #endregion

        #region 获取骨骼
        skinnedMeshRenderer_Man = GameObject.Find("Man/char_ethan_body").GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers_Woman[0] = woman.transform.Find("hero/model01/hero 1/hero_boot_lf")
            .GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers_Woman[1] = woman.transform.Find("hero/model01/hero 1/hero_boot_rt")
            .GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers_Woman[2] = woman.transform.Find("hero/model01/hero 1/hero_pants")
            .GetComponent<SkinnedMeshRenderer>();
        #endregion

        #region 获取捏脸滑动条
        bigEar_Slider = GameObject.Find("BigEar_Slider").GetComponent<Slider>();
        smallEar_Slider = GameObject.Find("SmallEar_Slider").GetComponent<Slider>();
        bigHead_Slider = GameObject.Find("BigHead_Slider").GetComponent<Slider>();
        #endregion

    }

    /// <summary>
    /// 捏脸滑动条
    /// </summary>
    void OnSliderValueChangeAddListener()
    {
        bigEar_Slider.onValueChanged.AddListener(value => CharacterCustomization.Instance.ChangeBlendShapeValue("Ear_Max", value));
        smallEar_Slider.onValueChanged.AddListener(value => CharacterCustomization.Instance.ChangeBlendShapeValue("Ear_Min", value));
        bigHead_Slider.onValueChanged.AddListener(value => CharacterCustomization.Instance.ChangeBlendShapeValue("Head_Max", value));
    }

    /// <summary>
    /// 显示换装系统和捏脸系统按钮
    /// </summary>
    public void ShowChangeClothesAndPinchFaceBtn()
    {
        changeClothesBtnGameObject.SetActive(true);
        pinchFaceBtnGameObject.SetActive(true);
    }

    void HideChangeClothesAndPinchFaceBtn()
    {
        changeClothesBtnGameObject.SetActive(false);
        pinchFaceBtnGameObject.SetActive(false);
    }

    /// <summary>
    /// 换装
    /// </summary>
    void ChangeClothesBackgroundAnim(bool isPlay)
    {
        changeClothes_EmptyAnimator.SetBool("Open", isPlay);
    }

    /// <summary>
    /// 选择人物按钮
    /// </summary>
    public void SelectCharacterBtnAnim(bool isPlay)
    {
        changeCharacterBtn_EmptyAnimator.SetBool("Open", isPlay);
    }

    /// <summary>
    /// 捏脸
    /// </summary>
    void PinchFaceSystemAnim(bool isPlay)
    {
        if (isMan)
        {
            pinchFace_HolderAnimator.SetBool("Open", isPlay);
        }
        else
        {
            ShowReminderTxt();
        }
    }

    /// <summary>
    /// 点击换装系统
    /// </summary>
    public void OnChangeClothesBtnClick()
    {
        HideChangeClothesAndPinchFaceBtn();

        SelectCharacterBtnAnim(false);
        Invoke("HideSelectCharacterBtn", 1);

        ChangeClothesBackgroundAnim(true);
    }

    /// <summary>
    /// 点击换装确定
    /// </summary>
    public void OnChangeClothesConfirmBtnClick()
    {
        ChangeClothesBackgroundAnim(false);
        Invoke("HideChangeClothesBackground", 1.5f);
        Invoke("ShowChangeClothesAndPinchFaceBtn", 1f);
        SelectCharacterBtnAnim(true);
    }

    /// <summary>
    /// 点击捏脸系统
    /// </summary>
    public void OnPinchFaceBtnClick()
    {
        HideChangeClothesAndPinchFaceBtn();
        SelectCharacterBtnAnim(false);
        PinchFaceSystemAnim(true);
    }

    public void OnPinchFaceConfirmBtnClick()
    {
        PinchFaceSystemAnim(false);
        SelectCharacterBtnAnim(true);
        Invoke("ShowChangeClothesAndPinchFaceBtn", 1);
    }

    #region 换肤

    public void OnGuiMeiBtnClick()
    {
        Instantiate(particles[0]);
        StartCoroutine(SetSkinnedMeshRendererColor(guiMei));
        
    }

    public void OnCangLongBtnClick()
    {
        Instantiate(particles[1]);
        StartCoroutine(SetSkinnedMeshRendererColor(cangLong));
        
    }

    public void OnZiDianBtnClick()
    {
        Instantiate(particles[2]);
        StartCoroutine(SetSkinnedMeshRendererColor(ziDian));
        
    }

    public void OnLvLuoBtnClick()
    {
        Instantiate(particles[3]);
        StartCoroutine(SetSkinnedMeshRendererColor(lvLuo));
        
    }

    public void OnZiYeBtnClick()
    {
        Instantiate(particles[4]);
        StartCoroutine(SetSkinnedMeshRendererColor(ziYe));
        
    }

    public void OnSiShenBtnClick()
    {
        Instantiate(particles[5]);
        StartCoroutine(SetSkinnedMeshRendererColor(siSheng));
        
    }

    IEnumerator SetSkinnedMeshRendererColor(Color c)
    {
        InactiveChangeClothesBtn();
        yield return new WaitForSeconds(2);
        if (isMan)
        {
            if(skinnedMeshRenderer_Man.materials[0].color==c) yield break;
            currentColor = c;
            skinnedMeshRenderer_Man.materials[0].color = c;
            skinnedMeshRenderer_Man.materials[1].color = c;
        }
        else
        {
            if (skinnedMeshRenderers_Woman[0].material.color == c) yield break;
            for (int i = 0; i < skinnedMeshRenderers_Woman.Length; i++)
            {
                skinnedMeshRenderers_Woman[i].material.color = c;
            }
        }
        ActiveChangeClothesBtn();

    }

    void InactiveChangeClothesBtn()
    {
        for (int i = 0; i < changeClothesBtns.Length; i++)
        {
            changeClothesBtns[i].interactable = false;
        }
    }

    void ActiveChangeClothesBtn()
    {
        for (int i = 0; i < changeClothesBtns.Length; i++)
        {
            changeClothesBtns[i].interactable = true;
        }
    }

    #endregion

    #region 选择人物
    public void OnManBtnClick()
    {
        if (man.activeSelf) return;
        isMan = true;
        man.SetActive(true);
        woman.SetActive(false);
    }

    public void OnWomanBtnClick()
    {
        if (woman.activeSelf) return;
        isMan = false;
        man.SetActive(false);
        woman.SetActive(true);
    }
    #endregion

    #region 枪手捏脸提示文字
    public void HideReminderTxt()
    {
        reminderTxt_GameObject.SetActive(false);
    }

    public void ShowReminderTxt()
    {
        reminderTxt_GameObject.SetActive(true);
    }
    #endregion

    public void OnChosenBtnClick()
    {
        if (isMan)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            gunnerTipTxt.enabled = true;
            Invoke("HideGunnerTipTxt", 2);
        }

    }

    void HideGunnerTipTxt()
    {
        gunnerTipTxt.enabled = false;
    }
}
