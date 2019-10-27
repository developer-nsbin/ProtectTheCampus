using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    /// <summary>
    /// 滚动速度
    /// </summary>
    private float speed = 1.5f;

    /// <summary>
    /// 计时器
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 销毁时间
    /// </summary>
    private float time = 0.8f;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        Scroll();
    }

    /// <summary>
    /// 冒泡效果
    /// </summary>
    private void Scroll()
    {
        //字体滚动
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        //字体缩小
        this.GetComponentInChildren<Text>().fontSize--;
        //字体渐变透明
        Destroy(gameObject, time);
    }


}