using UnityEngine;
using UnityEngine.UI;

public class MyItemMsg : MonoBehaviour
{
    public float smoothing = 5;

    private Text myItemMsgTxt;
    private CanvasGroup canvasGroup;
    private float targetAlpha = 0;

    void Awake()
    {
        myItemMsgTxt = GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    public void Shop(string text)
    {
        myItemMsgTxt.text = text;
        targetAlpha = 1;
    }

    public void Hide()
    {
        targetAlpha = 0;
    }

    public void SetLocalPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }
}
