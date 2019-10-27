using UnityEngine;

/// <summary>
/// 设置屏幕宽高
/// </summary>
public class SetScreenScale : MonoBehaviour
{
    public int width;   //设置屏幕的高度
    public int height;  //设置屏幕的宽度

    //当脚本实例被加载时会调用Awake函数
    void Awake()
    {
        Screen.SetResolution(width, height, false); //布尔值：设置是否全屏显示
    }
}
