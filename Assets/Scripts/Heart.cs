using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ϊ�ű�
/// </summary>
public class Heart : MonoBehaviour
{
    //���ԣ�


    //���ã�
    //�������ľ�����Ⱦ�����Sprite Renderer
    private SpriteRenderer sr;
    //��ȡ��������ͼƬ����Դ�ļ��е�Graphics�ļ����µ�Map�ĵ�����ͼƬ��
    public Sprite deadHeart;
    //��ȡ��ըЧ��
    public GameObject explosionEffectPrefab;
    //��ȡAudioClip����
    public AudioClip dieAudio;

    //��ȡsr���
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    //���������ӵ�����������⣬�л�����ͼƬΪ��������ͼƬ
    /// <summary>
    /// Heart���������Ҳ����
    /// </summary>
    private void Die()
    {
        //����������Ч
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
        //����������������ը��Ч
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        sr.sprite = deadHeart;
        //Heart���������Ҳ����
        PlayerManager.Instance.lifeCount = 0;
        PlayerManager.Instance.isDead = true;
    }
}
