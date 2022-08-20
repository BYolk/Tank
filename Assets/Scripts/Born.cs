using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家出生行为脚本
/// </summary>
public class Born : MonoBehaviour
{
    //属性
    //区分产生玩家和产生敌人
    public bool isBornPlayer;

    //引用
    //获取玩家预制体引用
    public GameObject playerPrefab;
    //获取敌人预制体引用
    public GameObject[] enemyPrefabs;
    private void Start()
    {
        //延迟调用BornTank方法：等待播放特效播放完毕
        Invoke("BornTank", 1);
        //延迟销毁
        Destroy(gameObject, 1);
    }
    private void BornTank()
    {
        if (isBornPlayer)
        {
            //实例化玩家对象，旋转方向不旋转
            //Born特效后期会放置在出生点，transform.position即表示在出生点位置出生
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            //随机实例化敌人:random.range方法的两个参数是前闭后开，即包括第一个参数，但小于但不等于第二个参数
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabs[num], transform.position, Quaternion.identity);
        }
        
    }
}
