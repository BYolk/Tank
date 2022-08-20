using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ���״̬����
/// </summary>
public class PlayerManager : MonoBehaviour
{
    //����ֵ:������������
    public int lifeCount = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    //����:�õ�����������ڸ���;��ȡUI��������Ҫ����UI�������ռ�using UnityEngine.UI;
    public GameObject bornPlayer1Prefab;
    public Text playerScoreText;
    public Text playerLifeCountText;
    public GameObject gamgOverImg;

    //����
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
    /// ��Awake����ʵ��������
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// ʱ�̼������״̬
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
    /// ��Ҹ����
    /// </summary>
    private void Recover()
    {
        if(lifeCount <= 0)
        {
            //��Ϸʧ�ܣ�����������
            isDefeat = true;
            Invoke("ReturnToMenu", 3);//��Ϸʧ��3s����ת���˵�����
        }
        else
        {
            lifeCount--;
            Instantiate(bornPlayer1Prefab, new Vector3(-2, -9, 0), Quaternion.identity);
            isDead = false;
        }
    }

    /// <summary>
    /// ��ת��������
    /// </summary>
    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
