using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹行为脚本
/// </summary>
public class Bullet : MonoBehaviour
{
    //定义子弹速度属性
    public float speed = 10;
    //区分玩家子弹与敌人子弹， isPlayerBullet为false表示敌人子弹
    public bool isPlayerBullet;

    private void Update()
    {
        //transform.forward是向着Z轴移动，在2D界面超前移动是transform.up Y轴移动
        //如果transform第一个参数是自身方向，那么第二个参数要填世界坐标系；如果第一个参数是世界坐标系，第二个参数可不填也可天自身坐标系
        transform.Translate(transform.up * speed * Time.deltaTime,Space.World);
    }

    /// <summary>
    /// 子弹触发检测
    /// SendMessage方法的好处在于：我们并不知道与子弹发生触发检测的对象是什么对象，所以要调用对象的某个方法需要先通过这个对象的Collider2D(other)组件先去获取到这个对象再调用对象的方法，这样太麻烦了。使用SendMessage只需要传递一个方法名，它就会自动去other组件所对应的游戏对象脚本内查找到与这个方法名一直的方法并调用它
    /// </summary>
    /// <param name="other">被子弹碰撞到的物体标签</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            //如果玩家被子弹碰到，播放爆炸特效，生命值-1，在出生地复活，播放无敌时间动画；这些方法都是与玩家有关的，写在玩家的脚本Player.cs，然后通过该脚本调用
            case "Tank":
                //如果与玩家发生碰撞检测的是敌人子弹，调用玩家死亡方法
                if(!isPlayerBullet)
                {
                    //通过传递方法名调用collider2D对象的方法，此处other表示的是与子弹发生触发检测的玩家，即调用玩家的Die方法
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            //如果与子弹发生触发检测的是心脏，销毁子弹和心脏（无关玩家子弹还是敌人子弹），创建“心脏死亡的预制体”（在资源文件夹Graphics的Map下的第六张图片），为Heart添加脚本，当Heart与子弹发生触发检测时，将Heart子弹切换为Map下的第六张图片表示死亡
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
            //如果与子弹发生触发检测的是墙，销毁子弹和墙（无关玩家子弹还是敌人子弹）
            case "Wall":
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
            //如果与子弹发生触发检测的是障碍物和空气墙（空气墙也是由障碍物制作而成，标签也是Barrier），销毁子弹（无关玩家子弹还是敌人子弹）
            case "Barrier":
                Destroy(gameObject);
                //调用Barrier脚本播放音效的方法:判断玩家的子弹，只有玩家的子弹才播放音频
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
