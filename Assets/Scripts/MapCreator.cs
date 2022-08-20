using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图制造器：测量出来的地图长宽为19*23
/// 坦克在地图中间最下方坐标为(0,-9)，地图中间最上方为（0,9），地图中间最左边为(-11,0)，最右边为（11,0）
/// </summary>
public class MapCreator : MonoBehaviour
{
    //用于存放初始化地图所需游戏对象的数组：Heart，墙，障碍，玩家出生效果，敌人出生效果，河流，草，空气墙(记住存放顺序，方便从数组中取出对应游戏对象)
    public GameObject[] gameItems;
    //用于存储已经实例化对象的位置，之后随机生成游戏对象时就要排除掉这些位置
    private List<Vector3> itemPositonList = new List<Vector3>();

    /// <summary>
    /// 最开始执行的方法Awake里面：初始化地图，实例化游戏对象
    /// </summary>
    private void Awake()
    {
        InitMap();
    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitMap()
    {
        //Heart实例化位置(0,-8.5),Quaternion.identity表示无旋转
        CreateInMapCreator(gameItems[0], new Vector3(0, -9, 0), Quaternion.identity);
        //用墙围住Heart
        CreateHeartProtetionWall();

        //玩家和敌人生成位置需要在地图其他物品都实例化前先实例化，玩家在Heart左右生成，敌人在最上面一行生成
        CreatePlayer1Born();
        CreateEnemiesBorn();

        //地图其他位置的墙（不是保护Heart的墙）
        CreateOtherWall();
        CreateBarrier();
        CreateRiver();
        CreateGrass();
        CreateAirBarrier();
    }

    /// <summary>
    /// 创建墙保护Heart
    /// </summary>
    private void CreateHeartProtetionWall()
    {
        CreateInMapCreator(gameItems[1], new Vector3(0, -8, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[1], new Vector3(-1, -9, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[1], new Vector3(1, -9, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[1], new Vector3(1, -8, 0), Quaternion.identity);
    }

    /// <summary>
    /// 创建玩家出生地点
    /// </summary>
    private void CreatePlayer1Born()
    {
        CreateInMapCreator(gameItems[3], new Vector3(-2, -9, 0), Quaternion.identity);
    }

    /// <summary>
    /// 创建敌人出生地点
    /// 游戏开始运行先创建3个敌人出生位置，立即产生三个敌人，然后每隔一段时间在三个出生位置随机生成一个敌人
    /// </summary>
    private void CreateEnemiesBorn()
    {
        CreateInMapCreator(gameItems[4], new Vector3(-11, 9, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[4], new Vector3(0, 9, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[4], new Vector3(11, 9, 0), Quaternion.identity);

        //重复调用方法：第一个参数为重复调用的方法名，第二个参数为几秒之后第一次调用，第三个参数为每隔多少时间调用一次
        //5秒之后调用CreateRandomEnemy方法，然后每隔10s调用一次
        InvokeRepeating("CreateRandomEnemy", 5, 10);
    }

    /// <summary>
    /// 在三个敌人出生位置随机生成一名敌人
    /// </summary>
    private void CreateRandomEnemy()
    {
        int random = Random.Range(0, 3);
        Vector3 enemyPosition = new Vector3();
        if (random == 0)
        {
            enemyPosition = new Vector3(0, 9, 0);
        }
        else if (random == 1)
        {
            enemyPosition = new Vector3(-11, 9, 0);
        }
        else
        {
            enemyPosition = new Vector3(11, 9, 0);
        }
        Instantiate(gameItems[4], enemyPosition, Quaternion.identity);
    }


    /// <summary>
    /// 创建其他墙（不是用来保护Heart的墙）
    /// </summary>
    private void CreateOtherWall()
    {
        for(int i = 0; i < 100; i++)
        {
            CreateInMapCreator(gameItems[1], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    /// <summary>
    /// 创建障碍物
    /// </summary>
    private void CreateBarrier()
    {
        for (int i = 0; i < 40; i++)
        {
            CreateInMapCreator(gameItems[2], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    /// <summary>
    /// 创建河流
    /// </summary>
    private void CreateRiver()
    {
        for (int i = 0; i < 40; i++)
        {
            CreateInMapCreator(gameItems[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    /// <summary>
    /// 创建草
    /// </summary>
    private void CreateGrass()
    {
        for (int i = 0; i < 40; i++)
        {
            CreateInMapCreator(gameItems[6], CreateRandomPosition(), Quaternion.identity);
        }
    }

    /// <summary>
    /// 创建空气墙方法
    /// </summary>
    private void CreateAirBarrier()
    {
        //实例化上下空气墙
        for (int x = -12; x <= 12; x++)
        {
            CreateInMapCreator(gameItems[7], new Vector3(x, -10, 0), Quaternion.identity);
            CreateInMapCreator(gameItems[7], new Vector3(x, 10, 0), Quaternion.identity);
        }
        //实例化左右空气墙
        for (int y = -10; y <= 10; y++)
        {
            CreateInMapCreator(gameItems[7], new Vector3(-12, y, 0), Quaternion.identity);
            CreateInMapCreator(gameItems[7], new Vector3(12, y, 0), Quaternion.identity);
        }
    }

    



    /// <summary>
    /// 将游戏对象的实例化放在MapCreator下，保持游戏运行时Hierarchy面板整洁
    /// </summary>
    /// <param name="createGameObject">要实例化的游戏对象</param>
    /// <param name="createPosition">实例化游戏对象的位置</param>
    /// <param name="createQuaternion">实例化游戏对象的角度</param>
    private void CreateInMapCreator(GameObject createGameObject,Vector3 createPosition,Quaternion createQuaternion)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createQuaternion);
        //将游戏对象父物体设置为挂有当前脚本的游戏对象（MapCreator）
        itemGo.transform.SetParent(gameObject.transform);
        //将实例化游戏对象的地址保存到itemPositonList，在随机生成游戏对象的方法中需要排除这些已经生成游戏对象的位置
        itemPositonList.Add(createPosition);
    }

    /// <summary>
    /// 产生随机位置的方法，用于随机实例化游戏对象
    /// </summary>
    private Vector3 CreateRandomPosition()
    {
        //以防万一有条横跨屏幕的Barrier障碍物实例化，那游戏会进行不下去，所以我们可以设置在竖直方向的最左右两边和水平方向最上边和最下边不实例化游戏对象，也就是这两条位置不参与“产生随机位置”
        //不生成X=11和X=-11的两列，不生成Y=-9和Y=9的两行
        while (true)
        {
            Vector3 randomPotition = new Vector3(Random.Range(-10, 10), Random.Range(-8, 8),0);
            //判定随机产生的位置是否与itemPosition内保存的位置有相同的，还有的话继续执行，没有的话表明这个随机生成的位置尚未实例化对象，可以进行实例化对象，return跳出循环，将可用的随机位置返回给调用者
            if (!HasThePosition(randomPotition))
            {
                return randomPotition;
            }
        }
    }

    /// <summary>
    /// 判断随机产生的位置是否包含在itemPositonList里面
    /// </summary>
    /// <param name="randomPosition">需要进行判断的位置</param>
    /// <returns>返回true表示产生的随机位置已经存在于itemPositonList，表明这个位置是不可用的</returns>
    private bool HasThePosition(Vector3 randomPosition)
    {
        //遍历列表，将列表内的元素与随机产生的位置进行比较
        for(int i = 0; i < itemPositonList.Count; i++)
        {
            if(randomPosition == itemPositonList[i])
            {
                return true;
            }
        }
        return false;
    }
}
