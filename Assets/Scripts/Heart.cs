using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 心脏行为脚本
/// </summary>
public class Heart : MonoBehaviour
{
    //属性：


    //引用：
    //心脏对象的精灵渲染器组件Sprite Renderer
    private SpriteRenderer sr;
    //获取心脏死亡图片（资源文件夹的Graphics文件夹下的Map的第六张图片）
    public Sprite deadHeart;
    //获取爆炸效果
    public GameObject explosionEffectPrefab;
    //获取AudioClip引用
    public AudioClip dieAudio;

    //获取sr组件
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    //当心脏与子弹发生触发检测，切换心脏图片为心脏死亡图片
    /// <summary>
    /// Heart死亡，玩家也死亡
    /// </summary>
    private void Die()
    {
        //播放死亡音效
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
        //心脏死亡，产生爆炸特效
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        sr.sprite = deadHeart;
        //Heart死亡，玩家也死亡
        PlayerManager.Instance.lifeCount = 0;
        PlayerManager.Instance.isDead = true;
    }
}
