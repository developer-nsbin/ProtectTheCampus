using System;
using UnityEngine;
using UnityEngine.UI;

public class MyForgePanel : MonoBehaviour
{
    private MyItemUI myItemUI;
    private Text speedTxt;
    private Text powerTxt;
    private Text defenceTxt;
    private Text hpTxt;

    private int speedBaseProperty = 10;
    private int defenceBaseProperty = 10;
    private int powerBaseProperty = 10;
    private int hpBaseProperty = 10;

    void Awake()
    {
        speedTxt = GameObject.Find("Speed_Txt/Txt").GetComponent<Text>();
        powerTxt = GameObject.Find("Power_Txt/Txt").GetComponent<Text>();
        defenceTxt = GameObject.Find("Defence_Txt/Txt").GetComponent<Text>();
        hpTxt = GameObject.Find("Hp_Txt/Txt").GetComponent<Text>();
    }

    public void OnConfirmBtnClick()
    {
        if (transform.GetChild(0).GetChild(0).childCount > 0)
        {
            MyItemUI myItemUI = GetComponentInChildren<MyItemUI>();
            MyItem myItem =myItemUI.MyItem;
            string myItemName = myItem.Sprite;
            string adjustStr = myItemName.Remove(0, 7);
            string[] neededStr = adjustStr.Split('+');
            string typeStr = neededStr[0];
            string propertyValueStr = neededStr[1];
            int propertyValue = Convert.ToInt32(propertyValueStr);
            switch (typeStr)
            {
                case "Defence":
                    defenceBaseProperty += propertyValue;
                    defenceTxt.text = defenceBaseProperty.ToString();
                    break;
                case "Speed":
                    speedBaseProperty += propertyValue;
                    speedTxt.text = speedBaseProperty.ToString();
                    break;
                case "Attack":
                    powerBaseProperty += propertyValue;
                    powerTxt.text = powerBaseProperty.ToString();
                    break;
                case "HP":
                    hpBaseProperty += propertyValue;
                    hpTxt.text = hpBaseProperty.ToString();
                    break;
            }

            Destroy(myItemUI.gameObject);
        }
    }
}
