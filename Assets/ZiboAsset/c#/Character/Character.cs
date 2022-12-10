using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour//��������������ص�card prefab ��
{


    [Header("�Զ����ñ���")]
    public GameObject CharacterPrefab;
    //public Transform currentTransform;
    [SerializeField]
    public Transform bornTransform = null;
    public MapPoint onMapPoint;

    public bool isReflectHarmOnce = false;
    public float reflectProportion = 1;

    public bool isCannotHurtOnce = false;

    public float maxHealth;
    public float defenceAbility = 0.1f;
    public float attackForce;
    public float shield;
    [SerializeField]
    public int MaxEnergy = 4;
    public int currentEnergy;
    public int RecoverEnergyATurn = 3;

    public Dictionary<Cyberborg.CyberborgType,Cyberborg> cyberborgs = new Dictionary<Cyberborg.CyberborgType,Cyberborg>();//���ڸ�ʵ���������Ҿ��е���������
    
    [Header("�ֶ����ñ���")]
    public List<Cyberborg> cyberborgPrefabs;
    public Vector2 characterSize = new Vector2(7,16);
    //һ���ֵ䣬�ֵ�����key��ȷ����/ /һ�������Cyberborg�ű���һ�����ܽű���Ԥ�Ƽ���
    /// </summary>
    //������ˣ��Ƿ����ֲ����ֲ�Ѫ�����Ƿ����Ȳ����Ȳ�Ѫ�����Ƿ����������壬
    //��û�����������������Ϊ��������������Ѫ��
    //��Ҫ���ĵ���AI�й���ģʽ���ڹ���ʱ����ʱ���ѡ��һ�����壩����Ҫ����player enemy��attack��attacked�ӿں��������һ��������������һ���֣�����
    //public 

    public void InitializeMapPosition()     //��ȡprefab
    {

        GameObject tGO = new GameObject("tGO");
        tGO.AddComponent<InistantiateCahracter>();
        tGO.GetComponent<InistantiateCahracter>().Init(gameObject, bornTransform);
        //Instantiate(this.gameObject, bornTransform.position, bornTransform.rotation);
    }
    public void setMaxHeatlth(float m)
    {
        if (m <= 0)
        {
            maxHealth = 0;
        }
        else
        {
            maxHealth = m;
        }
    }
    public void setDefenceAbility(float m)
    {
        if (m <= 0)
        {
            defenceAbility = 0;
        }
        else
        {
            defenceAbility = m;
        }
    }
    public void setAttackForce(float m)
    {
        if (m <= 0)
        {
            attackForce = 0;
        }
        else
        {
            attackForce = m;
        }
    }
    public void AddDamageableScript()
    {
        Damagable dama = gameObject.AddComponent<Damagable>();
        dama.DamageableMaxHealth = maxHealth;

        
    }
    public void ChangeCyberborg(Cyberborg cy)//�ڽ������������ʱ����Ҫ����һ��prefab(ע�����Ѿ����ص�������)����,�÷����ᰲװ���壬��������Ѫ����������������б�
    {

        if(cy == null)
        {
            Debug.Log("No cyberborg");
            return;
        }
        //ע��Ҫ�ı�Ĳ���pCyberborgrefabs�����������cyberborg����ע����ս��������浵��cyberborgPrefabs���������cyberborgs
        if (cyberborgs[cy.cyberborgType] != null)
        {
            //���滻��ͬ��������ֱ�Ӵݻٻ��Ƿ��뱳��?���Կ��Ǵ���������
            Destroy(cyberborgs[cy.cyberborgType]);
        }

        
        Cyberborg cyInstance = (Cyberborg) Instantiate(cy);
        //���úó�����
        cyInstance.holdCharacter = this;
        
        cyInstance.SetHealthBar();//��Һ͵������ú�Ѫ���ķ�ʽ��ͬ,���Ҫ�����ó�����������Ѫ����
        
        cyInstance.InstallThisCyberborg();                 //���ø���������Ϊ��ɫ�����壬��������λ�ã���������ɫ��һЩ����Ч������
         foreach(Transform trans in cyInstance.transform.GetComponentsInChildren<Transform>())
        {
            trans.gameObject.layer = 7;
        }   //��ʱ���ã����ɼ���������//������spriterender�Ľ��û���ã�����������Ҫ��Ѫ��Ҳ������һ��
        
        //�����ʵ����
        cyberborgs[cy.cyberborgType] = cyInstance;//���������б�
        //ע�������ʵ��������֮ǰ��Ҫ��ȡ�洢��Ϣ������CyberborgPrefabs,()  
    }
    // Start is called before the first frame update
    public void Start()
    {
        cyberborgs.Add(Cyberborg.CyberborgType.head, null);
        cyberborgs.Add(Cyberborg.CyberborgType.arm, null);
        cyberborgs.Add(Cyberborg.CyberborgType.leg, null);//ע��̳���ִ�иó�ʼ��
        foreach(Cyberborg cyberborg in cyberborgPrefabs)
        {
            ChangeCyberborg(cyberborg);//�ڳ�ʼ��ʱע��playerҪ�Ȼ�ȡ�浵�е�����prefabs����ִ�У�
            
        }
        //����ڹ�������ű���������Ҳ����ִ��
    }

    // Update is called once per frame
    void Update()
    {
        //ע�����������������������ִ��
       
    }
}
