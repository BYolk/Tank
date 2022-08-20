using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ӵ���Ϊ�ű�
/// </summary>
public class Bullet : MonoBehaviour
{
    //�����ӵ��ٶ�����
    public float speed = 10;
    //��������ӵ�������ӵ��� isPlayerBulletΪfalse��ʾ�����ӵ�
    public bool isPlayerBullet;

    private void Update()
    {
        //transform.forward������Z���ƶ�����2D���泬ǰ�ƶ���transform.up Y���ƶ�
        //���transform��һ����������������ô�ڶ�������Ҫ����������ϵ�������һ����������������ϵ���ڶ��������ɲ���Ҳ������������ϵ
        transform.Translate(transform.up * speed * Time.deltaTime,Space.World);
    }

    /// <summary>
    /// �ӵ��������
    /// SendMessage�����ĺô����ڣ����ǲ���֪�����ӵ������������Ķ�����ʲô��������Ҫ���ö����ĳ��������Ҫ��ͨ����������Collider2D(other)�����ȥ��ȡ����������ٵ��ö���ķ���������̫�鷳�ˡ�ʹ��SendMessageֻ��Ҫ����һ�������������ͻ��Զ�ȥother�������Ӧ����Ϸ����ű��ڲ��ҵ������������һֱ�ķ�����������
    /// </summary>
    /// <param name="other">���ӵ���ײ���������ǩ</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            //�����ұ��ӵ����������ű�ը��Ч������ֵ-1���ڳ����ظ�������޵�ʱ�䶯������Щ��������������йصģ�д����ҵĽű�Player.cs��Ȼ��ͨ���ýű�����
            case "Tank":
                //�������ҷ�����ײ�����ǵ����ӵ������������������
                if(!isPlayerBullet)
                {
                    //ͨ�����ݷ���������collider2D����ķ������˴�other��ʾ�������ӵ���������������ң���������ҵ�Die����
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            //������ӵ������������������࣬�����ӵ������ࣨ�޹�����ӵ����ǵ����ӵ���������������������Ԥ���塱������Դ�ļ���Graphics��Map�µĵ�����ͼƬ����ΪHeart��ӽű�����Heart���ӵ������������ʱ����Heart�ӵ��л�ΪMap�µĵ�����ͼƬ��ʾ����
            case "Heart":
                Destroy(gameObject);
                other.SendMessage("Die");
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            //������ӵ���������������ǽ�������ӵ���ǽ���޹�����ӵ����ǵ����ӵ���
            case "Wall":
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
            //������ӵ����������������ϰ���Ϳ���ǽ������ǽҲ�����ϰ����������ɣ���ǩҲ��Barrier���������ӵ����޹�����ӵ����ǵ����ӵ���
            case "Barrier":
                Destroy(gameObject);
                //����Barrier�ű�������Ч�ķ���:�ж���ҵ��ӵ���ֻ����ҵ��ӵ��Ų�����Ƶ
                if (isPlayerBullet)
                {
                    other.SendMessage("PlayAudio");
                }
                break;
            default:
                break;
        }
    }

    
}
