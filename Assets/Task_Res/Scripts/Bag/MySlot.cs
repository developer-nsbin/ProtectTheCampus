using UnityEngine;
using UnityEngine.EventSystems;

public class MySlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject myItemPrefab;

    public void StoreMyItem(MyItem myItem)
    {
        if (transform.childCount == 0)
        {
            GameObject myItemPrefab = Instantiate(this.myItemPrefab);
            myItemPrefab.transform.SetParent(transform);
            myItemPrefab.transform.localScale = Vector3.one;
            myItemPrefab.transform.localPosition = Vector3.zero;
            myItemPrefab.GetComponent<MyItemUI>().SetMyItemSpriteAndAmount(myItem);
        }
        else
        {
            transform.GetChild(0).GetComponent<MyItemUI>().AddMyItemAmount();
        }
    }

    public ItemType GetMyItemType()
    {
        return transform.GetChild(0).GetComponent<MyItemUI>().MyItem.Type;
    }

    public int GetMyItemID()
    {
        return transform.GetChild(0).GetComponent<MyItemUI>().MyItem.ID;
    }

    public bool IsFilled()
    {
        MyItemUI myItemUI = transform.GetChild(0).GetComponent<MyItemUI>();
        return myItemUI.Amount >= myItemUI.MyItem.Capacity;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string myItemMsgStr = transform.GetChild(0).GetComponent<MyItemUI>().MyItem.GetToopTipText();
            MyItemManager.Instance.ShowTip(myItemMsgStr);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            MyItemManager.Instance.HideTip();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (transform.childCount > 0)
        {
            MyItemUI currentMyItemUI = transform.GetChild(0).GetComponent<MyItemUI>();
            if (!MyItemManager.Instance.IsPickedItem)
            {
                MyItemManager.Instance.PickupItem(currentMyItemUI.MyItem, currentMyItemUI.Amount);
                Destroy(currentMyItemUI.gameObject);
            }
            else
            {
                if (currentMyItemUI.MyItem.ID == MyItemManager.Instance.PickedItem.MyItem.ID)
                {
                    if (currentMyItemUI.MyItem.Capacity > currentMyItemUI.Amount)
                    {
                        int amountRemain = currentMyItemUI.MyItem.Capacity - currentMyItemUI.Amount;//当前物品槽的容量减去当前物品槽的物品数量求得当前物品槽的剩余空间
                        if (amountRemain >= MyItemManager.Instance.PickedItem.Amount)//如果剩余空间大于鼠标上所选物品的数量（全部放下）
                        {
                            currentMyItemUI.SetMyItemAmount(
                                currentMyItemUI.Amount + MyItemManager.Instance.PickedItem.Amount);
                            MyItemManager.Instance.RemoveMyItem(MyItemManager.Instance.PickedItem.Amount);
                        }
                        else
                        {
                            currentMyItemUI.SetMyItemAmount(currentMyItemUI.Amount + amountRemain);//当前物品槽的容量加上剩余容量（即已经满了）
                            MyItemManager.Instance.RemoveMyItem(amountRemain);//移除鼠标上能放进物品槽中的最大数量，剩多少还放在鼠标上
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MyItem myItem = currentMyItemUI.MyItem;
                    int amount = currentMyItemUI.Amount;
                    currentMyItemUI.SetMyItemSpriteAndAmount(MyItemManager.Instance.PickedItem.MyItem,
                        MyItemManager.Instance.PickedItem.Amount);
                    MyItemManager.Instance.PickedItem.SetMyItemSpriteAndAmount(myItem, amount);
                }
            }
        }
        else
        {
            if (MyItemManager.Instance.IsPickedItem)
            {
                for (int i = 0; i < MyItemManager.Instance.PickedItem.Amount; i++)
                {
                    StoreMyItem(MyItemManager.Instance.PickedItem.MyItem);
                }

                

                MyItemManager.Instance.RemoveMyItem(MyItemManager.Instance.PickedItem.Amount);
            }
            else
            {
                return;
            }
        }
    }
}
