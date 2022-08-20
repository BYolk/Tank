using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 玩家状态管理
/// </summary>
public class PlayerManager : MonoBehaviour
{
    //属性值:玩家生命与分数
    public int lifeCount = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    //引用:拿到玩家引用用于复活;获取UI的引用需要引入UI的命名空间using UnityEngine.UI;
    public GameObject bornPlayer1Prefab;
    public Text playerScoreText;
    public Text playerLifeCountText;
    public GameObject gamgOverImg;

    //单例
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    /// <summary>
    /// 在Awake方法实例化单例
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 时刻监听玩家状态
    /// </summary>
    private void Update()
    {
        if (isDead)
        {
            Recover();
        }
        if (isDefeat)
        {
            gamgOverImg.SetActive(true);
        }
        playerScoreText.text = playerScore.ToString();
        playerLifeCountText.text = lifeCount.ToString();
    }

    /// <summary>
    /// 玩家复活方法
    /// </summary>
    private void Recover()
    {
        if(lifeCount <= 0)
        {
            //游戏失败，返回主界面
            isDefeat = true;
            Invoke("ReturnToMenu", 3);//游戏失败3s后跳转到菜单界面
        }
        else
        {
            lifeCount--;
            Instantiate(bornPlayer1Prefab, new Vector3(-2, -9, 0), Quaternion.identity);
            isDead = false;
        }
    }

    /// <summary>
    /// 跳转到主界面
    /// </summary>
    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
