using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ը��Ч��Ϊ�ű�
/// </summary>
public class ExplosionEffect : MonoBehaviour
{
    /// <summary>
    /// �����б��ű���Ԥ�Ƽ�����ը��Ч����ʵ�����󣬵���Start����
    /// </summary>
    private void Start()
    {
        //0.2���������壨��ը��Ч����ʱ��Ϊ0.167s��
        //��ը��Ч��������ͻᲥ����Ч���ȴ�0.2�ö�������������ٶ���
        Destroy(gameObject, 0.2f);
    }
}
