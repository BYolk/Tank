using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人行为脚本,属性方法基本同Player.cs脚本，但是移动攻击由脚本控制而不是玩家控制
/// </summary>
public class Enemy : MonoBehaviour
{
    //属性：
    public float speed = 3;
    public Sprite[] tankSprite;
    private Vector3 bulletEulerAngles;
    //敌人一出生就往下走,timeValChangeDirection不要设初始值，不然一出生就转向可能不会让敌人一出生就往下走
    private float vertical = -1;
    private float horizontal;
    //private bool isDefended = true;
    //private float defendedTimeVal = 3;

    //引用：
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public GameObject explosionEffectPrefab;
    //public GameObject shieldEffectPrefab;

    //计时器
    //攻击计时器
    private float timeValAttack;
    //改变转向计时器
    private float timeValChangeDirection;

    /// <summary>
    /// 在Awake方法内获取SpriteRenderer组件
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        /*if (isDefended)
        {
            shieldEffectPrefab.SetActive(true);
            defendedTimeVal -= Time.deltaTime;
            if (defendedTimeVal <= 0)
            {
                isDefended = false;
                shieldEffectPrefab.SetActive(false);
            }
        }*/

        //敌人攻击计时器
        if (timeValAttack >= 3)
        {
            Attack();
        }
        else
        {
            timeValAttack += Time.deltaTime;
        }
    }

    /// <summary>
    /// 固定时间执行一次，默认0.02s执行一次
    /// FixUpdate会在Update方法之后执行
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 坦克的攻击方法
    /// </summary>
    private void Attack()
    {
        timeValAttack = 0;
        //子弹实例化角度：当前坦克的角度+子弹应该旋转的角度,所以子弹bulletEulerAngles需要在Move方法里面根据坦克方向确定
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
    }

    /// <summary>
    /// 坦克移动方法
    /// </summary>
    private void Move()
    {
        if(timeValChangeDirection >= 4)
        {
            // 转向时间计时器到达4秒
            int num = Random.Range(0, 8);
            if(num >= 5)
            {
                //往下走，概率为八分之3
                vertical = -1;
                horizontal = 0;
            }
            else if(num == 0)
            {
                //往上走，概率为八分之一
                vertical = 1;
                horizontal = 0;
            }
            else if(num > 0 && num <=2)
            {
                //向左走
                horizontal = -1;
                vertical = 0;
            }
            else if(num > 2 && num <= 4)
            {
                //向右走
                horizontal = 1;
                vertical = 0;
            }
            //旋转后timeValChangeDirection要归零
            timeValChangeDirection = 0;
        }
        else
        {
            //转向时间计时器还未到达4秒
            timeValChangeDirection += Time.deltaTime;
        }

        //向左或者向右旋转
        if (horizontal < 0)
        {
            spriteRenderer.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (horizontal > 0)
        {
            spriteRenderer.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        transform.Translate(Vector3.right * horizontal * speed * Time.fixedDeltaTime, Space.World);
        //向上或者向下旋转
        if (vertical < 0)
        {
            spriteRenderer.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (vertical > 0)
        {
            spriteRenderer.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        transform.Translate(Vector3.up * vertical * speed * Time.fixedDeltaTime, Space.World);
    }


    /// <summary>
    /// 坦克死亡方法：产生爆发特效，销毁玩家游戏对象
    /// </summary>
    private void Die()
    {
        PlayerManager.Instance.playerScore++;//坦克被击中，玩家分数加1
        //if (isDefended) return;
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    /// <summary>
    /// 如果敌人与敌人，障碍物，墙，河流，发生碰撞（碰撞检测），敌人转向
    /// 转向方法：将敌人转向计时器设置为4，当计时器大于等于4时，敌人会执行转向操作
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Heart" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Barrier" || collision.gameObject.tag == "Grass" || collision.gameObject.tag == "River")
        {
            timeValChangeDirection = 4;
        }
    }
}
