using UnityEngine;

public class CameraMoveToMan : MonoBehaviour
{
    public float speed;

    private Vector3 needToMovePos;
    private Vector3 orignalPos;
    private bool isMoveToTarget = false;

    void Awake()
    {
        orignalPos = transform.position;
        needToMovePos = new Vector3(-0.72f, 1.6769f, 1.38f);
    }

    void Update()
    {
        if (isMoveToTarget)
        {
            if (Vector3.Distance(transform.position, needToMovePos) > 0.001f)
            {
                transform.position = Vector3.Lerp(transform.position, needToMovePos, Time.deltaTime * speed);
            }
            else
            {
                transform.position = needToMovePos;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, orignalPos) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, orignalPos, Time.deltaTime * speed);
            }
            else
            {
                transform.position = orignalPos;
            }
        }
    }

    public void OnPinchFachBtnClick()
    {
        isMoveToTarget = true;
    }

    public void OnPinchFachBtnConfirmClick()
    {
        isMoveToTarget = false;
    }

    public void OnReminderBtnClick()
    {
        isMoveToTarget = false;
        UIManager.Instance.HideReminderTxt();
        UIManager.Instance.ShowChangeClothesAndPinchFaceBtn();
        UIManager.Instance.SelectCharacterBtnAnim(true);
    }
}
