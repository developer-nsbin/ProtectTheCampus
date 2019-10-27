using UnityEngine;
using UnityEngine.UI;

public class MyItemUI : MonoBehaviour 
{
    public MyItem MyItem { get; set; }
    public int Amount { get; set; }
    public Image MyItemImage
    {
        get
        {
            if (myItemImage == null)
                myItemImage = GetComponent<Image>();
            return myItemImage;
        }
    }
    public Text AmountTxt
    {
        get
        {
            if (amountTxt == null)
                amountTxt = GetComponentInChildren<Text>();
            return amountTxt;
        }
    }

    private float targetScale = 1;
    private Vector3 animationScale = new Vector3(1.3f, 1.3f, 1.3f);
    private float smoothing = 4;

    #region 属性
    private Image myItemImage;
    private Text amountTxt;
    #endregion

    #region 进入动画
    void Update()
    {
        //初初放入背包时的动画
        if (transform.localScale.x != targetScale)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < .2f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }
    #endregion

    public void SetMyItemSpriteAndAmount(MyItem myItem, int amount = 1)
    {
        transform.localScale = animationScale;
        MyItem = myItem;
        Amount = amount;
        MyItemImage.sprite = Resources.Load<Sprite>(myItem.Sprite);
        if (MyItem.Capacity > 1)
        {
            AmountTxt.text = Amount.ToString();
        }
        else
        {
            AmountTxt.text = "";
        }
    }

    public void SetMyItemAmount(int amount)
    {
        transform.localScale = animationScale;
        Amount = amount;
        if (MyItem.Capacity > 1)
        {
            AmountTxt.text = Amount.ToString();
        }
        else
        {
            AmountTxt.text = "";
        }
    }

    public void AddMyItemAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        Amount += amount;
        if (MyItem.Capacity > 1)
        {
            AmountTxt.text = Amount.ToString();
        }
        else
        {
            AmountTxt.text = "";
        }
    }

    public void ReduceMyItemAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        Amount -= amount;
        if (MyItem.Capacity > 1)
        {
            AmountTxt.text = Amount.ToString();
        }
        else
        {
            AmountTxt.text = "";
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void Exchange(MyItemUI anotherItemUI)
    {
        MyItem myItemTemp = anotherItemUI.MyItem;
        int myItemAmountTemp = anotherItemUI.Amount;
        anotherItemUI.SetMyItemSpriteAndAmount(MyItem, Amount);
        SetMyItemSpriteAndAmount(myItemTemp, myItemAmountTemp);
    }
}
