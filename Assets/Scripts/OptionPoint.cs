using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始界面的“选项指针”脚本
/// 开始界面是第0个场景，开始游戏是第1个场景
/// </summary>
public class OptionPoint : MonoBehaviour
{
    //玩家选项
    private int choice = 1;
    public Transform onePlayerPosition;
    public Transform twoPlayerPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            choice = 1;
            transform.position = onePlayerPosition.position;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            choice = 2;
            transform.position = twoPlayerPosition.position;
        }
        //如果choice为1并且玩家按下空格，那么切换场景（场景的切换需要引入场景管理的命名空间：using UnityEngine.SceneManagement;）
        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
