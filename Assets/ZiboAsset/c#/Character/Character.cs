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
    public float defenceAbility;
    public float attackForce;
    public float currenthealth;
    public float shield;
    [SerializeField]
    public int MaxEnergy = 4;
    public int currentEnergy;
    public int RecoverEnergyATurn = 3;
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
        gameObject.AddComponent<Damagable>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        CharacterPrefab = this.gameObject;//ֱ�ӻ�ȡ��Ӧ��ʵ��
        AddDamageableScript();
        Debug.Log("the addDamageable execute");
        //����ڹ�������ű���������Ҳ����ִ��
    }

    // Update is called once per frame
    void Update()
    {
        //ע�����������������������ִ��
        if (GetComponent<Damagable>() != null)
        {
            currenthealth = this.GetComponent<Damagable>()._health;
        }
    }
}
