using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ϰ�����Ϊ�ű�
/// </summary>
public class Barrier : MonoBehaviour
{
    //�ӵ���ײ�ϰ�������Ƭ��
    public AudioClip hitAudio;

    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
