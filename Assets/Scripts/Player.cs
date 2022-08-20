using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������Ϊ�ű�
/// </summary>
public class Player : MonoBehaviour
{
    //���ԣ�
    //�����ٶ�����
    public float speed = 3;
    //���徫�����飬���ڴ��������������ͼƬ����Inspector�Ͻ�������������ͼƬ����Sprite�����ڣ��ֱ���0��8,16,24��˳��ֱ����ϣ��ң��£���
    public Sprite[] tankSprite;
    //�����ӵ�ŷ����
    private Vector3 bulletEulerAngles;
    //�����ʱ�������ڹ���CD
    private float timeVal;
    //��������޵�״̬��ע�⣺����ֵĬ��ֵΪfalse
    private bool isDefended = true;
    //��������޵�ʱ��
    private float defendedTimeVal = 3;

    //���ã�
    //��ȡ��ҵ�Sprite Renderer���������ͼƬ�л�
    private SpriteRenderer spriteRenderer;
    //��ȡ�ӵ�prefab
    public GameObject bulletPrefab;
    //��ȡ��ը��ЧԤ���壬������������
    public GameObject explosionEffectPrefab;
    //��ȡ�޵���ЧԤ���壬�����޵з���
    public GameObject shieldEffectPrefab;
    //��ȡ��Դ����
    public AudioSource audioSource;
    public AudioClip[] tankAudio;

    /// <summary>
    /// ��Awake�����ڻ�ȡSpriteRenderer���
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //����޵�ʱ���ʱ��
        if (isDefended)
        {
            //������ҵĻ���
            shieldEffectPrefab.SetActive(true);
            defendedTimeVal -= Time.deltaTime;
            if(defendedTimeVal <= 0)
            {
                isDefended = false;
                //����ʱ��ľ����ر���һ���
                shieldEffectPrefab.SetActive(false);
            }
        }


        //��ҹ�����ʱ��
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
    /// �̶�ʱ��ִ��һ�Σ�Ĭ��0.02sִ��һ��
    /// FixUpdate����Update����֮��ִ��
    /// </summary>
    private void FixedUpdate()
    {
        //���Heart�����У�PlayerManager.Instance.isDefeat���Ϊtrue��ֱ��return��ִ��FixUpdate���������ֹͣ�ƶ�����
        if (PlayerManager.Instance.isDefeat)
        {
            timeVal = -999f;
            return;
        }

        Move();

        
    }

    /// <summary>
    /// ̹�˵Ĺ�������
    /// </summary>
    private void Attack()
    {
        //������¿ո�ʵ�����ӵ������ӵ�Ĭ��ʵ�����������ϣ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeVal = 0;
            //�ж�̹�˷��򣬸���̹�˷�����ת�ӵ��������ӵ�������̹�˷���һ��
            //�ӵ�ʵ�����Ƕȣ���ǰ̹�˵ĽǶ�+�ӵ�Ӧ����ת�ĽǶ�,�����ӵ�bulletEulerAngles��Ҫ��Move�����������̹�˷���ȷ��
            //ʵ�����ӵ�����
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
           
        }
    }

    /// <summary>
    /// ̹���ƶ�����
    /// </summary>
    private void Move()
    {
        //��ȡ��������
        //Input.GetAxisRaw ������Ϸ���е�ʱ��, ���������úõļ��̾ͻ᷵�� 1�� - 1������ֵ
        //Input.GetAxis �����������õĽ���᷵��һ�����Ƽ��ٶȵ�ֵ  0.1-- > 0.3-- > 0.1Ȼ�󽫻����μ���..����ɲ���Ϳ���.
        //̹�˴�ս����б�������У����Ի�ȡһ�������Ҫ�ƶ�һ������
        float horizontal = Input.GetAxisRaw("Horizontal");
        //����Ұ������󰴼�ʱ��horizontalΪ-1������horizontalΪ-1ʱ���л��������ͼƬ�������ң�����ͬ��
        if (horizontal < 0)
        {
            //���̹�������ƶ����ӵ���������90����ת
            spriteRenderer.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (horizontal > 0)
        {
            //���̹�������ƶ����ӵ�������90����ת
            spriteRenderer.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        //���horizontal�����ߴ���0.05��˵��������ƶ��������ƶ���Դ
        if(Mathf.Abs(horizontal) > 0.05f)
        {
            audioSource.clip = tankAudio[1];
            //��Ϊmove�������ƶ�ʱ���һֱ���ã����Ի�һֱ�����ƶ���Դ����Դ�Ĳ��Ż��غϱ�úܷ��ӣ�������Ҫ�ж����������ڲ��ŵ�ʱ�򣬾Ͳ��ٵ��ò��ŷ�����
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        //ÿ���ƶ�3�ף��������������귽��
        transform.Translate(Vector3.right * horizontal * speed * Time.fixedDeltaTime, Space.World);
        //����ˮƽ����˳λ�����ˮƽ�������ƶ����Ͳ��ܴ�ֱ�������ˮƽ����û���ƶ������жϴ�ֱ������û���ƶ�
        if (horizontal != 0)
        {
            return;
        }


        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical < 0)
        {
            //���̹�������ƶ����ӵ�������180����ת
            spriteRenderer.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (vertical > 0)
        {
            //���̹�������ƶ����ӵ���������ת���ӵ�����Ĭ�����ϣ�
            spriteRenderer.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        //��� vertical �����ߴ���0.05��˵��������ƶ��������ƶ���Դ
        if (Mathf.Abs(vertical) > 0.05f)
        {
            audioSource.clip = tankAudio[1];
            //��Ϊmove�������ƶ�ʱ���һֱ���ã����Ի�һֱ�����ƶ���Դ����Դ�Ĳ��Ż��غϱ�úܷ��ӣ�������Ҫ�ж����������ڲ��ŵ�ʱ�򣬾Ͳ��ٵ��ò��ŷ�����
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        transform.Translate(Vector3.up * vertical * speed * Time.fixedDeltaTime, Space.World);

        //���̹���������Ҷ�û�ƶ������ž�ֹ��Ч
        if(Mathf.Abs(vertical) < 0.05f && Mathf.Abs(horizontal) < 0.05f)
        {
            audioSource.clip = tankAudio[0];
            //��Ϊmove�������ƶ�ʱ���һֱ���ã����Ի�һֱ�����ƶ���Դ����Դ�Ĳ��Ż��غϱ�úܷ��ӣ�������Ҫ�ж����������ڲ��ŵ�ʱ�򣬾Ͳ��ٵ��ò��ŷ�����
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }


    /// <summary>
    /// ̹����������������������Ч�����������Ϸ����
    /// </summary>
    private void Die()
    {
        //�����Ҵ����޵�״̬��ֱ��return��ִ�����淽��
        if (isDefended) return;
        PlayerManager.Instance.isDead = true;//��ұ��ӵ����У�isDead����Ϊtrue������Recover�������и���������������0��
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
