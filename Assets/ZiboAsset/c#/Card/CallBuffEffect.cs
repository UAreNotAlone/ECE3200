using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  CallBuffEffect:MonoBehaviour, IEffect//rangeAttack�ȵ��ã���buffTransformʵ����buff icon,
                                   //ʣ�µĽ���buff�Լ�����,
                                   //��ϸ��һ�룬ʵ����buff���Ű������buffIconManager����ɣ�����ֻ����buffʵ����֮���buff���彻��manager��list
{                                      //ͨ����Ӳ�ͬ��buffԤ�Ƽ���ʵ�ֲ�ͬ��buff
                                        //���Կ���buff�����غϣ��Ӻ�ʱ��ʼ

    
    public BattleGameMaster battleGameMaster;
    public Transform buffTransform;
    [Header("��Ҫ�ֶ�����")]
    public Buff buffPrefab;
    public int buffStartAfter = 1;
    public int buffEndAfter = 2;//
    public int buffLastfor = 1;//���Կ���buff�غϣ�Ҳ������=ʹ��buffĬ�ϲ���buff���
    // Start is called before the first frame update
    
    void Start()
    {
        battleGameMaster = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();
        buffTransform = GameObject.Find("BuffTransform").transform;
    }

    public void CauseEffect(List<Transform> t)
    {
        Buff buff = Instantiate(buffPrefab, buffTransform.position, buffTransform.rotation);//ʵ����buff
                                                                                                //Debug.Log(buff.gameObject.GetComponent<IBuff>());
        buff.SetActiveParameter(battleGameMaster.player,buffStartAfter, buffLastfor, buffEndAfter);//����buffʵ��Ч��
        buffTransform.GetComponent<buffIconManager>().AddBuff(buff.transform);//����buffͼ��
    }
    
}
