using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品管理
/// </summary>
public class MyItemManager : MonoBehaviour
{
    private static MyItemManager _instance;
    public static MyItemManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("MyItemManager").GetComponent<MyItemManager>();

            return _instance;
        }
    }

    public MyKnapsack myKnapsack;

    public List<MyItem> myItemList;

    public MyItemUI PickedItem
    {
        get
        {
            return pickedItem;
        }
        set { pickedItem = value; }
    }
    public bool IsPickedItem
    {
        get
        {
            return isPickedItem;
        }
        set { isPickedItem = value; }
    }

    private Canvas canvas;
    private MyItemMsg myItemMsg;
    private Vector2 myItemMsgPosOffset = new Vector2(100, -120);
    private bool isShowMyItemMsg = false;

    #region 属性
    private MyItemUI pickedItem;
    private bool isPickedItem = false;
    #endregion

    void Awake()
    {
        ParseMyItemJson();
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        PickedItem = GameObject.Find("PickedItem").GetComponent<MyItemUI>();
        myKnapsack = GameObject.Find("Knapsack_Panel").GetComponent<MyKnapsack>();
        myItemMsg = GameObject.Find("MyItemMsg").GetComponent<MyItemMsg>();
        PickedItem.Hide();
    }

    void Update()
    {
        //拖动物体时的操作
        if (IsPickedItem)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out pos);
            PickedItem.SetLocalPosition(pos);
        }
        else if (isShowMyItemMsg)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out pos);
            myItemMsg.SetLocalPos(pos + myItemMsgPosOffset);
        }

        if (IsPickedItem && Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(-1) == false)
        {
            IsPickedItem = false;
            PickedItem.Hide();
        }
    }

    void ParseMyItemJson()
    {
        myItemList = new List<MyItem>();
        MyItem myItem = null;
        TextAsset myItemText = Resources.Load<TextAsset>("MyItemInformation");
        string myItemStr = myItemText.text;
        JSONObject jsonObject = new JSONObject(myItemStr);
        foreach (JSONObject obj in jsonObject.list)
        {
            int id = (int)obj["id"].n;
            string name = obj["name"].str;
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), obj["type"].str);
            string description = obj["description"].str;
            int capacity = (int)obj["capacity"].n;
            int buyPrice = (int) obj["buyPrice"].n;
            string sprite = obj["sprite"].str;

            myItem = new MyItem(id, name, type, description, capacity, buyPrice, sprite);
            myItemList.Add(myItem);
        }
    }

    public MyItem GetMyItemByID(int id)
    {
        foreach (MyItem myItem in myItemList)
        {
            if (myItem.ID == id)
            {
                return myItem;
            }
        }

        Debug.LogError("根据ID获取物品错误,错误ID:" + id);
        return null;
    }

    public void PickupItem(MyItem myItem, int amount)
    {
        PickedItem.SetMyItemSpriteAndAmount(myItem, amount);
        IsPickedItem = true;
        PickedItem.Show();
        myItemMsg.Hide();
    }

    public void RemoveMyItem(int amount = 1)
    {
        PickedItem.ReduceMyItemAmount(amount);
        if (PickedItem.Amount <= 0)
        {
            IsPickedItem = false;
            PickedItem.Hide();
        }
    }

    public void ShowTip(string text)
    {
        if (isPickedItem) return;
        isShowMyItemMsg = true;
        myItemMsg.Shop(text);
    }

    public void HideTip()
    {
        isShowMyItemMsg = false;
        myItemMsg.Hide();
    }
}
