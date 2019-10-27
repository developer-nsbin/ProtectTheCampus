using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskUIManager : Singleton<TaskUIManager>
{
    private Animator announcementAnimator;
    private Animator taskPanelAnimator;
    private Animator propertyPanelAnimator;
    private Animator knapsackPanelAnimator;
    private Animator shopPanelAnimator;
    private Animator forgePanelAnimator;
    private bool havedPanel = false;

    void Start()
    {
        announcementAnimator = GameObject.Find("Announcement_Panel").GetComponent<Animator>();
        taskPanelAnimator = GameObject.Find("Task_Panel").GetComponent<Animator>();
        propertyPanelAnimator = GameObject.Find("Property_Panel").GetComponent<Animator>();
        knapsackPanelAnimator = GameObject.Find("Knapsack_Panel").GetComponent<Animator>();
        shopPanelAnimator = GameObject.Find("Shop_Panel").GetComponent<Animator>();
        forgePanelAnimator = GameObject.Find("Forge_Panel").GetComponent<Animator>();
    }

    #region 公告

    void AnnouncementPanelAnim(bool isOpen)
    {
        announcementAnimator.SetBool("Open", isOpen);
    }

    public void OnAnnouncementBtnClick()
    {
        if (havedPanel) return;
        havedPanel = true;
        AnnouncementPanelAnim(true);
    }

    public void OnAnnouncementConfirmBtnClick()
    {
        havedPanel = false;
        AnnouncementPanelAnim(false);
    }

    #endregion

    #region 任务

    void TaskPanelAnim(bool isOpen)
    {
        taskPanelAnimator.SetBool("Open", isOpen);
    }

    public void OnTaskNPCClick()
    {
        if (havedPanel) return;
        havedPanel = true;
        TaskPanelAnim(true);
    }

    public void OnTaskNPCConfirmBtnClick()
    {
        havedPanel = false;
        TaskPanelAnim(false);
    }
    #endregion

    #region 属性

    void PropertyPanelAnim(bool isOpen)
    {
        propertyPanelAnimator.SetBool("Open", isOpen);
    }

    public void OnHeadBtnClick()
    {
        if (havedPanel) return;
        havedPanel = true;
        PropertyPanelAnim(true);
    }

    public void OnHeadConfirmBtnClick()
    {
        havedPanel = false;
        PropertyPanelAnim(false);
    }

    #endregion

    #region 背包
    void KnapsackPanelAnim(bool isOpen)
    {
        knapsackPanelAnimator.SetBool("Open", isOpen);
    }

    public void OnKnapsackBtnClick()
    {
        if (havedPanel) return;
        havedPanel = true;
        KnapsackPanelAnim(true);
    }

    public void OnKnapsackConfirmBtnClick()
    {
        havedPanel = false;
        KnapsackPanelAnim(false);
    }
    #endregion

    #region 商城
    void ShopPanelAnim(bool isOpen)
    {
        shopPanelAnimator.SetBool("Open", isOpen);
    }

    public void OnShopBtnClick()
    {
        if (havedPanel) return;
        havedPanel = true;
        ShopPanelAnim(true);
        KnapsackPanelAnim(true);
    }

    public void OnShopConfirmBtnClick()
    {
        havedPanel = false;
        ShopPanelAnim(false);
        KnapsackPanelAnim(false);
    }
    #endregion

    #region 锻造
    void ForgePanelAnim(bool isOpen)
    {
        forgePanelAnimator.SetBool("Open", isOpen);
        KnapsackPanelAnim(true);
    }

    public void OnForgeBtnClick()
    {
        if (havedPanel) return;
        havedPanel = true;
        ForgePanelAnim(true);
    }

    public void OnForgeConfirmBtnClick()
    {
        havedPanel = false;
        ForgePanelAnim(false);
        KnapsackPanelAnim(false);
    }
    #endregion

    public void EnterBattleScene()
    {
        SceneManager.LoadScene(3);
    }

}
