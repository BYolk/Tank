using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ�����������������ĵ�ͼ����Ϊ19*23
/// ̹���ڵ�ͼ�м����·�����Ϊ(0,-9)����ͼ�м����Ϸ�Ϊ��0,9������ͼ�м������Ϊ(-11,0)�����ұ�Ϊ��11,0��
/// </summary>
public class MapCreator : MonoBehaviour
{
    //���ڴ�ų�ʼ����ͼ������Ϸ��������飺Heart��ǽ���ϰ�����ҳ���Ч�������˳���Ч�����������ݣ�����ǽ(��ס���˳�򣬷����������ȡ����Ӧ��Ϸ����)
    public GameObject[] gameItems;
    //���ڴ洢�Ѿ�ʵ���������λ�ã�֮�����������Ϸ����ʱ��Ҫ�ų�����Щλ��
    private List<Vector3> itemPositonList = new List<Vector3>();

    /// <summary>
    /// �ʼִ�еķ���Awake���棺��ʼ����ͼ��ʵ������Ϸ����
    /// </summary>
    private void Awake()
    {
        InitMap();
    }

    /// <summary>
    /// ��ʼ����ͼ
    /// </summary>
    private void InitMap()
    {
        //Heartʵ����λ��(0,-8.5),Quaternion.identity��ʾ����ת
        CreateInMapCreator(gameItems[0], new Vector3(0, -9, 0), Quaternion.identity);
        //��ǽΧסHeart
        CreateHeartProtetionWall();

        //��Һ͵�������λ����Ҫ�ڵ�ͼ������Ʒ��ʵ����ǰ��ʵ�����������Heart�������ɣ�������������һ������
        CreatePlayer1Born();
        CreateEnemiesBorn();

        //��ͼ����λ�õ�ǽ�����Ǳ���Heart��ǽ��
        CreateOtherWall();
        CreateBarrier();
        CreateRiver();
        CreateGrass();
        CreateAirBarrier();
    }

    /// <summary>
    /// ����ǽ����Heart
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
    /// ������ҳ����ص�
    /// </summary>
    private void CreatePlayer1Born()
    {
        CreateInMapCreator(gameItems[3], new Vector3(-2, -9, 0), Quaternion.identity);
    }

    /// <summary>
    /// �������˳����ص�
    /// ��Ϸ��ʼ�����ȴ���3�����˳���λ�ã����������������ˣ�Ȼ��ÿ��һ��ʱ������������λ���������һ������
    /// </summary>
    private void CreateEnemiesBorn()
    {
        CreateInMapCreator(gameItems[4], new Vector3(-11, 9, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[4], new Vector3(0, 9, 0), Quaternion.identity);
        CreateInMapCreator(gameItems[4], new Vector3(11, 9, 0), Quaternion.identity);

        //�ظ����÷�������һ������Ϊ�ظ����õķ��������ڶ�������Ϊ����֮���һ�ε��ã�����������Ϊÿ������ʱ�����һ��
        //5��֮�����CreateRandomEnemy������Ȼ��ÿ��10s����һ��
        InvokeRepeating("CreateRandomEnemy", 5, 10);
    }

    /// <summary>
    /// ���������˳���λ���������һ������
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
    /// ��������ǽ��������������Heart��ǽ��
    /// </summary>
    private void CreateOtherWall()
    {
        for(int i = 0; i < 100; i++)
        {
            CreateInMapCreator(gameItems[1], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    /// <summary>
    /// �����ϰ���
    /// </summary>
    private void CreateBarrier()
    {
        for (int i = 0; i < 40; i++)
        {
            CreateInMapCreator(gameItems[2], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    /// <summary>
    /// ��������
    /// </summary>
    private void CreateRiver()
    {
        for (int i = 0; i < 40; i++)
        {
            CreateInMapCreator(gameItems[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    /// <summary>
    /// ������
    /// </summary>
    private void CreateGrass()
    {
        for (int i = 0; i < 40; i++)
        {
            CreateInMapCreator(gameItems[6], CreateRandomPosition(), Quaternion.identity);
        }
    }

    /// <summary>
    /// ��������ǽ����
    /// </summary>
    private void CreateAirBarrier()
    {
        //ʵ�������¿���ǽ
        for (int x = -12; x <= 12; x++)
        {
            CreateInMapCreator(gameItems[7], new Vector3(x, -10, 0), Quaternion.identity);
            CreateInMapCreator(gameItems[7], new Vector3(x, 10, 0), Quaternion.identity);
        }
        //ʵ�������ҿ���ǽ
        for (int y = -10; y <= 10; y++)
        {
            CreateInMapCreator(gameItems[7], new Vector3(-12, y, 0), Quaternion.identity);
            CreateInMapCreator(gameItems[7], new Vector3(12, y, 0), Quaternion.identity);
        }
    }

    



    /// <summary>
    /// ����Ϸ�����ʵ��������MapCreator�£�������Ϸ����ʱHierarchy�������
    /// </summary>
    /// <param name="createGameObject">Ҫʵ��������Ϸ����</param>
    /// <param name="createPosition">ʵ������Ϸ�����λ��</param>
    /// <param name="createQuaternion">ʵ������Ϸ����ĽǶ�</param>
    private void CreateInMapCreator(GameObject createGameObject,Vector3 createPosition,Quaternion createQuaternion)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createQuaternion);
        //����Ϸ������������Ϊ���е�ǰ�ű�����Ϸ����MapCreator��
        itemGo.transform.SetParent(gameObject.transform);
        //��ʵ������Ϸ����ĵ�ַ���浽itemPositonList�������������Ϸ����ķ�������Ҫ�ų���Щ�Ѿ�������Ϸ�����λ��
        itemPositonList.Add(createPosition);
    }

    /// <summary>
    /// �������λ�õķ������������ʵ������Ϸ����
    /// </summary>
    private Vector3 CreateRandomPosition()
    {
        //�Է���һ���������Ļ��Barrier�ϰ���ʵ����������Ϸ����в���ȥ���������ǿ�����������ֱ��������������ߺ�ˮƽ�������ϱߺ����±߲�ʵ������Ϸ����Ҳ����������λ�ò����롰�������λ�á�
        //������X=11��X=-11�����У�������Y=-9��Y=9������
        while (true)
        {
            Vector3 randomPotition = new Vector3(Random.Range(-10, 10), Random.Range(-8, 8),0);
            //�ж����������λ���Ƿ���itemPosition�ڱ����λ������ͬ�ģ����еĻ�����ִ�У�û�еĻ��������������ɵ�λ����δʵ�������󣬿��Խ���ʵ��������return����ѭ���������õ����λ�÷��ظ�������
            if (!HasThePosition(randomPotition))
            {
                return randomPotition;
            }
        }
    }

    /// <summary>
    /// �ж����������λ���Ƿ������itemPositonList����
    /// </summary>
    /// <param name="randomPosition">��Ҫ�����жϵ�λ��</param>
    /// <returns>����true��ʾ���������λ���Ѿ�������itemPositonList���������λ���ǲ����õ�</returns>
    private bool HasThePosition(Vector3 randomPosition)
    {
        //�����б����б��ڵ�Ԫ�������������λ�ý��бȽ�
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
