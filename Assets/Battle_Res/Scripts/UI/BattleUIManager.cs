using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : Singleton<BattleUIManager>
{
    public Sprite openSoundSprite;
    public Sprite closeSoundSprite;
    public Texture2D[] cursorSprites;//0—正常，1—敌人

    private GameObject set_Panel;
    private GameObject exit_Panel;
    private GameObject maxMap_Panel;
    private Image soundImage;
    private bool isOpenSound = true;
    private AudioSource audioSource;

    void Start()
    {
        set_Panel = transform.Find("Set_Panel").gameObject;
        exit_Panel = transform.Find("Exit_Panel").gameObject;
        maxMap_Panel = transform.Find("MaxMap").gameObject;
        soundImage = set_Panel.transform.Find("Btn_Audio").GetComponent<Image>();
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
    }

    #region 小地图
    public void OnMinMapBtnClick()
    {
        maxMap_Panel.SetActive(true);
    }

    public void OnMaxMapBtnClick()
    {
        maxMap_Panel.SetActive(false);
    }
    #endregion

    #region 设置面板
    public void OnSetBtnClick()
    {
        set_Panel.SetActive(true);
    }

    public void OnSoundSpriteBtnClick()
    {
        set_Panel.SetActive(isOpenSound);
        isOpenSound = !isOpenSound;
        soundImage.sprite = isOpenSound ? openSoundSprite : closeSoundSprite;
        audioSource.enabled = isOpenSound;
        set_Panel.SetActive(false);
    }
    #endregion

    #region 离开面板
    public void OnExitBtnClick()
    {
        exit_Panel.SetActive(true);
    }

    public void OnExitConfirmBtnClick()
    {
        
    }

    public void OnExitCancelBtnClick()
    {
        exit_Panel.SetActive(false);
    }
    #endregion

    #region 光标
    public void SetNormalCursor()
    {
        Cursor.SetCursor(cursorSprites[0], Vector2.zero, CursorMode.Auto);
    }

    public void SetEnemyCursor()
    {
        Cursor.SetCursor(cursorSprites[1], Vector2.zero, CursorMode.Auto);
    }
    #endregion

    public void ExitGame()
    {
        Application.Quit();
    }
}
