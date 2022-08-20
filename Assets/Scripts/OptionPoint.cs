using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ʼ����ġ�ѡ��ָ�롱�ű�
/// ��ʼ�����ǵ�0����������ʼ��Ϸ�ǵ�1������
/// </summary>
public class OptionPoint : MonoBehaviour
{
    //���ѡ��
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
        //���choiceΪ1������Ұ��¿ո���ô�л��������������л���Ҫ���볡������������ռ䣺using UnityEngine.SceneManagement;��
        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
