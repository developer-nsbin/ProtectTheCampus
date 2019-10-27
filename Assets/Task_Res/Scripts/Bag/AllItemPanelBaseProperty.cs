using UnityEngine;

public class AllItemPanelBaseProperty : MonoBehaviour
{
    public MySlot[] slotList;

    protected virtual void Start()
    {
        slotList = GetComponentsInChildren<MySlot>();
    }

    public bool StoreMyItem(int id)
    {
        MyItem myItem = MyItemManager.Instance.GetMyItemByID(id);
        return StoreMyItem(myItem);
    }

    public bool StoreMyItem(MyItem myItem)
    {
        if (myItem == null)
        {
            print("要存储的物品的id不存在！");
            return false;
        }

        if (myItem.Capacity == 1)
        {
            MySlot mySlot = FindEmptySlot();
            if(mySlot == null) Debug.LogWarning("没有空的物品槽了！");
            else
            {
                mySlot.StoreMyItem(myItem);
            }
        }
        else
        {
            MySlot mySlot = FindSameIDSlot(myItem);
            if (mySlot != null) mySlot.StoreMyItem(myItem);
            else
            {
                MySlot emptySlot = FindEmptySlot();
                if (emptySlot != null) emptySlot.StoreMyItem(myItem);
                else
                {
                    Debug.LogError("没有空的物品槽了!");
                    return false;
                }
            }
        }

        return true;
    }

    MySlot FindEmptySlot()
    {
        foreach (MySlot mySlot in slotList)
        {
            if (mySlot.transform.childCount == 0) return mySlot;
        }

        return null;
    }

    MySlot FindSameIDSlot(MyItem myItem)
    {
        foreach (MySlot mySlot in slotList)
        {
            if (mySlot.transform.childCount >= 1 && mySlot.GetMyItemID() == myItem.ID && !mySlot.IsFilled())
            {
                return mySlot;
            }
        }

        return null;
    }
}
