using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyberborg : Damagable//��Ҹ���cyberborg��ͨ��character�����changecyberʵ�֣��洢����������ݿ���ͨ������character�����cyberborgs���
{
    public enum CyberborgType
    {
        head,
        arm,
        leg
    }

    public Character holdCharacter;
   //ע�⵱ĳ����ɫ�̳н�ɫ��ʱ�����в��������ھ����ɫ�����þ���������������壬���ܽ�ɫ��������Ӧ������prefabs�������ټ�������ű�������Դ���ű���ʵ������
    //������Ʋ����ڸ���װ����
    Dictionary<CyberborgType, float> arrange = new Dictionary<CyberborgType, float>();
    [Header("�ֶ����ñ���")]
    public CyberborgType cyberborgType = CyberborgType.head;
    public float maxBorgHealth = 20f;
    public float attack = 10;//һ�ι������ܹ�����ȡ���ڿ��ƣ�����ʹ�õ����壬����ҵĻ�������
    public float defense = 0.2f;//һ�η����ķ�����ȡ���ڵ��˹����Ĳ�λ�͸ò�λ�ķ��������������ķ�����
    // Start is called before the first frame update
  
   
    public void Awake()
    {
        base.Start();
        arrange.Add(CyberborgType.head, -15);
        arrange.Add(CyberborgType.arm, -30);
        arrange.Add(CyberborgType.leg, -45);
        Health = maxBorgHealth;
        DamageableMaxHealth = maxBorgHealth;//����damageable�����current health��maxhealth
        gameObject.AddComponent<NoSpecialEffect>();
       
        //holdCharacter = gameMaster.enemies[0];//ʵ����䣬ɾȥ//�Ƿ�Ҫ���������ʵ����Ѫ���Լ����úø������������Ȳ���//

        //����ֻʵ���˼���Ѫ����Ϊcyber��������������壬�����Զ�cyber����������дݻ٣�s
    }

    public void Start()//Ϊʲô���ﲻִ����
    {
        
    }
    //��д��������
    public override void Death()//Ҳ���Բ���д��ע���ڵ���ʱ
    {
        
            //ע����ʵ�������������ڶ�Ӧ����ű�ִ�й����У���������������д�ķ���
        base.Death();
        
        //holdCharacter.cyberborgs[cyberborgType] = null;//���������ҵ������б��еĸ�����
        Destroy(this.gameObject);//��damageable����ű�ִ�У�//������������ű�Ƕ��ýű�����������������д

    }
    public void InstallThisCyberborg()
    {
            transform.SetParent(holdCharacter.transform);
            transform.position = holdCharacter.transform.position + new Vector3(-0.5f * holdCharacter.characterSize.x,
                holdCharacter.characterSize.y + 1 + arrange[cyberborgType] * holdCharacter.characterSize.y / 45f, 0);
            
       
    }

    public void SetHealthBar()
    {
        playHealthBar = Resources.Load<GameObject>("PlayerHealthBar");
        enemyHealthBar = Resources.Load<GameObject>("EnemyHealthBar");
        if (holdCharacter.gameObject.tag == "Player")
        {
            Transform trans = GameObject.Find("PlayerHealthBarTransform").transform;
            thisHealthBar = (GameObject)Instantiate(playHealthBar, trans.position, trans.rotation);
            
            thisHealthBar.transform.SetParent(trans.transform);//Ѫ��������������������
            thisHealthBar.transform.position = trans.transform.position + new Vector3(0,
               arrange[cyberborgType], 0);
            //Debug.Log(arrange.Count);

            _health = holdCharacter.gameObject.GetComponent<Character>().maxHealth;
            Bar = thisHealthBar.transform.GetChild(1);
        }
        else if (holdCharacter.gameObject.tag == "Enemy")
        {
            Enemy enemy = holdCharacter.gameObject.GetComponent<Enemy>();
            thisHealthBar = (GameObject)Instantiate(enemyHealthBar);//
            thisHealthBar.transform.SetParent(gameObject.transform);//ע��Ѫ����Ϊ�������������ǽ�ɫ��������
            thisHealthBar.transform.localPosition = Vector3.zero;//�ǵð�enemy��pivot�������ײ�
            
            _health = holdCharacter.gameObject.GetComponent<Character>().maxHealth;
            Bar = thisHealthBar.transform.GetChild(1);
        }
        
        //ע���ʱ��ȡ����Ѫ�� ���Ե����ڸ��� ����������Ͻ�ʵ����
        //ע��
    }
    public void UseCyberborgFuntion()
    {
        ISpeciaEffect[] sps = GetComponents<ISpeciaEffect>();
        foreach(ISpeciaEffect sp in sps)
        {
            sp.SpecialEffect();
        }
    }
    // Update is called once per frame
    
}
