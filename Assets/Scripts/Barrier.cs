using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障碍物行为脚本
/// </summary>
public class Barrier : MonoBehaviour
{
    //子弹碰撞障碍物声音片段
    public AudioClip hitAudio;

    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
