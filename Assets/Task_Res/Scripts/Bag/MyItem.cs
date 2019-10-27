using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    Weapon,
}

/// <summary>
/// 物品的基础属性
/// </summary>
public class MyItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public string Sprite { get; set; }

    public MyItem(int id, string name, ItemType type, string description, int capacity, int buyPrice, string sprite)
    {
        ID = id;
        Name = name;
        Type = type;
        Description = description;
        Capacity = capacity;
        BuyPrice = buyPrice;
        Sprite = sprite;
    }

    public virtual string GetToopTipText()
    {
        string color = "";
        switch (Type)
        {
            case ItemType.Consumable:
                color = "magenta";
                break;
            case ItemType.Equipment:
                color = "lime";//绿黄色
                break;
            case ItemType.Weapon :
                color = "navy";//深蓝色
                break;
        }

        string text = string.Format("<color={4}>{0}</color>\n\n" +
                                    "<size=22><color=while>购买价格：" +"{1} \n " +
                                    "类型：{2}</color></size>\n" +
                                    "<color=red><size=22>{3}</size></color>"
            , Name, BuyPrice, Type, Description, color);
        return text;
    }
}
