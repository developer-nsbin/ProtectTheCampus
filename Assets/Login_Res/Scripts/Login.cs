using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 登录
/// </summary>
public class Login : MonoBehaviour
{
    private InputField userNameInputField;      //用户名输入框
    private InputField userPasswardInputField;  //用户密码输入框
    private Text loginTipTxt;                   //登录提示文字

    //在Awake方法中获取相关游戏组件
    void Awake()
    {
        userNameInputField = transform.Find("Login_Background/Account_Txt/InputField").GetComponent<InputField>();
        userPasswardInputField = transform.Find("Login_Background/Passward_Txt/InputField").GetComponent<InputField>();
        loginTipTxt = transform.Find("Login_TipTxt").GetComponent<Text>();
    }

    //登录按钮的注册事件
    public void OnLoginBtnClick()
    {
        //如果没有输入，则提示输入
        if (userNameInputField.text == "" &&
            userPasswardInputField.text == "")
        {
            CancelInvoke(); //取消重复调用
            loginTipTxt.gameObject.SetActive(true);
            loginTipTxt.text = "请输入正确的账号和密码！";
            Invoke("HideLoginTxt", 2);
            return;
        }

        //
        if(userNameInputField.text == PlayerPrefs.GetString("UserName", "protectthecampus") &&
                userPasswardInputField.text == PlayerPrefs.GetString("UserPassward", "bingo"))
        {
            CancelInvoke();
            loginTipTxt.gameObject.SetActive(true);
            loginTipTxt.text = "登录成功";
            AudioManager.Instance.bgAudioSource.enabled = false;
            AudioManager.Instance.btnAudioSource.enabled = false;
            Invoke("HideLoginTxt", 2);
            Invoke("LoadCharacterScene", 2.5f);
        }
        else
        {
            CancelInvoke();
            loginTipTxt.gameObject.SetActive(true);
            loginTipTxt.text = "账号或密码暂未注册！";
            Invoke("HideLoginTxt", 2);
        }
    }

    public void OnRegisterBtnClick()
    {
        if (userNameInputField.text != "" && userPasswardInputField.text!="")
        {
            PlayerPrefs.SetString("UserName", userNameInputField.text);
            PlayerPrefs.SetString("UserPassward", userPasswardInputField.text);

            CancelInvoke();
            loginTipTxt.gameObject.SetActive(true);
            loginTipTxt.text = "注册成功！";
            Invoke("HideLoginTxt", 2);
        }
        else
        {
            CancelInvoke();
            loginTipTxt.gameObject.SetActive(true);
            loginTipTxt.text = "请输入正确的账号和密码！";
            Invoke("HideLoginTxt", 2);
        }

    }

    void HideLoginTxt()
    {
        loginTipTxt.gameObject.SetActive(false);
    }

    void LoadCharacterScene()
    {
        SceneManager.LoadScene(1);
    }
}
