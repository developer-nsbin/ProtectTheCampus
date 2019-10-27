using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject damagePrefab;
    public GameObject playerDamagePrefab;
    public bool gameOver = false;
    public Text sceneTipTxt;
    [HideInInspector]public int allWaveOver = 0;
    public GameObject finalEnemySpawnPoint;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    private int waitTime = 250;
    private int familiarTime = 50;

    void Start()
    {
        InvokeRepeating("CountDown", 1, 1);
    }

    void CountDown()
    {
        familiarTime -= 1;
        sceneTipTxt.text = "你有" + familiarTime + "秒熟悉场景噢！";
        waitTime -= 1;

        if (familiarTime <= 0)
        {
            sceneTipTxt.text = "你有" + waitTime + "秒打倒怪物，赚取金钱噢！";
        }

        if (waitTime <= 0)
        {
            finalEnemySpawnPoint.SetActive(true);
            sceneTipTxt.text = "";
            CancelInvoke("CountDown");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(3);
    }

    public void ReturnSelectCharacterScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowGameOverPanel()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void ShowGameWinPanel()
    {
        gameOver = true;
        gameWinPanel.SetActive(true);
    }

    public GameObject ShowDamage(int damage)
    {
        GameObject hud = Instantiate(damagePrefab) as GameObject;
        hud.GetComponentInChildren<Text>().text = "-" + damage.ToString();
        return hud;
    }

    public GameObject ShowPlayerDamage(int damage)
    {
        GameObject hud = Instantiate(playerDamagePrefab) as GameObject;
        hud.GetComponentInChildren<Text>().text = "-" + damage.ToString();
        return hud;
    }
}
