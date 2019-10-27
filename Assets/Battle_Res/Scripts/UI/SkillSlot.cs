using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : Singleton<SkillSlot>
{
    public float smoothing = 0.5f;
    public bool canClick = false;

    private Image maskImg;
    private Button skillBtn;
    private float timer = 1;
    private bool countDown = false;

    void Start()
    {
        maskImg = transform.Find("Mask").GetComponent<Image>();
        skillBtn = transform.GetComponentInChildren<Button>();
        skillBtn.onClick.AddListener(OnSkillBtnClick);
    }

    void Update()
    {
        if (canClick)
        {
            maskImg.raycastTarget = false;
            maskImg.fillAmount = 0;
            canClick = false;
        }

        if (countDown)
        {
            timer -= Time.deltaTime*smoothing;
            maskImg.fillAmount = timer;
            if (timer <= 0)
            {
                maskImg.raycastTarget = false;
                timer = 1;
                countDown = false;
            }

        }
    }

    public void OnSkillBtnClick()
    {
        maskImg.raycastTarget = true;
        maskImg.fillAmount = 1;
        countDown = true;
    }
}
