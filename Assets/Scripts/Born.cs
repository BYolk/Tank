using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҳ�����Ϊ�ű�
/// </summary>
public class Born : MonoBehaviour
{
    //����
    //���ֲ�����ҺͲ�������
    public bool isBornPlayer;

    //����
    //��ȡ���Ԥ��������
    public GameObject playerPrefab;
    //��ȡ����Ԥ��������
    public GameObject[] enemyPrefabs;
    private void Start()
    {
        //�ӳٵ���BornTank�������ȴ�������Ч�������
        Invoke("BornTank", 1);
        //�ӳ�����
        Destroy(gameObject, 1);
    }
    private void BornTank()
    {
        if (isBornPlayer)
        {
            //ʵ������Ҷ�����ת������ת
            //Born��Ч���ڻ�����ڳ����㣬transform.position����ʾ�ڳ�����λ�ó���
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            //���ʵ��������:random.range����������������ǰ�պ󿪣���������һ����������С�ڵ������ڵڶ�������
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabs[num], transform.position, Quaternion.identity);
        }
        
    }
}
