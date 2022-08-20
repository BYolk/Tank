using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ϊ�ű�,���Է�������ͬPlayer.cs�ű��������ƶ������ɽű����ƶ�������ҿ���
/// </summary>
public class Enemy : MonoBehaviour
{
    //���ԣ�
    public float speed = 3;
    public Sprite[] tankSprite;
    private Vector3 bulletEulerAngles;
    //����һ������������,timeValChangeDirection��Ҫ���ʼֵ����Ȼһ������ת����ܲ����õ���һ������������
    private float vertical = -1;
    private float horizontal;
    //private bool isDefended = true;
    //private float defendedTimeVal = 3;

    //���ã�
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public GameObject explosionEffectPrefab;
    //public GameObject shieldEffectPrefab;

    //��ʱ��
    //������ʱ��
    private float timeValAttack;
    //�ı�ת���ʱ��
    private float timeValChangeDirection;

    /// <summary>
    /// ��Awake�����ڻ�ȡSpriteRenderer���
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

        //���˹�����ʱ��
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
    /// �̶�ʱ��ִ��һ�Σ�Ĭ��0.02sִ��һ��
    /// FixUpdate����Update����֮��ִ��
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// ̹�˵Ĺ�������
    /// </summary>
    private void Attack()
    {
        timeValAttack = 0;
        //�ӵ�ʵ�����Ƕȣ���ǰ̹�˵ĽǶ�+�ӵ�Ӧ����ת�ĽǶ�,�����ӵ�bulletEulerAngles��Ҫ��Move�����������̹�˷���ȷ��
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
    }

    /// <summary>
    /// ̹���ƶ�����
    /// </summary>
    private void Move()
    {
        if(timeValChangeDirection >= 4)
        {
            // ת��ʱ���ʱ������4��
            int num = Random.Range(0, 8);
            if(num >= 5)
            {
                //�����ߣ�����Ϊ�˷�֮3
                vertical = -1;
                horizontal = 0;
            }
            else if(num == 0)
            {
                //�����ߣ�����Ϊ�˷�֮һ
                vertical = 1;
                horizontal = 0;
            }
            else if(num > 0 && num <=2)
            {
                //������
                horizontal = -1;
                vertical = 0;
            }
            else if(num > 2 && num <= 4)
            {
                //������
                horizontal = 1;
                vertical = 0;
            }
            //��ת��timeValChangeDirectionҪ����
            timeValChangeDirection = 0;
        }
        else
        {
            //ת��ʱ���ʱ����δ����4��
            timeValChangeDirection += Time.deltaTime;
        }

        //�������������ת
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
        //���ϻ���������ת
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
    /// ̹����������������������Ч�����������Ϸ����
    /// </summary>
    private void Die()
    {
        PlayerManager.Instance.playerScore++;//̹�˱����У���ҷ�����1
        //if (isDefended) return;
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    /// <summary>
    /// �����������ˣ��ϰ��ǽ��������������ײ����ײ��⣩������ת��
    /// ת�򷽷���������ת���ʱ������Ϊ4������ʱ�����ڵ���4ʱ�����˻�ִ��ת�����
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
