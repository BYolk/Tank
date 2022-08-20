using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制玩家行为脚本
/// </summary>
public class Player : MonoBehaviour
{
    //属性：
    //主角速度属性
    public float speed = 3;
    //定义精灵数组，用于存放上下左右四张图片。在Inspector上将上下左右四张图片存入Sprite数组内（分别是0，8,16,24，顺序分别是上，右，下，左）
    public Sprite[] tankSprite;
    //定义子弹欧拉角
    private Vector3 bulletEulerAngles;
    //定义计时器，用于攻击CD
    private float timeVal;
    //定义玩家无敌状态；注意：布尔值默认值为false
    private bool isDefended = true;
    //定义玩家无敌时间
    private float defendedTimeVal = 3;

    //引用：
    //获取玩家的Sprite Renderer组件，进行图片切换
    private SpriteRenderer spriteRenderer;
    //获取子弹prefab
    public GameObject bulletPrefab;
    //获取爆炸特效预制体，用于死亡方法
    public GameObject explosionEffectPrefab;
    //获取无敌特效预制体，用于无敌方法
    public GameObject shieldEffectPrefab;
    //获取音源引用
    public AudioSource audioSource;
    public AudioClip[] tankAudio;

    /// <summary>
    /// 在Awake方法内获取SpriteRenderer组件
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //玩家无敌时间计时器
        if (isDefended)
        {
            //激活玩家的护盾
            shieldEffectPrefab.SetActive(true);
            defendedTimeVal -= Time.deltaTime;
            if(defendedTimeVal <= 0)
            {
                isDefended = false;
                //激活时间耗尽，关闭玩家护盾
                shieldEffectPrefab.SetActive(false);
            }
        }


        //玩家攻击计时器
        if (timeVal > 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// 固定时间执行一次，默认0.02s执行一次
    /// FixUpdate会在Update方法之后执行
    /// </summary>
    private void FixedUpdate()
    {
        //如果Heart被击中，PlayerManager.Instance.isDefeat会变为true，直接return不执行FixUpdate方法，玩家停止移动攻击
        if (PlayerManager.Instance.isDefeat)
        {
            timeVal = -999f;
            return;
        }

        Move();

        
    }

    /// <summary>
    /// 坦克的攻击方法
    /// </summary>
    private void Attack()
    {
        //如果按下空格，实例化子弹对象（子弹默认实例化方向向上）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeVal = 0;
            //判断坦克方向，根据坦克方向旋转子弹方向，让子弹方向与坦克方向一致
            //子弹实例化角度：当前坦克的角度+子弹应该旋转的角度,所以子弹bulletEulerAngles需要在Move方法里面根据坦克方向确定
            //实例化子弹对象
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
           
        }
    }

    /// <summary>
    /// 坦克移动方法
    /// </summary>
    private void Move()
    {
        //获取键盘输入
        //Input.GetAxisRaw 当在游戏运行的时候, 按下你设置好的键盘就会返回 1和 - 1这两个值
        //Input.GetAxis 当按下你设置的建则会返回一个类似加速度的值  0.1-- > 0.3-- > 0.1然后将会依次减少..类似刹车和开车.
        //坦克大战不能斜方向运行，所以获取一个方向就要移动一个方向
        float horizontal = Input.GetAxisRaw("Horizontal");
        //当玩家按下向左按键时，horizontal为-1，即当horizontal为-1时，切换到向左的图片，对向右，上下同理
        if (horizontal < 0)
        {
            //如果坦克向左移动，子弹方向向正90°旋转
            spriteRenderer.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (horizontal > 0)
        {
            //如果坦克向右移动，子弹方向向负90°旋转
            spriteRenderer.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        //如果horizontal绝对者大于0.05，说明玩家在移动，播放移动音源
        if(Mathf.Abs(horizontal) > 0.05f)
        {
            audioSource.clip = tankAudio[1];
            //因为move方法在移动时候会一直调用，所以会一直播放移动音源，音源的播放会重合变得很繁杂，所以需要判定当有音乐在播放的时候，就不再调用播放方法了
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        //每秒移动3米，方向是世界坐标方向
        transform.Translate(Vector3.right * horizontal * speed * Time.fixedDeltaTime, Space.World);
        //设置水平方向顺位：如果水平方向有移动，就不管垂直方向，如果水平方向没有移动，就判断垂直方向有没有移动
        if (horizontal != 0)
        {
            return;
        }


        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical < 0)
        {
            //如果坦克向下移动，子弹方向向负180°旋转
            spriteRenderer.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (vertical > 0)
        {
            //如果坦克向上移动，子弹方向向不旋转（子弹方向默认向上）
            spriteRenderer.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        //如果 vertical 绝对者大于0.05，说明玩家在移动，播放移动音源
        if (Mathf.Abs(vertical) > 0.05f)
        {
            audioSource.clip = tankAudio[1];
            //因为move方法在移动时候会一直调用，所以会一直播放移动音源，音源的播放会重合变得很繁杂，所以需要判定当有音乐在播放的时候，就不再调用播放方法了
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        transform.Translate(Vector3.up * vertical * speed * Time.fixedDeltaTime, Space.World);

        //如果坦克上下左右都没移动，播放静止音效
        if(Mathf.Abs(vertical) < 0.05f && Mathf.Abs(horizontal) < 0.05f)
        {
            audioSource.clip = tankAudio[0];
            //因为move方法在移动时候会一直调用，所以会一直播放移动音源，音源的播放会重合变得很繁杂，所以需要判定当有音乐在播放的时候，就不再调用播放方法了
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }


    /// <summary>
    /// 坦克死亡方法：产生爆发特效，销毁玩家游戏对象
    /// </summary>
    private void Die()
    {
        //如果玩家处于无敌状态，直接return不执行下面方法
        if (isDefended) return;
        PlayerManager.Instance.isDead = true;//玩家被子弹击中，isDead设置为true，调用Recover方法进行复活（如果生命数大于0）
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
