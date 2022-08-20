using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆炸特效行为脚本
/// </summary>
public class ExplosionEffect : MonoBehaviour
{
    /// <summary>
    /// 当挂有本脚本的预制件（爆炸特效）被实例化后，调用Start方法
    /// </summary>
    private void Start()
    {
        //0.2后销毁物体（爆炸特效播放时间为0.167s）
        //爆炸特效被创建后就会播放特效，等待0.2让动画播放完后销毁动画
        Destroy(gameObject, 0.2f);
    }
}
