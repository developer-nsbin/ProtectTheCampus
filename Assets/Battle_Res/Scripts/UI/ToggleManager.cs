using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : Singleton<ToggleManager>
{
    public Toggle[] towerToggle;
    public Toggle[] pulldownTowerToggle;

    void Start()
    {
        OnToggleBtnClick();
    }

    public void OnToggleBtnClick()
    {
        for (int i = 0; i < towerToggle.Length; i++)
        {
            if (towerToggle[i].isOn)
            {
                towerToggle[i].transform.Find("Mask").gameObject.SetActive(false);
                pulldownTowerToggle[i].transform.parent.transform.Find("Mask").gameObject.SetActive(false);
            }
            else
            {
                towerToggle[i].transform.Find("Mask").gameObject.SetActive(true);
                pulldownTowerToggle[i].transform.parent.transform.Find("Mask").gameObject.SetActive(true);
            }
        }
    }
}
